using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumFramework;
using SeleniumTests.Urls;

namespace SeleniumTests.Pages
{
    public class LoginPage : CorePage
    {
        #region WebElements

        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement UsernameInput { get; set; }
        
        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement PasswordInput { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//input[@value='Войти']")]
        private IWebElement LoginBtn { get; set; }

        #endregion
        
        public override bool IsDisplayed()
        {
            return LoginBtn.Displayed;
        }

        public override void WaitForLoading()
        {
            LoginBtn.WaitForExists();
        }
        
        public LoginPage Invoke()
        {
            Driver.Url = Urls.Urls.MantisUrl + Urls.Urls.Login; //todo почему два раза Urls
            WaitForLoading();
            return this;
        }

        public void Login()
        {
            Thread.Sleep(1000);
            UsernameInput.SendKeys("administrator"); //todo вынести логин и пароль в тест дату. посмотреть в проекте
            PasswordInput.SendKeys("root");
            LoginBtn.Click();
        }
    }
}