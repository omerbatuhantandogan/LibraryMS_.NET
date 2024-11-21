using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IBookService
    {
        public IQueryable<BookModel> Query();

        public ServiceBase Create(Book record);
        public ServiceBase Update(Book record);
        public ServiceBase Delete(int id);
    }

    public class BookService : ServiceBase, IBookService
    {
        public BookService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Book record)
        {
            if (_db.Books.Any(b => b.BookName.ToUpper() == record.BookName.ToUpper().Trim()))
                return Error("Book already exists");

            record.BookName = record.BookName?.Trim();
            _db.Books.Add(record);
            _db.SaveChanges();
            return Success("Book created successfully");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Books.SingleOrDefault(b => b.Id == id);
            if (entity == null)
                return Error("Book not found");

            _db.Books.Remove(entity);
            _db.SaveChanges();
            return Success($"Book \"{entity.BookName}\" deleted successfully.");
        }

        public IQueryable<BookModel> Query()
        {
            return _db.Books.Include(b => b.Student).OrderBy(b => b.BookName).Select(b => new BookModel { Record = b });
        }

        public ServiceBase Update(Book record)
        {
            if (_db.Books.Any(b => b.Id != record.Id && b.BookName.ToUpper() == record.BookName.ToUpper().Trim()))
                return Error("Book already exists");

            var entity = _db.Books.SingleOrDefault(b => b.Id == record.Id);
            if (entity == null)
                return Error("Book not found");

            entity.BookName = record.BookName?.Trim();
            entity.StudentId = record.StudentId;
            _db.Books.Update(entity);
            _db.SaveChanges();
            return Success("Book updated successfully");
        }
    }
}
