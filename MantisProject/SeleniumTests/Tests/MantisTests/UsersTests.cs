using NUnit.Framework;
using SeleniumTests.Models;
using SeleniumTests.Pages;

namespace SeleniumTests.Tests.MantisTests
{
    /// <summary>
    /// Тесты управления пользователями
    /// </summary>
    [TestFixture]
    public class UsersTests : BaseTest
    {
        private readonly LoginPage _loginPage = new LoginPage();
        private readonly UsersPage _usersPage = new UsersPage();
        private readonly CreateUserPage _createUserPage = new CreateUserPage();
        private readonly ManageUserPage _manageUserPage = new ManageUserPage();
        
        [OneTimeSetUp]
        public void Login()
        {
            _loginPage
                .Invoke()
                .Login();
            _usersPage.Invoke();
        }
        
        [OneTimeTearDown]
        public void LogOff()
        {
            _usersPage.FastLogoff();
        }

        /// <summary>
        /// Проверяем создание нового пользователя
        /// </summary>
        [Test]
        public void CreateNewUser()
        {
            var user = User.Create();
            
            _usersPage.InitUserCreation();
            _createUserPage.CreateUser(user);
            _manageUserPage.WaitForLoading();
            _usersPage.Invoke();

            Assert.IsTrue(_usersPage.IsUserExists(user), "User is not created");
        }
        
        //todo делать
        // добавить тесты: удаление пользователя, неактивный пользователь, защищенный пользователь
        // рефачить - перенести со страницы проектов нужные методы на страницу создания проектов
        // распределить все по папочкам
        // генерировать имена и имейлы в моделях - для этого нужен генератор и тестдата
        // пробовать подключиться к базе и дописать проверки
        // разбирать вэйтеры - смотреть как здесь сделано и думать надо ли мне какие свои оставить
    }
}