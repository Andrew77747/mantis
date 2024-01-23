using SeleniumTests.Pages;

namespace SeleniumTests.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string State { get; set; }
        public bool ProjectInheritGlobal { get; set; }
        public string Visibility { get; set; }
        public string Description { get; set; }

        public static Project Create(
            string name = "Тестовый проект", //todo можно заполнять рандомным именем
            string state = "в разработке",
            bool projectInheritGlobal = true,
            string visibility = "публичная",
            string description = "Это тестовый проект")
        {
            return new Project()
            {
                Name = name,
                State = state,
                ProjectInheritGlobal = projectInheritGlobal,
                Visibility = visibility,
                Description = description
            };
        }
    }
}