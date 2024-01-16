using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace SeleniumFramework
{
    public static class Browser
    {
        private static IWebDriver _driver = null;

        // Возвращает WebDriver
        // Если Driver создан и браузер открыт - возвращает ссылку на него
        // Если Driver еще не инициализирован - создает новый инстанс WebDriver'a
        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    _driver = WebDriverRunner.Run("Chrome");
                }

                return _driver;
            }
        }
        
        // Закрывает инстанс WebDriver и окно браузера
        public static void CloseDriver()
        {
            if (_driver != null)
            {
                _driver.Dispose();
                _driver = null;
            }
        }
        
        public static Screenshot TakeScreenShot()
        {
            return Driver.TakeScreenshot();
        }
    }
}