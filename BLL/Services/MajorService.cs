using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    //public interface IMajorService
    //{
    //    public IQueryable<MajorModel> Query();
    //    public ServiceBase Create(Major record);
    //    public ServiceBase Update(Major record);
    //    public ServiceBase Delete(int id);
    //}
    public class MajorService : ServiceBase, IService<Major, MajorModel>
    {
        public MajorService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Major record)
        {
            if (_db.Majores.Any(b => b.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Major already exists");
            record.Name = record.Name?.Trim();
            _db.Majores.Add(record);
            _db.SaveChanges();
            return Success("Major created successfully.");
        }

        public ServiceBase Delete(int Id)
        {
            var entity = _db.Majores.Include(b => b.Teachers).SingleOrDefault(b => b.Id == Id);
            if (entity is null)
                return Error("Major can't be found!");
            _db.Majores.Remove(entity);
            _db.SaveChanges();
            return Success("Major deleted successfully.");
        }

        public IQueryable<MajorModel> Query()
        {
         return _db.Majores.OrderBy(b => b.Name).Select(b => new MajorModel() { Record = b });

        }

        public ServiceBase Update(Major record)
        {
            if (_db.Majores.Any(b => b.Id != record.Id && b.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Major with the same name exists!");

            var entity = _db.Majores.SingleOrDefault(b => b.Id == record.Id);
            if (entity is null)
                return Error("Major can't be found!");
            entity.Name = record.Name?.Trim();
            _db.Majores.Update(entity);
            _db.SaveChanges(); // commit to the database
            return Success("Major updated successfully.");
        }
    }
}
