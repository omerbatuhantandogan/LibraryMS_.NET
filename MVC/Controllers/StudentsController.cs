using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;

namespace MVC.Controllers
{
    public class StudentsController : MvcController
    {
        // Service injections:
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: Students
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _studentService.Query().ToList();
            return View(list);
        }

        // GET: Students/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _studentService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (if required, e.g., for dropdowns):
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentModel student)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _studentService.Create(student.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = student.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(student);
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _studentService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Students/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StudentModel student)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _studentService.Update(student.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = student.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(student);
        }

        // GET: Students/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _studentService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Students/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _studentService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
