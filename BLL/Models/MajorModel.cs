using BLL.DAL;

namespace BLL.Models
{
    public class MajorModel
    {
        public Major Record { get; set; }
        public string Name => Record.Name;
    }
}
