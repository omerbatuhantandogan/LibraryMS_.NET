using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.DAL;
using BLL.Services.Bases;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class TeachersController : MvcController
    {
        // Service injections:
        private readonly IService<Teacher,TeacherModel> _TeacherService;
        private readonly IService<Major,MajorModel> _MajorService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public TeachersController(
            IService<Teacher, TeacherModel> TeacherService
            , IService<Major, MajorModel> MajorService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _TeacherService = TeacherService;
            _MajorService = MajorService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Teachers
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _TeacherService.Query().ToList();
            return View(list);
        }

        // GET: Teachers/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _TeacherService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["MajorId"] = new SelectList(_MajorService.Query().ToList(), "Record.Id", "Name");
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Teachers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TeacherModel Teacher)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _TeacherService.Create(Teacher.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = Teacher.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(Teacher);
        }

        // GET: Teachers/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _TeacherService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Teachers/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TeacherModel Teacher)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _TeacherService.Update(Teacher.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = Teacher.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(Teacher);
        }

        // GET: Teachers/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _TeacherService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Teachers/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _TeacherService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
