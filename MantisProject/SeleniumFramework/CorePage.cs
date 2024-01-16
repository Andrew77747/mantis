using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumFramework
{
    public abstract class CorePage
    {
        public IWebDriver Driver => Browser.Driver;

        protected CorePage()
        {
            PageFactory.InitElements(Driver, this);
        }
        
        public abstract bool IsDisplayed();

        public abstract bool WaitForLoading();
        
        public void SwitchToDefaultContent() //todo может вынести это в отдельный класс с методами
        {
            Driver.SwitchTo().DefaultContent();
        }
    }
}