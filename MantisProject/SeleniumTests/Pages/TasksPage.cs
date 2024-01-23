using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumFramework;

namespace SeleniumTests.Pages
{
    public class TasksPage : BasePage
    {
        #region WebElements

        [FindsBy(How = How.XPath, Using = "//*[@value='Создать постоянную ссылку']")]
        private IWebElement CreateLinkBtn { get; set; }

        #endregion
        
        public override bool IsDisplayed()
        {
            return CreateLinkBtn.Displayed;
        }

        public override void WaitForLoading()
        {
            CreateLinkBtn.WaitForVisible(35000);
        }
    }
}