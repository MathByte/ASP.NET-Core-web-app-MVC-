using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JellyFish.Models;
using JellyFish.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace JellyFish.Areas.Identity.Pages.Account.Manage
{
    public class MyJobsModel : PageModel
    {
        private readonly JellyFish.Models.JellyFishDbContext _context;
        private readonly UserManager<JellyFishUser> _userManager;
        public MyJobsModel(JellyFish.Models.JellyFishDbContext context, UserManager<JellyFishUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Applicant> Applicant { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var user = _userManager.GetUserId(User);
            if (_context.Applicants != null)
            {
                Applicant = await _context.Applicants
                .Include(a => a.Job)
                .Include(a => a.User).Include(x=> x.Job.Employer.Company).Where(x=> x.UserId == user).ToListAsync();
            }
        }
    }
}
