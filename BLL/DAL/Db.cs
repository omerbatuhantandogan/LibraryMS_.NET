using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;

namespace BLL.DAL
{
    public class Db : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Major> Majores { get; set; }
        public DbSet<TeacherStudent> TeacherStudents { get; set; }


        public Db(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "server=localhost;database=SchoolDB;user=root;",
                    new MySqlServerVersion(new Version(8, 0, 30))
                );
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    if (property.ClrType == typeof(string))
                    {
                        var maxLength = property.GetMaxLength();
                        property.SetColumnType(maxLength != null ? $"VARCHAR({maxLength})" : "TEXT");
                    }
                    if (property.ClrType == typeof(bool))
                    {
                        property.SetColumnType("TINYINT(1)");
                    }
                }
            }
        }


    }
}
