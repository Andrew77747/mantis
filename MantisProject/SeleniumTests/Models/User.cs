namespace SeleniumTests.Models
{
    public class User
    {
        public string Username { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Access { get; set; }
        public bool Active { get; set; }
        public bool Protect { get; set; }
        
        public static User Create(
            string username = "Test User", //todo можно заполнять рандомным именем
            string realName = "Sergey Sergeevich",
            string email = "test@test.ru",
            string access = "автор",
            bool active = true,
            bool protect = false)
        {
            return new User()
            {
                Username = username,
                RealName = realName,
                Email = email,
                Access = access,
                Active = active,
                Protect = protect
            };
        }
    }
}