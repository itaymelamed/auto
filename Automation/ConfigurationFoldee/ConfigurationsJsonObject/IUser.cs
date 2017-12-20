using System;
namespace Automation.ConfigurationFoldee.ConfigurationsJsonObject
{
    public interface IUser
    {
        string UserName { get; set; }
        string Password { get; set; }
    }
}
