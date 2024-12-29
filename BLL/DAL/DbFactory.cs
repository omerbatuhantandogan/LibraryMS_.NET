using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace BLL.DAL
{
    public class DbFactory : IDesignTimeDbContextFactory<Db>
    {
        public Db CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();
            optionsBuilder.UseMySql(
                "server=localhost;database=SchoolDB;user=root;",
                new MySqlServerVersion(new Version(8, 0, 30))
            );

            return new Db(optionsBuilder.Options);
        }
    }
}