using System;
using System.Collections.Generic;

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

    public class Apis
    {
        public string Feed { get; set; }
        public string Ctegory { get; set; }
    }

    public class ConfigObject
	{
        public Apis Apis { get; set; }
        public Users Users { get; set; }
		public string Url { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
	}
}