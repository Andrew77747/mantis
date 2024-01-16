using SeleniumFramework;

namespace SeleniumTests.Pages
{
    public class LoginPage : BasePage
    {
        public override bool IsDisplayed()
        {
            throw new NotImplementedException(); //todo реализовать метод
        }

        public override bool WaitForLoading()
        {
            throw new NotImplementedException(); //todo реализовать метод
        }
        
        public LoginPage Invoke()
        {
            //Driver.Url = EnvironmentConfiguration._configuration.AdminUrl; //todo сделать конфиги
            Driver.Url = "http://localhost/mantisbt-1.3.20/signup_page.php"; //todo вынести адрес в конфиги
            WaitForLoading();
            return this;
        }
    }
}