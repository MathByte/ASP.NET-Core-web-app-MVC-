using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JellyFish.Models;
using JellyFish.Models.View_Models;
using Microsoft.AspNetCore.Identity;
using JellyFish.Areas.Identity.Data;
using JellyFish.Repository;
using JellyFish.Repository.IRepository;
using System.Security.Claims;
using System.Collections;

namespace JellyFish.Controllers
{
	public class JobsController : Controller
	{
		private readonly JellyFishDbContext _context;
		private readonly UserManager<JellyFishUser> _userManager;
		private readonly IUnitOfWork _unitOfWork;

		public JobsController(JellyFishDbContext context, UserManager<JellyFishUser> userManager, IUnitOfWork unitOfWork)
		{
			_userManager = userManager;
			_context = context;
			_unitOfWork = unitOfWork;
		}



		// GET: Jobs
		public async Task<IActionResult> Index(string? searchQuery)
		{
			ViewData["searchQuery"] = searchQuery;




			if (User.IsInRole("JobSeeker"))
			{

				var user = await _userManager.GetUserAsync(User);
				var applicantJobs = _context.Jobs.Include(x => x.Category).Include(x => x.JobType).Include(x => x.Level).Include(x => x.Employer.Company).Where(x=> x.IsActive == true && x.IsOpen == true).ToList();


				if (!String.IsNullOrEmpty(searchQuery))
				{
					applicantJobs = applicantJobs.Where(x => x.Title.Contains(searchQuery)).ToList();
				}


				var jobViewModel = GetViewModel(applicantJobs, null, null, null, null);

				return View("Index_Appl", jobViewModel);
				//return View( "Index_Appl");
			}




			if (User.IsInRole("Employer"))
			{


				if (!String.IsNullOrEmpty(searchQuery))
				{
					if (searchQuery.Equals("inact"))
					{
						var jobsx = _context.Jobs.Where(r => r.IsActive == false).Include(x => x.Category);

						// For counting applicants' number on job posting for employer

						try
						{
							var jobList = _context.Jobs.Where(r => r.IsActive == false).Select(x => x.JobId).ToList();
							var applicantList = _context.Applicants.Select(x => x.JobId).ToList();
							List<string> applicantCountArray = new List<string>();
							int count = 0;


							for (int i = 0; i < jobList.Count; i++)
							{
								for (int j = 0; j < applicantList.Count; j++)
								{
									if (applicantList[j] == jobList[i])
									{
										count++;
									}
								}

								applicantCountArray.Add(jobList[i] + " " + count);
								count = 0;
							}

							if (applicantCountArray.Count > 0)
							{

								ViewBag.ApplicantCountArray = applicantCountArray;
							}
							//else
							//{
							//    ViewBag.ApplicantCountArray = 0;
							//}




							return View("Index_Emp", jobsx.ToList());



						}
						catch (Exception ex)
						{
							//Response.Write("Property: " + ex.Message);
							return View();
						}

					}

					

					if (searchQuery.Equals("opend"))
					{
						var jobsz = _context.Jobs.Where(r => r.IsOpen == true).Include(x => x.Category);

						// For counting applicants' number on job posting for employer

						try
						{
							var jobList = _context.Jobs.Where(r => r.IsOpen == true).Select(x => x.JobId).ToList();
							var applicantList = _context.Applicants.Select(x => x.JobId).ToList();
							List<string> applicantCountArray = new List<string>();
							int count = 0;

							for (int i = 0; i < jobList.Count; i++)
							{
								for (int j = 0; j < applicantList.Count; j++)
								{
									if (applicantList[j] == jobList[i])
									{
										count++;
									}
								}

								applicantCountArray.Add(jobList[i] + " " + count);
								count = 0;
							}
							if (applicantCountArray.Count > 0)
							{

								ViewBag.ApplicantCountArray = applicantCountArray;
							}
							else
							{
								ViewBag.ApplicantCountArray = applicantCountArray;
							}

							return View("Index_Emp", jobsz.ToList());

						}
						catch (Exception ex)
						{
							//Response.Write("Property: " + ex.Message);
							return View();
						}

					}
					if (searchQuery.Equals("closed"))
					{
						var jobsz = _context.Jobs.Where(r => r.IsOpen == false).Include(x => x.Category);

						// For counting applicants' number on job posting for employer

						try
						{
							var jobList = _context.Jobs.Where(r => r.IsOpen == false).Select(x => x.JobId).ToList();
							var applicantList = _context.Applicants.Select(x => x.JobId).ToList();
							List<string> applicantCountArray = new List<string>();
							int count = 0;

							for (int i = 0; i < jobList.Count; i++)
							{
								for (int j = 0; j < applicantList.Count; j++)
								{
									if (applicantList[j] == jobList[i])
									{
										count++;
									}
								}

								applicantCountArray.Add(jobList[i] + " " + count);
								count = 0;
							}
							if (applicantCountArray.Count > 0)
							{

								ViewBag.ApplicantCountArray = applicantCountArray;
							}
							else
							{
								ViewBag.ApplicantCountArray = applicantCountArray;
							}

							return View("Index_Emp", jobsz.ToList());

						}
						catch (Exception ex)
						{
							//Response.Write("Property: " + ex.Message);
							return View();
						}

					}

					if (searchQuery.Equals("all"))
					{

						var jobsv = _context.Jobs.Include(x => x.Category);

						// For counting applicants' number on job posting for employer
						using (JellyFishDbContext context = new JellyFishDbContext())
						{
							try
							{
								Job job = new Job();
								var jobList = _context.Jobs.Select(x => x.JobId).ToList();
								var applicantList = _context.Applicants.Select(x => x.JobId).ToList();
								List<string> applicantCountArray = new List<string>();
								int count = 0;




								for (int i = 0; i < jobList.Count; i++)
								{
									for (int j = 0; j < applicantList.Count; j++)
									{
										if (applicantList[j] == jobList[i])
										{
											count++;
										}
									}



									applicantCountArray.Add(jobList[i] + " " + count);
									count = 0;
								}
								if (applicantCountArray.Count > 0)
								{

									ViewBag.ApplicantCountArray = applicantCountArray;
								}
								//else
								//{
								//    ViewBag.ApplicantCountArray = 0;
								//}




								return View("Index_Emp", jobsv.ToList());



							}
							catch (Exception ex)
							{
								//Response.Write("Property: " + ex.Message);
								return View();
							}
						}
					}
				}




				var jobs = _context.Jobs.Include(x => x.Category);

				// For counting applicants' number on job posting for employer
				using (JellyFishDbContext context = new JellyFishDbContext())
				{
					try
					{
						Job job = new Job();
						var jobList = _context.Jobs.Select(x => x.JobId).ToList();
						var applicantList = _context.Applicants.Select(x => x.JobId).ToList();
						List<string> applicantCountArray = new List<string>();
						int count = 0;




						for (int i = 0; i < jobList.Count; i++)
						{
							for (int j = 0; j < applicantList.Count; j++)
							{
								if (applicantList[j] == jobList[i])
								{
									count++;
								}
							}



							applicantCountArray.Add(jobList[i] + " " + count);
							count = 0;
						}


						ViewBag.ApplicantCountArray = applicantCountArray;






						return View("Index_Emp", jobs.ToList());



					}
					catch (Exception ex)
					{
						//Response.Write("Property: " + ex.Message);
						return View();
					}
				}

			}
			return View();
		}







