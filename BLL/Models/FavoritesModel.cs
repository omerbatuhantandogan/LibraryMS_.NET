using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class FavoritesModel
    {
        public int  TeacherId { get; set; }
        public int UserId { get; set; }
        public string TeacherName { get; set; }
    }
}
