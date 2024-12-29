using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{

    public interface IRoleService
    {
        public IQueryable<RoleModel> Query();

        public ServiceBase Create(Role record);
        public ServiceBase Update(Role record);
        public ServiceBase Delete(int id);
    }
    public class RoleService : ServiceBase, IRoleService
    {
        public RoleService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Role record)
        {
            if (_db.Roles.Any(u => u.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Role already exists");

            record.Name = record.Name?.Trim();
            _db.Roles.Add(record);
            _db.SaveChanges();
            return Success("Role created successfully");
        }

        public ServiceBase Delete(int id)
        {  
            var entity= _db.Roles.Include(r => r.Users).SingleOrDefault(u => u.Id == id);
            if (entity is null)
                return Error("Role not found");
            if (entity.Users.Any())
                return Error("Role has users");

            _db.Roles.Remove(entity);
            _db.SaveChanges();
            return Success("Role deleted successfully");

        }

        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.OrderBy(u => u.Name).Select(u => new RoleModel { Record = u });
        }

        public ServiceBase Update(Role record)
        {
            if (_db.Roles.Any(u => u.Id != record.Id && u.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Role already exists");

            var entity = _db.Roles.SingleOrDefault(u => u.Id == record.Id);
            if (entity is null)
                return Error("Role not found");

            entity.Name = record.Name?.Trim();
            _db.Roles.Update(entity);
            _db.SaveChanges();
            return Success("Role updated successfully");
        }
    }
}
