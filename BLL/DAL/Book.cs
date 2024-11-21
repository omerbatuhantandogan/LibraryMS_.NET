using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        public string BookName { get; set; }

        public int? StudentId { get; set; }

        public Student Student { get; set; }
    }
}