		public async Task<IActionResult> AcceptDeclineApplicant(string? acceptA, string? A_ID, string? Job_ID)
		{

			var Applicant_List = _context.Applicants.Where(w => w.ApplicantId == Int32.Parse(A_ID)).ToList();
			Applicant Current_Aplicant = Applicant_List[0];

			if(acceptA.Equals("Accept"))
			{
				Current_Aplicant.IsAccepted = 1;
			}
			else
			{
				Current_Aplicant.IsAccepted = 2;
			}

			_context.Applicants.Update(Current_Aplicant);
			_context.SaveChanges();




		

			return RedirectToAction("DisplayApplicents","Jobs", new { jobID = Int32.Parse(Job_ID) });
		}


		public async Task<IActionResult> DisplayApplicents(string? jobID)
		{
			var user = _userManager.GetUserId(User);
			List<Job> _job = (List<Job>)_context.Jobs
				.Include(k => k.Level)
				.Include(k => k.Category)
				.Include(k => k.JobType)
				.Include(l => l.Employer)
				.ThenInclude(q => q.Company)
				.Include(w => w.Applicants)
				.ThenInclude(w => w.User)
				.ThenInclude(w => w.UserSkills)
				.ThenInclude(w => w.Skill)
				.Where(j => j.EmployerId == user.ToString())
				.Where(r => r.JobId == Int32.Parse(jobID)).ToList();

			return View("DisplayAppl", _job[0]);
		}






