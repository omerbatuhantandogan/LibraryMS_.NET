using BLL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class TeacherModel
    {
        public Teacher Record { get; set; }
        public string Name => Record.Name;
        public string Surname => Record.Surname;
        public string NameAndSurname => Record.Name+ " " + Record.Surname;
        public string Major => Record.Major?.Name;

        public string Students => string.Join("<br>", Record.TeacherStudents?.Select(dp => dp.Student?.Name + " " + dp.Student?.Surname));

    }
}
