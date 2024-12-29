using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{

    public interface IUserService
    {
        public IQueryable<UserModel> Query();

        public ServiceBase Create(User record);
        public ServiceBase Update(User record);
        public ServiceBase Delete(int id);


    }
    public class UserService : ServiceBase, IUserService
    {
        public UserService(Db db) : base(db)
        {
        }

        public ServiceBase Create(User record)
        {
            if (_db.Users.Any(u => u.UserName.ToUpper() == record.UserName.ToUpper().Trim()))
                return Error("User already exists");

            record.UserName = record.UserName?.Trim();
            _db.Users.Add(record);
            _db.SaveChanges();
            return Success("User created successfully");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Users.SingleOrDefault(u => u.Id == id);
            entity.isActive = false;
            var result = Update(entity);
            _db.SaveChanges();
            return Success($"User with \"{entity.UserName}\" deleted successfully.");

        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users.Include(u =>u.Role).OrderByDescending(u => u.isActive).ThenBy(u => u.UserName).Select(u => new UserModel { Record = u });
        }

        public ServiceBase Update(User record)
        {
            if (_db.Users.Any(u => u.Id != record.Id && u.UserName.ToUpper() == record.UserName.ToUpper().Trim()))
                return Error("User already exists");

            var entity = _db.Users.SingleOrDefault(u => u.Id == record.Id);
            if (entity is null)
                return Error("User not found");

            entity.UserName = record.UserName?.Trim();
            entity.Password = record.Password?.Trim();
            entity.isActive = record.isActive;
            entity.RoleId = record.RoleId;
            _db.Users.Update(entity);
            _db.SaveChanges();
            return Success("User updated successfully");

        }
    }
}
