using Microsoft.EntityFrameworkCore;

namespace BLL.DAL
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
