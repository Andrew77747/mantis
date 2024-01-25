using System.Configuration;
using System.IO;
using System.Reflection;
using ApiFramework.Models;
using Newtonsoft.Json;

namespace ApiFramework
{
    public static class EnvironmentConfiguration
    {
        public static ConfigurationModel _configuration { get; set; } 
        public static string EnvironmentName { get; set; }
        
        private static readonly string PathToConfig = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        static EnvironmentConfiguration()
        {
            _configuration = SetConfiguration();
        }

        /// <summary>
        /// Возвращает значение, заданное в переменной окружения environmentName,
        /// или значение, заданное в ApiFramework.config, если переменная окружения отсутствует
        /// </summary>
        private static string GetEnvironmentName()
        {
            EnvironmentName = System.Environment.GetEnvironmentVariable("environmentName");
            if (EnvironmentName != null)
            {
                return EnvironmentName;
            }
            
            var configMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = Path.Combine(PathToConfig, "ApiFramework.config")
            };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            EnvironmentName = config.AppSettings.Settings["environmentName"].Value;
            return EnvironmentName;
        }

        /// <summary>
        /// Десериализует environments.json файл
        /// </summary>
        private static void DeserializeJson(string environmentName)
        {
            var json = File.ReadAllText(Path.Combine(PathToConfig, "Environments", $"{environmentName}.json"));
            _configuration = JsonConvert.DeserializeObject<ConfigurationModel>(json);
        }
        
        /// <summary>
        /// Десериализует environments.json файл, возвращает конфигурацию тестовой среды, заданной в ApiFramework.config файле
        /// </summary>
        private static ConfigurationModel SetConfiguration()
        {
            if (_configuration != null) 
                return _configuration;
            EnvironmentName = GetEnvironmentName();
            DeserializeJson(EnvironmentName);

            return _configuration;
        }
        
        /// <summary>
        /// Десериализует environments.json файл, возвращает конфигурацию тестовой среды, исходя из environmentName
        /// </summary>
        /// <param name="environmentName"></param>
        public static ConfigurationModel SetConfiguration(string environmentName)
        {
            DeserializeJson(environmentName);
            return _configuration;
        }
    }
}