using Automation.ConfigurationFolder.ConfigurationsObjectFolder;

namespace Automation.ConfigurationFolder.ConfigurationsObjectFolder
{
    public class Rep : IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Admin : IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SuperViser : IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Consumer : IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AmsUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Agency { get; set; }
    }

    public class Users
    {
        public Rep Rep { get; set; }
        public Admin Admin { get; set; }
        public SuperViser SuperViser { get; set; }
        public Consumer Consumer { get; set; }
        public AmsUser AmsUser { get; set; }
    }

    public class Url
    {
        public string Desktop { get; set; }
        public string AMS { get; set; }
    }

    public class ConfigurationsObject
    {
        public Users Users { get; set; }
        public Url Url { get; set; }
        public string DBConnectionString { get; set; }
        public string SDK { get; set; }
    }
}