		public async Task<IActionResult> RadioSelect(string? searchQuery)
		{
			if (searchQuery.Equals("all"))
			{
				return RedirectToAction("Index", "Jobs", new { searchQuery = "all" });
			}

			


			if (searchQuery.Equals("inact"))
			{

				return RedirectToAction("Index", "Jobs", new { searchQuery = "inact" });
			}
			if (searchQuery.Equals("opend"))
			{

				return RedirectToAction("Index", "Jobs", new { searchQuery = "opend" });
			}
			if (searchQuery.Equals("closed"))
			{

				return RedirectToAction("Index", "Jobs", new { searchQuery = "closed" });
			}
			return View();
		}


		[HttpPost]
		public IActionResult ChangeStatus(int jobId, string statusValue)
		{
			if (ModelState.IsValid)
			{
				var updateJobs = _context.Jobs.Where(x => x.JobId == jobId).FirstOrDefault();

				if (statusValue == "Active")
				{
					updateJobs.IsOpen = true;

				}
				else
				{
					updateJobs.IsOpen = false;
				}
				_context.Update(updateJobs);
				_context.SaveChanges();

				return RedirectToAction("Index");

			}

			return View();
		}

		// GET: Jobs/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			var user = await _userManager.GetUserAsync(User);
			string userid = await _userManager.GetUserIdAsync(user);

			if (id == null || _context.Jobs == null)
			{
				return NotFound();
			}

			var job = await _context.Jobs.Include(j => j.Applicants).Include(x => x.Employer.Company).Include(x => x.JobType).Include(x => x.Level).Include(x => x.Category).FirstOrDefaultAsync(m => m.JobId == id);
			if (job == null)
			{
				return NotFound();
			}
			if (job.Applicants.Where(x => x.UserId == userid).FirstOrDefault() != null)
			{
				ViewBag.applied = job.Applicants.Where(x=> x.UserId == userid).FirstOrDefault().IsApplied;
				ViewBag.saved = job.Applicants.Where(x => x.UserId == userid).FirstOrDefault().IsSaved;
			}
			else
			{
				ViewBag.already = "Neither";

			}
            return View(job);
        }


    











