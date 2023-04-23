using JellyFish.Areas.Identity.Data;
using JellyFish.IService;
using JellyFish.Models;
using JellyFish.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JellyFish.Controllers
{
	public class NotificationsController : Controller
	{
        private readonly JellyFishDbContext _context;
        private readonly UserManager<JellyFishUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(JellyFishDbContext context, UserManager<JellyFishUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _context = context;
            _unitOfWork = unitOfWork;
        }
		
        INotiService _notiService = null;
		List<Noti> _oNotifications = new List<Noti>();

		public NotificationsController(INotiService notiService)
		{
			_notiService = notiService;
		}

		public IActionResult AllNotifications()
		{
			return View();
		}

		public JsonResult GetNotifications(bool bIsGetOnlyUnread = false)
		{
            var sendAcceptMsg = _context.Employers.Where(x => x.Title == "True").FirstOrDefault();
            string nToUserId = "8bd71642-e5e3-4ab5-8498-d4c1883003d7";
			_oNotifications = new List<Noti>();
			_oNotifications = _notiService.GetNotifications(nToUserId, bIsGetOnlyUnread);
			return Json(_oNotifications);
		}
	}
}
