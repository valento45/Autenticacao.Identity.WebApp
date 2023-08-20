using Microsoft.AspNetCore.Identity;

namespace Identity.WebApp.Authorization
{
	public class MyUser 
	{
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }

        public MyUser()
        {
            
        }
    }
}
