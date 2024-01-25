using System.Reflection;
using ApiFramework;
using TestDataFramework.Models;
using Newtonsoft.Json;

namespace TestDataFramework.Extensions
{
    public static class TerminalConfigurationExtension
    {
        private static readonly string EnvironmentName = EnvironmentConfiguration.EnvironmentName;
        private static readonly string WorkingDirectoryName =
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private const string EnvironmentsTestData = "EnvironmentsTestData";
        private const string CommonTestData = "CommonTestData";

        public static EnvironmentTestDataModel GetEnvironmentTestData()
        {
            var path = Path.Combine(WorkingDirectoryName, EnvironmentsTestData, $"{EnvironmentName}.json");
            return ParseJsonFileToObject<EnvironmentTestDataModel>(path);
        }
        
        public static EnvironmentTestDataModel GetCommonTestData()
        {
            var path = Path.Combine(WorkingDirectoryName, EnvironmentsTestData, $"{CommonTestData}.json");
            return ParseJsonFileToObject<EnvironmentTestDataModel>(path);
        }

        private static TModel ParseJsonFileToObject<TModel>(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<TModel>(json);
        }
    }
}