using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class StudentService : ServiceBase, IService<Student,StudentModel>
    {
     
        public StudentService(Db db) : base(db)
        {
        }


        public IQueryable<StudentModel> Query()
        {
            return _db.Students.Include(p => p.TeacherStudents).ThenInclude(dp => dp.Teacher).OrderByDescending(p => p.Id).Select(p => new StudentModel() { Record = p });
        }
        public ServiceBase Create(Student record)
        {
            if (_db.Students.Any(p=> p.Name.ToLower() == record.Name.ToLower().Trim() && p.Surname.ToLower() == record.Surname.ToLower().Trim() && p.BirthDate == record.BirthDate))
                return Error("Student with the same name, surname, birth date exists!");
            record.Name = record.Name?.Trim();
            _db.Students.Add(record);
            _db.SaveChanges();
            return Success("Student created successfully");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Students.Include(p => p.TeacherStudents).SingleOrDefault(p => p.Id == id);
            if (entity is null)
                return Error("Student can't be found!");
            _db.TeacherStudents.RemoveRange(entity.TeacherStudents);
            _db.Students.Remove(entity);
            _db.SaveChanges();
            return Success("Student deleted successfully.");
        }

    
        public ServiceBase Update(Student record)
        {
            if (_db.Students.Any(p => p.Id != record.Id && p.Name.ToLower() == record.Name.ToLower().Trim() &&
                p.Surname.ToLower() == record.Surname.ToLower().Trim() && p.BirthDate == record.BirthDate))
                return Error("Student with the same name, surname, and birth date exists!");
            var entity = _db.Students.Include(p => p.TeacherStudents).SingleOrDefault(p => p.Id == record.Id);
            if (entity is null)
                return Error("Student not found!");
            _db.TeacherStudents.RemoveRange(entity.TeacherStudents);
            entity.Name = record.Name?.Trim();
            entity.Surname = record.Surname?.Trim();
            entity.BirthDate = record.BirthDate;
            entity.Height = record.Height;
            entity.Weight = record.Weight;
            //entity.SpeciesId = record.SpeciesId;
            entity.TeacherStudents = record.TeacherStudents;
            _db.Students.Update(entity);
            _db.SaveChanges();
            return Success("Student updated successfully.");


        }
    }
}
