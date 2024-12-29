using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.Services.Bases;
using BLL.DAL;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class MajoresController : MvcController
    {
        // Service injections:
        private readonly IService<Major,MajorModel> _MajorService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public MajoresController(
            IService<Major, MajorModel> MajorService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _MajorService = MajorService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Majores
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _MajorService.Query().ToList();
            return View(list);
        }

        // GET: Majores/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _MajorService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Majores/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Majores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MajorModel Major)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _MajorService.Create(Major.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = Major.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(Major);
        }

        // GET: Majores/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _MajorService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Majores/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MajorModel Major)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _MajorService.Update(Major.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = Major.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(Major);
        }

        // GET: Majores/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _MajorService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Majores/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _MajorService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
