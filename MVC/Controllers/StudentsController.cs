using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.DAL;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;

// Generated from Custom Template.

namespace MVC.Controllers
{
    [Authorize]
    public class StudentsController : MvcController
    {
        // Service injections:
        private readonly IService<Student, StudentModel> _StudentService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entity name in the controller and views. */
        private readonly IService<Teacher,TeacherModel> _TeacherService;

        public StudentsController(
            IService<Student, StudentModel> StudentService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            , IService<Teacher, TeacherModel> TeacherService

        )
        {
            _StudentService = StudentService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            _TeacherService = TeacherService;
        }

        // GET: Students
        [AllowAnonymous]
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _StudentService.Query().ToList();
            return View(list);
        }

        // GET: Students/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _StudentService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            ViewBag.TeacherIds = new MultiSelectList(_TeacherService.Query().ToList(), "Record.Id", "NameAndSurname");
        }

        // GET: Students/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public IActionResult Create(StudentModel Student)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _StudentService.Create(Student.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = Student.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(Student);
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _StudentService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Students/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(StudentModel Student)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _StudentService.Update(Student.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = Student.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(Student);
        }

        // GET: Students/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _StudentService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        [Authorize(Roles = "Admin")]
        // POST: Students/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _StudentService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
