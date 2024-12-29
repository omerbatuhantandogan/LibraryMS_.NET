using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "User name is required!")]
        [StringLength(20, ErrorMessage ="User name must be maximum {1} characters!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [StringLength(12, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Password { get; set; }

        public bool isActive { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
