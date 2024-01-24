using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumFramework;
using SeleniumTests.Models;

namespace SeleniumTests.Pages
{
    public class CreateUserPage : BasePage
    {
        #region WebElements

        [FindsBy(How = How.Id, Using = "user-username")]
        private IWebElement UsernameInput { get; set; }
        
        [FindsBy(How = How.Id, Using = "user-realname")]
        private IWebElement RealNameInput { get; set; }
        
        [FindsBy(How = How.Id, Using = "email-field")]
        private IWebElement EmailInput { get; set; }
        
        [FindsBy(How = How.Id, Using = "user-access-level")]
        private IWebElement AccessSelect { get; set; }
        
        [FindsBy(How = How.Id, Using = "user-enabled")]
        private IWebElement UserEnabled { get; set; }
        
        [FindsBy(How = How.Id, Using = "user-protected")]
        private IWebElement UserProtected { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//*[@value='Создать']")]
        private IWebElement CreateBtn { get; set; }

        #endregion
        
        public override bool IsDisplayed()
        {
            return CreateBtn.Displayed;
        }

        public override void WaitForLoading()
        {
            CreateBtn.WaitForVisible(35000);
        }

        public CreateUserPage CreateUser(User user)
        {
            UsernameInput.ClearAndEnterValue(user.Username);
            RealNameInput.ClearAndEnterValue(user.RealName);
            EmailInput.ClearAndEnterValue(user.Email);
            AccessSelect.SelectDropdownText(user.Access);
            
            switch (user.Active)
            {
                case true:
                    if (!UserEnabled.Selected)
                    {
                        UserEnabled.Click();
                    }

                    break;

                case false:
                    if (UserEnabled.Selected)
                    {
                        UserEnabled.Click();
                    }

                    break;
            }
            
            switch (user.Protect)
            {
                case true:
                    if (!UserProtected.Selected)
                    {
                        UserProtected.Click();
                    }

                    break;

                case false:
                    if (UserProtected.Selected)
                    {
                        UserProtected.Click();
                    }

                    break;
            }
            
            CreateBtn.Click();
            return this;
        }
    }
}