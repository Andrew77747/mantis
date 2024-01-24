using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumFramework;

namespace SeleniumTests.Pages
{
    public class ManageUserPage : BasePage
    {
        #region WebElements

        [FindsBy(How = How.XPath, Using = "//*[@value='Изменить учетную запись']")]
        private IWebElement EditUserBtn { get; set; }

        #endregion
        
        public override bool IsDisplayed()
        {
            return EditUserBtn.Displayed;
        }

        public override void WaitForLoading()
        {
            EditUserBtn.WaitForVisible(35000);
        }
    }
}