using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace SeleniumFramework
{
    public class WebDriverRunner
    {
        //todo вынести в enum
        private const string BrowserFirefox = "Firefox";
        private const string BrowserChrome = "Chrome";
        private const string BrowserHeadlessChrome = "HeadlessChrome";

        public static IWebDriver Run(string browserName)
        {
            return StartEmbededDriver(browserName);
        }

        private static IWebDriver StartEmbededDriver(string browserName)
        {
            var options = new ChromeOptions();
            IWebDriver driver = null;

            switch (browserName)
            {
                case BrowserFirefox:
                    driver = new FirefoxDriver();
                    break;
                case BrowserChrome:
                    driver = new ChromeDriver();
                    break;
                case BrowserHeadlessChrome:
                    options.AddArgument("--headless");
                    options.AddArgument("--window-size=1920,979");
                    options.AddArgument("--allow-running-insecure-content");
                    driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options,
                        TimeSpan.FromMinutes(3));
                    break;
                default:
                    throw new ArgumentException($@"{browserName} is not supported");
            }
            
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            return driver;
        }
    }
}