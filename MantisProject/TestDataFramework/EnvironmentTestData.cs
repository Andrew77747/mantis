using TestDataFramework.Extensions;
using TestDataFramework.Models;

namespace TestDataFramework
{
    public static class EnvironmentTestData
    {
        public static EnvironmentTestDataModel Data { get; }

        static EnvironmentTestData() //todo зачем здесь статик конструктор и статик класс
        {
            Data = TerminalConfigurationExtension.GetCommonTestData();
            var environmentTestData = TerminalConfigurationExtension.GetEnvironmentTestData();
            
            CopyValues(Data, environmentTestData);

        }

        private static void CopyValues<T>(T target, T source)
        {
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
        }
    }
}