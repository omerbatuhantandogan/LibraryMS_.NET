using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Role
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Role name is required!")]
        [StringLength(10, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
