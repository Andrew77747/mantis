using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumFramework;
using SeleniumTests.Models;

namespace SeleniumTests.Pages
{
    public class UsersPage : BasePage
    {
        #region WebElements

        [FindsBy(How = How.XPath, Using = "//*[@value='Создать учетную запись']")]
        private IWebElement CreateUserBtn { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//h2[text()='Управление пользователями']/..//tbody//td[1]/a")]
        private IList<IWebElement> UsersList { get; set; }

        #endregion
        
        public override bool IsDisplayed()
        {
            return CreateUserBtn.Displayed;
        }

        public override void WaitForLoading()
        {
            CreateUserBtn.WaitForVisible(35000);
        }
        
        public UsersPage Invoke()
        {
            Driver.Url = Urls.Urls.MantisUrl + Urls.Urls.Users;
            WaitForLoading();
            return this;
        }

        public UsersPage InitUserCreation()
        {
            CreateUserBtn.Click();
            return this;
        }

        public List<User> GetUsersList()
        {
            var usersList = new List<User>();

            foreach (var user in UsersList)
            {
                usersList.Add(new User()
                {
                    Username = user.GetAttribute("innerText")
                });
            }

            return usersList;
        }

        public bool IsUserExists(User user)
        {
            foreach (var us in UsersList)
            {
                if (us.GetAttribute("innerText").Equals(user.Username))
                {
                    return true;
                }
            }

            return false;
        }
    }
}