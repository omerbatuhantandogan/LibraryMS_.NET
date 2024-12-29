using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Teacher name is required!")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required!")]
        [StringLength(20, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Surname { get; set; }
        public int MajorId { get; set; }
        public Major Major { get; set; }
        public List<TeacherStudent> TeacherStudents { get; set; } = new List<TeacherStudent>();

    }
}
