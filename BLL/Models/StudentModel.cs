using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class StudentModel
    {
        public Student Record { get; set; }
        public string Name => Record.Name;
        public string Surname => Record.Surname;
        
        [DisplayName("Birth Date")]
        public string BirthDate => Record.BirthDate is null ? string.Empty : Record.BirthDate.Value.ToString("MM/dd/yyyy");
        public string Height => Record.Height.HasValue ? Record.Height.Value.ToString("N2") : string.Empty;
        public string Weight => (Record.Weight ?? 0).ToString("N1");

        public string Teachers => string.Join("<br>",Record.TeacherStudents.Select(dp => dp.Teacher?.Name + " " + dp.Teacher?.Surname));

        [DisplayName("Teacher(s)")]
        public List<int> TeacherIds
        {
            get => Record.TeacherStudents?.Select(dp=>dp.TeacherId).ToList();
            set => Record.TeacherStudents = value.Select(v => new TeacherStudent() { TeacherId = v }).ToList();
        }
         
    } 
}
