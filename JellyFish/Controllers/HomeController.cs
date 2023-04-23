using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using JellyFish.Models;
using JellyFish.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using JellyFish.Models.View_Models;
using System.Security.Claims;

namespace JellyFish.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly UserManager<JellyFishUser> _userManager;
		private readonly SignInManager<JellyFishUser> _signInManager;
		private readonly Models.JellyFishDbContext _context;
		private IWebHostEnvironment _webHostEnvironment;


		public HomeController(ILogger<HomeController> logger, JellyFishDbContext context, UserManager<JellyFishUser> userManager, SignInManager<JellyFishUser> signInManager, IWebHostEnvironment webHostEnvironment)
		{
			_logger = logger;
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
			_webHostEnvironment = webHostEnvironment;

		}

		public IActionResult Index(string SelectOption, string SearchString)
		{

			if (User.IsInRole("JobSeeker"))
			{
				return RedirectToAction("Index", "Jobs");
			}

			if (User.IsInRole("Employer"))
			{
				return RedirectToAction("Index", "Jobs");
			}



			if (User.IsInRole("Administrator"))
			{

				ViewData["CurrentFilter"] = SearchString;
				var jobPosting = from b in _context.Jobs.Include(x => x.Employer).ThenInclude(x => x.Company) where b.IsActive == false select b;
				if (!String.IsNullOrEmpty(SearchString))
				{
					switch (SelectOption)
					{
						case "Title":
							jobPosting = jobPosting.Where(b => b.Title.ToLower().Contains(SearchString.ToLower()));
							break;
						case "Company":
							jobPosting = jobPosting.Where(b => b.Employer.Company.Name.ToLower().Contains(SearchString.ToLower()));
							break;
						case "Location":
							jobPosting = jobPosting.Where(b => b.Location.ToLower().Contains(SearchString.ToLower()));
							break;
						case "Salary":
							jobPosting = jobPosting.Where(b => b.Salary == Convert.ToDecimal(SearchString));
							break;
					}
				}

				return View("Admin", jobPosting.ToList());
			}
			return View();

		}


		[HttpPost]
		public IActionResult AcceptJob(int id)
		{
			string checkAccept = HttpContext.Request.Form["checkAccept"].ToString();

			var acceptJobs = _context.Jobs.Where(x => x.JobId == id).FirstOrDefault();


			if (checkAccept == "Accept")
			{
				acceptJobs.IsActive = true;


				_context.Update(acceptJobs);
				_context.SaveChanges();
				TempData["success"] = "It has been accepted successfully!";
			}
			else if (checkAccept == "Decline")
			{
				_context.Jobs.Remove(acceptJobs);
				_context.SaveChanges();
				TempData["success"] = "It has been declined successfully!";
			}

			return RedirectToAction("Index");
		}

		public IActionResult ActiveJob(string SelectOption, string SearchString)
		{
			ViewData["CurrentFilter"] = SearchString;
			var jobPosting = from b in _context.Jobs.Include(x => x.Employer).ThenInclude(x => x.Company) where b.IsActive == true select b;


			if (!String.IsNullOrEmpty(SearchString))
			{
				switch (SelectOption)
				{
					case "Title":
						jobPosting = jobPosting.Where(b => b.Title.ToLower().Contains(SearchString.ToLower()));
						break;
					case "Company":
						jobPosting = jobPosting.Where(b => b.Employer.Company.Name.ToLower().Contains(SearchString.ToLower()));
						break;
					case "Location":
						jobPosting = jobPosting.Where(b => b.Location.ToLower().Contains(SearchString.ToLower()));
						break;
					case "Salary":
						jobPosting = jobPosting.Where(b => b.Salary == Convert.ToDecimal(SearchString));
						break;
				}
			}

			return View("ActiveJobPosting", jobPosting.ToList());
		}

		[HttpPost]
		public IActionResult DeclineActiveJobposting(int id)
		{
			string checkAccept = HttpContext.Request.Form["checkAccept"].ToString();
			var declineJobs = _context.Jobs.Where(x => x.JobId == id).FirstOrDefault();

			if (checkAccept == "Decline")
			{
				_context.Jobs.Remove(declineJobs);
				_context.SaveChanges();
				TempData["success"] = "It has been declined successfully!";
			}
			return RedirectToAction("ActiveJob");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}