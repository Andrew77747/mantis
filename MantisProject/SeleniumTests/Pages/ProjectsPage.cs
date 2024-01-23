using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumFramework;
using SeleniumTests.Models;

namespace SeleniumTests.Pages
{
    public class ProjectsPage : BasePage
    {
        #region WebElements

        [FindsBy(How = How.XPath, Using = "//*[@value='создать новый проект']")]
        private IWebElement CreateNewProjectBtn { get; set; }

        [FindsBy(How = How.Id, Using = "project-name")]
        private IWebElement ProjectNameInput { get; set; }

        [FindsBy(How = How.Id, Using = "project-status")]
        private IWebElement ProjectStatusSelect { get; set; }

        [FindsBy(How = How.Id, Using = "project-inherit-global")]
        private IWebElement ProjectInheritGlobal { get; set; }

        [FindsBy(How = How.Id, Using = "project-view-state")]
        private IWebElement Visibility { get; set; }

        [FindsBy(How = How.Id, Using = "project-description")]
        private IWebElement Description { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@value='Добавить проект']")]
        private IWebElement AddProjectBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//h2[text()='Проекты']/..//tbody//td[1]/a")]
        private IList<IWebElement> ProjectsList { get; set; }

        #endregion

        public override bool IsDisplayed()
        {
            return CreateNewProjectBtn.Displayed;
        }

        public override void WaitForLoading()
        {
            CreateNewProjectBtn.WaitForVisible(35000);
        }

        public ProjectsPage Invoke()
        {
            Driver.Url = Urls.Urls.MantisUrl + Urls.Urls.Projects;
            WaitForLoading();
            return this;
        }

        public ProjectsPage CreateNewProjects(Project project)
        {
            CreateNewProjectBtn.Click();
            ProjectNameInput.ClearAndEnterValue(project.Name);
            ProjectStatusSelect.SelectDropdownText(project.State);

            switch (project.ProjectInheritGlobal)
            {
                case true:
                    if (!ProjectInheritGlobal.Selected)
                    {
                        ProjectInheritGlobal.Click();
                    }

                    break;

                case false:
                    if (ProjectInheritGlobal.Selected)
                    {
                        ProjectInheritGlobal.Click();
                    }

                    break;
            }

            Visibility.SelectDropdownText(project.Visibility);
            Description.ClearAndEnterValue(project.Description);
            AddProjectBtn.Click();
            WaitForLoading();
            return this;
        }

        public bool IsProjectContains(Project project)
        {
            foreach (var projects in ProjectsList)
            {
                if (projects.GetAttribute("innerText").Equals(project.Name))
                {
                    return true;
                }
            }

            return false;
        }

        public List<Project> GetProjectsList()
        {
            var projectList = new List<Project>();
            foreach (var project in ProjectsList)
            {
                projectList.Add(new Project()
                {
                    Name = project.GetAttribute("innerText")
                });
            }

            return projectList;
        }

        public ProjectsPage CreateIfNoProject()
        {
            if (ProjectsList.Count == 0)
            {
                var project = Project.Create();
                CreateNewProjects(project);
            }

            return this;
        }

        public ProjectsPage OpenProject(Project project)
        {
            foreach (var proj in ProjectsList)
            {
                if (proj.GetAttribute("innerText").Equals(project.Name))
                {
                    proj.Click();
                    break;
                }
            }

            return this;
        }
    }
}