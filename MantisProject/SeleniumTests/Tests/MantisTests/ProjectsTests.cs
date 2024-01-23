using NUnit.Framework;
using SeleniumTests.Models;
using SeleniumTests.Pages;

namespace SeleniumTests.Tests.MantisTests
{
    /// <summary>
    /// Тесты управления проектами
    /// </summary>
    public class ProjectsTests: BaseTest
    {
        private readonly LoginPage _loginPage = new LoginPage();
        private readonly ProjectsPage _projectsPage = new ProjectsPage();
        private readonly ProjectDetailsPage _projectDetailsPage = new ProjectDetailsPage();

        [OneTimeSetUp]
        public void Login()
        {
            _loginPage
                .Invoke()
                .Login();
            _projectsPage.Invoke();
        }

        /// <summary>
        /// Проверяем создание проекта
        /// </summary>
        [Test]
        public void CreateProject()
        {
            var project = Project.Create();
            
            _projectsPage.CreateNewProjects(project);
            
            Assert.IsTrue(_projectsPage.IsProjectContains(project), "Project is not created"); //todo потом переделать ассерт на проверку в базе
        }
        
        /// <summary>
        /// Проверяем удаление проекта
        /// </summary>
        [Test]
        public void DeleteProject()
        {
            _projectsPage.CreateIfNoProject();
            
            var projectList = _projectsPage.GetProjectsList();
            var projectToDelete = projectList[0];

            _projectsPage.OpenProject(projectToDelete);
            _projectDetailsPage.DeleteProject();
            
            var newProjectList = _projectsPage.GetProjectsList();
            
            Assert.AreNotEqual(projectList.Count, newProjectList, "Projects count is equal");
            Assert.IsFalse(_projectsPage.IsProjectContains(projectToDelete), "Project is not deleted");
        }
    }
}