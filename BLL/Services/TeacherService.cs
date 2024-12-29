using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class TeacherService : ServiceBase, IService<Teacher, TeacherModel>
    {
        public TeacherService(Db db) : base(db)
        {
        }

        public IQueryable<TeacherModel> Query()
        {
            return _db.Teachers.Include(d => d.Major).Include(d=> d.TeacherStudents).ThenInclude(dp=>dp.Student).Select(d=> new TeacherModel() { Record = d});
        }

        public ServiceBase Create(Teacher record)
        {
            if (_db.Teachers.Any(d => d.Name.ToLower() == record.Name.ToLower().Trim() && d.Surname.ToLower() == record.Surname.ToLower()))
               return Error("Teacher with the same name and surname exists!");
            record.Name = record.Name?.Trim();
            _db.Teachers.Add(record);
            _db.SaveChanges();
            return Success("Teacher created successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Teachers.Include(p => p.TeacherStudents).SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return Error("Teacher can't be found!");
            _db.TeacherStudents.RemoveRange(entity.TeacherStudents);
            _db.Teachers.Remove(entity);
            _db.SaveChanges();
            return Success("Teacher deleted successfully.");
        }

       

        public ServiceBase Update(Teacher record)
        {
            if (_db.Teachers.Any(d => d.Name.ToLower() == record.Name.ToLower().Trim() && d.Surname.ToLower() == record.Surname.ToLower() && d.MajorId == record.MajorId))
                return Error("Teacher with the same properties exists!");
            var entity = _db.Teachers.Include(d => d.TeacherStudents).SingleOrDefault(p => p.Id == record.Id);
            if (entity is null)
                return Error("Teacher not found!");
            _db.TeacherStudents.RemoveRange(entity.TeacherStudents);
            entity.Name = record.Name.Trim();
            entity.Surname = record.Surname.Trim();
          
            entity.MajorId = record.MajorId;
            entity.TeacherStudents = record.TeacherStudents;
            _db.Teachers.Update(entity);
            _db.SaveChanges();
            return Success("Teacher updated successfully.");
        }
    }
}
