using NUnit.Framework;
using SeleniumTests.Pages;

namespace SeleniumTests.Tests.MantisTests
{
    public class testDelete : BaseTest //todo удалить класс - это просто пробный базовый класс
    {
        [Test] //todo delete
        public void TestTest()
        {
            LoginPage login = new LoginPage();
            login.Invoke();
            Thread.Sleep(2000);
        }
    }
}