    // GET: Jobs1/Create
    [HttpGet]
		public IActionResult Create()
		{

			var employerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			TempData["employerId"] = employerId;
			ViewBag.EmployerId = TempData["employerId"];


			JobPostingViewModel jobPostingViewModel = new()
			{
				job = new(),
				CategoryList = _unitOfWork.Category.GetAll().Select(
				u => new SelectListItem
				{
					Text = u.Name,
					Value = u.CategoryId.ToString()
				}),
				JobTypeList = _unitOfWork.JobType.GetAll().Select(
				u => new SelectListItem
				{
					Text = u.Name,
					Value = u.JobTypeId.ToString()
				}),
				LevelList = _unitOfWork.Level.GetAll().Select(
				u => new SelectListItem
				{
					Text = u.LevelName,
					Value = u.Id.ToString()
				})

			};

			return View(jobPostingViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Title,Salary,isActive, isOpen, CategoryId,JobTypeId,LevelId,EmployerId,Description, Location")] Job job)
		{
			if (ModelState.IsValid)
			{
				job.CreatedDate = DateTime.Today.Date;
				job.IsActive = false;
				job.IsOpen = false;
				_unitOfWork.Job.Add(job);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}

			JobPostingViewModel jobPostingViewModel = new()
			{
				job = job,
				CategoryList = _unitOfWork.Category.GetAll().Select(
				 u => new SelectListItem
				 {
					 Text = u.Name,
					 Value = u.CategoryId.ToString()
				 }),
				JobTypeList = _unitOfWork.JobType.GetAll().Select(
				 u => new SelectListItem
				 {
					 Text = u.Name,
					 Value = u.JobTypeId.ToString()
				 }),
				LevelList = _unitOfWork.Level.GetAll().Select(
				 u => new SelectListItem
				 {
					 Text = u.LevelName,
					 Value = u.Id.ToString()
				 })
			};
			//return RedirectToAction("Index");
			return View(jobPostingViewModel);
		}



		public IActionResult Edit(int? id)
		{


			ViewBag.CategoryList = new SelectList(_context.Categories.ToList(), "CategoryId", "Name");
			ViewBag.LevelList = new SelectList(_context.Levels.ToList(), "Id", "LevelName");
			ViewBag.JobType = new SelectList(_context.JobTypes.ToList(), "JobTypeId", "Name");



			Job job = _unitOfWork.Job.GetFirstOrDefault(u => u.JobId == id);
			return View(job);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit([Bind("JobId,Title,Salary,IsActive, IsOpen, CreatedDate, CategoryId,JobTypeId,LevelId,EmployerId,Description, Location")] Job job)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Job.Update(job);
				_unitOfWork.Save();

				return RedirectToAction("Index");
			}
			else
			{
				return View(job);
			}


		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var jobFromDbFirst = _unitOfWork.Job.GetFirstOrDefault(u => u.JobId == id);
			int categoryId = _unitOfWork.Job.GetFirstOrDefault(u => u.JobId == id).CategoryId;
			int jobTypeId = _unitOfWork.Job.GetFirstOrDefault(u => u.JobId == id).JobTypeId;
			int levelId = _unitOfWork.Job.GetFirstOrDefault(u => u.JobId == id).LevelId;

			JobPostingViewModel jobPostingViewModel = new()
			{
				job = _unitOfWork.Job.GetFirstOrDefault(u => u.JobId == id),
				category = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == categoryId),
				jobType = _unitOfWork.JobType.GetFirstOrDefault(u => u.JobTypeId == jobTypeId),
				level = _unitOfWork.Level.GetFirstOrDefault(u => u.Id == levelId)

			};

			if (jobFromDbFirst == null)
			{
				return NotFound();
			}
			return View(jobPostingViewModel);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePost(int? id)
		{
			var obj = _unitOfWork.Job.GetFirstOrDefault(u => u.JobId == id);
			if (obj == null)
			{
				return NotFound();
			}

			var applicantForJob = _context.Applicants.Where(x=> x.JobId == id).ToList();

			_context.Applicants.RemoveRange(applicantForJob);
			_context.SaveChanges();

			_unitOfWork.Job.Remove(obj);
			_unitOfWork.Save();
			//TempData["success"] = "It's been deleted successfully";
			return RedirectToAction("Index");
		}

		private bool JobExists(int id)
		{
			return (_context.Jobs?.Any(e => e.JobId == id)).GetValueOrDefault();
		}


		[HttpGet]
		public async Task<IActionResult> MyJobs()
		{
			var user = _userManager.GetUserId(User);
			
			
			
			

			var applicant = _context.Applicants.Where(x => x.UserId == user).Select(x=> x.JobId).ToList();

			if(applicant != null)
			{

                var applicantJobs = _context.Jobs
                .Include(x => x.Applicants)
                .ThenInclude(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.JobType)
                .Include(x => x.Level)
                .Include(x => x.Employer.Company)
				.Where(x=> applicant.Contains(x.JobId))
                .ToList();
                return View(applicantJobs);

            }


            //         var myjobs = await _context.Applicants
            //             .Include(a => a.User)
            //         .Include(a => a.Job)
            //.ThenInclude(x=> x.Employer)
            //.ThenInclude(x => x.Company)
            //.Where(x => x.UserId == user)
            //.ToListAsync();

            return View();
		}

		[HttpGet]
		public  IActionResult MyJobsFiltered(string appliedJobs)
		{

            var user = _userManager.GetUserId(User);
			List<int> applicant;


            if (appliedJobs == "Applied")
			{
                applicant = _context.Applicants.Where(x => x.UserId == user && x.IsApplied == true).Select(x => x.JobId).ToList();
            }
			else if(appliedJobs == "Selected")
			{
                applicant = _context.Applicants.Where(x => x.UserId == user && x.IsAccepted == 1).Select(x => x.JobId).ToList();
            }
			else if (appliedJobs == "Saved")
			{
                applicant = _context.Applicants.Where(x => x.UserId == user && x.IsSaved == true).Select(x => x.JobId).ToList();
            }
			else
			{
                applicant = _context.Applicants.Where(x => x.UserId == user).Select(x => x.JobId).ToList();
            }


            

            if (applicant != null)
            {

                var applicantJobs = _context.Jobs
                .Include(x => x.Applicants)
                .ThenInclude(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.JobType)
                .Include(x => x.Level)
                .Include(x => x.Employer.Company)
                .Where(x => applicant.Contains(x.JobId))
                .ToList();
                return PartialView("_MyJobsPartial", applicantJobs);

            }
            return View();
        }



            [HttpPost]
		public IActionResult FilterJobType(JobViewModel types)
		{


			//var user =  _userManager.GetUserAsync(User);
			//var applicantJobs = _context.Jobs.Include(x => x.Category).Include(x => x.JobType).Include(x => x.Level).Include(x => x.Employer.Company).ToList();


			//var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "\\images\\", "amazon.png");

			bool remote = false;

			if(types.IsRemote == "Yes")
			{
                 remote = true;
            }
			else if(types.IsRemote == "No")
			{
				 remote = false;
			}
			
			



			List<int> allJobTypes = _context.Jobs.Select(x => x.JobTypeId).Distinct().ToList();
			List<int> allCatTypes = _context.Jobs.Select(x => x.CategoryId).Distinct().ToList();
			List<int> allLevelTypes = _context.Jobs.Select(x => x.LevelId).Distinct().ToList();
			var jobFilter = types.JobTypeFilterId == null ? allJobTypes : new List<int>()
			{

				types.JobTypeFilterId ?? 0
			};

			var jobFilterCat = types.CategoryFilterId == null ? allCatTypes : new List<int>()
			{

				types.CategoryFilterId ?? 0
			};

			var jobFilterLev = types.LevelFilterId == null ? allLevelTypes : new List<int>()
			{

				types.LevelFilterId ?? 0
			};
			var jobFiltered = _context.Jobs.Include(x => x.Category).Include(x => x.JobType).Include(x => x.Level).Include(x => x.Employer.Company).Where(x => jobFilter.Contains(x.JobTypeId) && jobFilterCat.Contains(x.CategoryId) && jobFilterLev.Contains(x.LevelId)).ToList();

			if(types.IsRemote != null)
			{
				jobFiltered = jobFiltered.Where(x => x.IsRemote == remote).ToList();
			}
			//test.ForEach(job => { job.Employer.Company.Logo = imagePath; });
			var jobViewModel = GetViewModel(jobFiltered, types.JobTypeFilterId, types.CategoryFilterId, types.LevelFilterId, types.IsRemote);


			return View("Index_Appl", jobViewModel);
		}


		public JobViewModel GetViewModel(List<Job> jobList, int? jobTypeFilter, int? categoryFilter, int? levelFilter, string? isRemote)
		{
			//var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "\\images\\", "amazon.png");



			//List<int> /*allJobTypes*/ = _context.Jobs.Select(x => x.JobTypeId).Distinct().ToList();

			//jobList.ForEach(job => { job.Employer.Company.Logo = imagePath; });
			var remoteList = new List<string>{ "Yes", "No" };


            var s = new SelectList(_context.JobTypes.ToList(), "JobTypeId", "Name");
			ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "Name");
			ViewBag.Level = new SelectList(_context.Levels.ToList(), "Id", "LevelName");
			ViewBag.Types = new SelectList(_context.JobTypes.ToList(), "JobTypeId", "Name");
			ViewBag.IsRemote = new SelectList(remoteList);

			JobViewModel jobViewModel = new JobViewModel
			{

				Jobs = jobList,
				JobTypeFilterId = jobTypeFilter,
				CategoryFilterId = categoryFilter,
				LevelFilterId = levelFilter,
				IsRemote = isRemote
				

			};


			return jobViewModel;
		}
	}
}
