using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class BookModel
    {
        public Book Record { get; set; }

        [DisplayName("Book Name")]
        public string BookName => Record.BookName;

        [DisplayName("Belongs to Student")]
        public string StudentName => Record.Student?.Name ?? "No Student Assigned";
    }

}
