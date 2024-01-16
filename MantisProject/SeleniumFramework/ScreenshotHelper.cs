using System.Runtime.InteropServices;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace SeleniumFramework
{
    public class ScreenshotHelper
    {
        public static void ScreenshotAfterFail() //todo разобраться с этим методом или взять старый свой
        {
            // if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            // {
            //     var directory = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @"C:\screenshots" : @"/Users/Shared/Screenshots";
            //     Directory.CreateDirectory(directory);
            //
            //     //Обрезаем полный путь к тесту и удаляем "SeleniumTests.Tests." либо "WidgetTests.Tests."
            //     var pathToTest = TestContext.CurrentContext.Test.FullName;
            //     int index = (pathToTest.Contains("SeleniumTests")) ? 20 : 18;
            //     var fullname = Path.Combine(directory, TestContext.CurrentContext.Test.FullName.Substring(index) + ".png");
            //
            //     Browser.TakeScreenShot()
            //         .SaveAsFile(fullname, OpenQA.Selenium.ScreenshotImageFormat.Png);
            //
            //     using (var writer = new TeamCityServiceMessages().CreateWriter())
            //     {
            //         writer.PublishArtifact(fullname + " => ScreenshotAfterFail");
            //     }
            // }
        }
    }
}