using BLL.DAL;

namespace BLL.Models
{
    public class StudentModel
    {
        public Student Record { get; set; }
        public string Name => Record.Name;
    }
}
