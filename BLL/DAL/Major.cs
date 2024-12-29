using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Major
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Major name is required!")]
        [StringLength(20, ErrorMessage ="Major name must be maximum {1} characters!")]
        public string Name { get; set; }

        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
