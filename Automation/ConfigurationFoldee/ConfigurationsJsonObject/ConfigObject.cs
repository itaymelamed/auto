
namespace Automation.ConfigurationFoldee.ConfigurationsJsonObject
{
    public class RegularUser : IUser
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}

    public class AdminUser : IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Users 
    {
        public RegularUser RegularUser { get; set; }
        public AdminUser AdminUser { get; set; }
    }

    public class ConfigObject
	{
        public Users Users { get; set; }
		public string Url { get; set; }
        public string Echo { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string Language { get; set; }
	}
}