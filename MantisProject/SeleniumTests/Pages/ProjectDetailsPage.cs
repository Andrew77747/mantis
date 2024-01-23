using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumFramework;

namespace SeleniumTests.Pages
{
    public class ProjectDetailsPage : BasePage
    {
        #region WebElements

        [FindsBy(How = How.XPath, Using = "//*[@value='Удалить проект']")]
        private IWebElement DeleteProjectBtn { get; set; }
        
        [FindsBy(How = How.ClassName, Using = "confirm-msg")]
        private IWebElement ConfirmMsg { get; set; }

        #endregion
        
        public override bool IsDisplayed()
        {
            return DeleteProjectBtn.Displayed;
        }

        public override void WaitForLoading()
        {
            DeleteProjectBtn.WaitForVisible(35000);
        }

        public void DeleteProject()
        {
            DeleteProjectBtn.Click();
            Wait.WaitForVisible(ConfirmMsg, 5000);
            DeleteProjectBtn.Click();
        }
    }
}