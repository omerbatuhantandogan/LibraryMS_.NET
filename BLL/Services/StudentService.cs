using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IStudentService
    {
        public IQueryable<StudentModel> Query();

        public ServiceBase Create(Student record);
        public ServiceBase Update(Student record);
        public ServiceBase Delete(int id);
    }

    public class StudentService : ServiceBase, IStudentService
    {
        public StudentService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Student record)
        {
            if (_db.Students.Any(s => s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Student already exists");

            record.Name = record.Name?.Trim();
            _db.Students.Add(record);
            _db.SaveChanges();
            return Success("Student created successfully");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Students.Include(s => s.Books).SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return Error("Student not found");
            if (entity.Books.Any())
                return Error("Student has assigned books");

            _db.Students.Remove(entity);
            _db.SaveChanges();
            return Success("Student deleted successfully");
        }

        public IQueryable<StudentModel> Query()
        {
            return _db.Students.OrderBy(s => s.Name).Select(s => new StudentModel { Record = s });
        }

        public ServiceBase Update(Student record)
        {
            if (_db.Students.Any(s => s.Id != record.Id && s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Student already exists");

            var entity = _db.Students.SingleOrDefault(s => s.Id == record.Id);
            if (entity is null)
                return Error("Student not found");

            entity.Name = record.Name?.Trim();
            _db.Students.Update(entity);
            _db.SaveChanges();
            return Success("Student updated successfully");
        }
    }
}
