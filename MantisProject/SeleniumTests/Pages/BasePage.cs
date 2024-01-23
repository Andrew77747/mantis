using SeleniumFramework;

namespace SeleniumTests.Pages
{
    public abstract class BasePage : CorePage
    {
        //todo сделать обычный метод выхода Logoff и посмотреть что еще взять можно из этого класса
        
        // Логаут через переход по ссылке /mantisbt-1.3.20/logout_page.php
        public LoginPage FastLogoff()
        {
            Driver.Url = "http://localhost/mantisbt-1.3.20/logout_page.php";
            return new LoginPage();
        }
    }
}