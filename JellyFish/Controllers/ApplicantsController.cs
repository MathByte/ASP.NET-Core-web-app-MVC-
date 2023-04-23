using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JellyFish.Models;
using JellyFish.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using JellyFish.Models.View_Models;
using System.Drawing;

namespace JellyFish.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly JellyFishDbContext _context;
        private readonly UserManager<JellyFishUser> _userManager;
 



        public ApplicantsController(UserManager<JellyFishUser> userManager , JellyFishDbContext context)
        {
            _context = context;
            _userManager = userManager;
            
        }

        // GET: Applicants
        public async Task<IActionResult> Index(string? jobID, bool isApplied, bool isSaved )
        {
            var user = await _userManager.GetUserAsync(User);
            string userid = await _userManager.GetUserIdAsync(user);

            if (User.IsInRole("JobSeeker"))
            {
                List<Job> jobs =  (List<Job>) _context.Jobs.Where(j => j.JobId == Int32.Parse(jobID)).ToList(); 

                ApplyJobsViewModel vm = new ApplyJobsViewModel()
                {
                    job_ID = Int32.Parse(jobID),
                    user_ID = userid,
                    job_title = jobs[0].Title,
                    job_desc = jobs[0].Description
                };
                Applicant appl = new Applicant();

                var existJob = _context.Applicants.Where(x => x.UserId == userid && x.JobId == Int32.Parse(jobID)).FirstOrDefault();
                try
                {
                    if (existJob != null)
                    {
                        if (isApplied)
                        {
                            existJob.IsApplied = isApplied;

                        }
                        if(isSaved) { 
                            existJob.IsSaved = isSaved;
                        }
                        _context.Update(existJob);
                        _context.SaveChanges();

                    }
                    else
                    {
                        if (isApplied)
                        {
                            appl.IsApplied = isApplied;
                        }
                        if (isSaved)
                        {
                            appl.IsSaved = isSaved;
                        }

                        appl.JobId = vm.job_ID;
                        appl.UserId = vm.user_ID;
                        _context.Applicants.Add(appl);
                        _context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return RedirectToAction("Details","Jobs", new { id =  vm.job_ID});
                
                //return View("Index_Appl", vm);
            }

            var appl2 = _context.Applicants.Include(a => a.Job).Include(a => a.User);
            return View(await appl2.ToListAsync());
        }

		//[HttpPost("/Applicants/ApplyForJob/"), ActionName("ApplyForJob")]
		//public async Task<IActionResult> ApplyForJob( [Bind("job_ID,job_title,user_ID,job_desc")] ApplyJobsViewModel vm)
		//{
            
     
   //         Applicant appl = new Applicant()
   //         {
   //             JobId = vm.job_ID,
   //             UserId = vm.user_ID
			//};
   //         try
   //         {
			//	await _context.Applicants.AddAsync(appl);
			//	await _context.SaveChangesAsync();
			//}
   //         catch(Exception ex) 
   //         {

   //         }
           


   //         return RedirectToAction("Index", "Jobs");
       // }



        // GET: Applicants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }


            //var _user = await _context.AspNetUsers
            //    .Include(q => q.Address)
            // .Include(w => w.UserSkills)
            // .ThenInclude(w => w.Skill)
            // .Include(e => e.Applicants) ;
             
           

            ViewBag.address = await _context.Applicants
                .Include(a => a.User)
                .ThenInclude(w => w.Address)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);



            var applicant = await _context.Applicants
                .Include(a => a.User)          
                .ThenInclude(w => w.UserSkills)
                .ThenInclude(w => w.Skill)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);

            if (applicant == null)
            {
                return NotFound();
            }
           
            return View(applicant);
        }

        // GET: Applicants/Create
        public IActionResult Create()
        {
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Applicants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicantId,JobId,UserId")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", applicant.JobId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", applicant.UserId);
            return View(applicant);
        }

        // GET: Applicants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", applicant.JobId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", applicant.UserId);
            return View(applicant);
        }

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicantId,JobId,UserId")] Applicant applicant)
        {
            if (id != applicant.ApplicantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantExists(applicant.ApplicantId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", applicant.JobId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", applicant.UserId);
            return View(applicant);
        }

        // GET: Applicants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .Include(a => a.Job)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Applicants == null)
            {
                return Problem("Entity set 'JellyFishDbContext.Applicants'  is null.");
            }
            var applicant = await _context.Applicants.FindAsync(id);
            if (applicant != null)
            {
                _context.Applicants.Remove(applicant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantExists(int id)
        {
          return (_context.Applicants?.Any(e => e.ApplicantId == id)).GetValueOrDefault();
        }
    }
}
