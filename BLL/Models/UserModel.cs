using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class UserModel
    {
        public User Record { get; set; }

        [DisplayName("User Name")]
        public string UserName => Record.UserName;

        [DisplayName("Password")]
        public string Password => Record.Password;

        [DisplayName("is Active?")]
        public string isActive => Record.isActive ? "Active" : "Inactive";

        [DisplayName("Role Name")]
        public string Role => Record.Role?.Name;
    }
}
