using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Experimental.ProjectCache;

namespace MVC.Controllers
{
    [Authorize]
    public class FavoritesController : MvcController
    {
        const string SESSIONKEY = "Favorites";

        private readonly HttpServiceBase _httpService;
        private readonly IService<Teacher,TeacherModel> _TeacherService;

        public FavoritesController(HttpServiceBase httpServiceBase, IService<Teacher,TeacherModel> TeacherService)
        {
            _httpService = httpServiceBase;
            _TeacherService = TeacherService;
        }

        private int GetUserId() => Convert.ToInt32(User.Claims.SingleOrDefault(c=>c.Type == "Id").Value);
        private List<FavoritesModel> GetSession(int userId)
        {
       
            var favorites = _httpService.GetSession<List<FavoritesModel>>(SESSIONKEY);
            return favorites?.Where(f => f.UserId == userId).ToList();
        }
        public IActionResult Get()
        {
            return View("List",GetSession(GetUserId()));
        }

        public IActionResult Remove(int TeacherId)
        {
            var favorites = GetSession(GetUserId());
            var favoritesItem = favorites.FirstOrDefault(c => c.TeacherId == TeacherId);
            favorites.Remove(favoritesItem);
            _httpService.SetSession(SESSIONKEY, favorites);
            return RedirectToAction(nameof(Get));
        }

        // GET: /Favorites/Add?TeacherId=17
        public IActionResult Add(int TeacherId)
        {
            int userId = GetUserId();
            var favorites = GetSession(userId);
            favorites = favorites ?? new List<FavoritesModel>();
    
            if (!favorites.Any(f => f.TeacherId == TeacherId))
            {
                var Teacher = _TeacherService.Query().SingleOrDefault(p => p.Record.Id == TeacherId);
                var favoritesItem = new FavoritesModel()
                {
                    TeacherId = TeacherId,
                    UserId = userId,
                    TeacherName = Teacher.Name
                };
                favorites.Add(favoritesItem);
                _httpService.SetSession(SESSIONKEY, favorites);
                TempData["Message"] = $"\"{Teacher.Name}\" added to favorites.";
            }
            return RedirectToAction("Index", "Teachers");
        }

    }
}
