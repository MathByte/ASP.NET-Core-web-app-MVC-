using JellyFish.Areas.Identity.Data;
using JellyFish.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace JellyFish.Areas.Identity.Pages.Account.Manage
{
    public class EMP : PageModel
    {
        private readonly UserManager<JellyFishUser> _userManager;
        private readonly SignInManager<JellyFishUser> _signInManager;
        private readonly JellyFish.Models.JellyFishDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;



		public EMP(
            UserManager<JellyFishUser> userManager,
            SignInManager<JellyFishUser> signInManager,
            JellyFish.Models.JellyFishDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            //[Required(ErrorMessage = "You must provide a phone number")]
            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
            [Display(Name = "Phone number")]
            public string? PhoneNumber { get; set; }
            [Display(Name = "First Name")]
            public string? FirstName { get; set; }
            [Display(Name = "Last Name")]
            public string? LastName { get; set; }
            [Display(Name = "Job Title")]
            public string? Title { get; set; }
            [Display(Name = "Company Name")]
            public string? Name { get; set; }
            public string? Url { get; set; }
            public IFormFile LogoFile  { get; set; }
            public string? Logo { get; set; }
        }

        private async Task LoadAsync(JellyFishUser user)
        {
            var userMan = _userManager.GetUserId(HttpContext.User);



            var employerDetail = _context.Employers.Include(x=> x.EmployerNavigation).Include(x => x.Company).Where(x => x.EmployerId == userMan).FirstOrDefault();




            //var employeeCompany = _context.Employers.Include(x => x.Company).Where(x => x.EmployerId == userMan).FirstOrDefault();

            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

       

            Username = userName;

            if(employerDetail != null)
            {
                Input = new InputModel
                {
                    PhoneNumber = employerDetail.EmployerNavigation.PhoneNumber,
                    FirstName = employerDetail.EmployerNavigation.FirstName,
                    LastName = employerDetail.EmployerNavigation.LastName,
                    Title = employerDetail.Title,
                    Name = employerDetail.Company.Name,
                    Url = employerDetail.Company.Url,
                    Logo = ""
                };
            }

            
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var employerCompany = _context.Employers.Include(x => x.Company).Where(x => x.EmployerId == user.Id).FirstOrDefault();
            bool companyUpdated = false;
            bool employerUpdated = false;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
                
            }
            if (Input.FirstName != "" && user.FirstName != Input.FirstName)
            {
                user.FirstName = Input.FirstName;
                employerUpdated = true;
            }
            if (Input.LastName != "" && user.LastName != Input.LastName)
            {
                user.LastName = Input.LastName;
                employerUpdated = true;
            }
            if (Input.Name != "" && employerCompany.Company.Name != Input.Name)
            {
                employerCompany.Company.Name = Input.Name;
                companyUpdated = true;


            }
            if (Input.Url != "" && employerCompany.Company.Url != Input.Url)
            {
                employerCompany.Company.Url = Input.Url;
                companyUpdated = true;

            }
            if (Input.Title != "" && employerCompany.Title != Input.Title)
            {
                employerCompany.Title = Input.Title;
                companyUpdated = true;
            }
            if (Input.Logo != "" && employerCompany.Company.Logo != Input.Logo)
            {
                var folder = "/images/" + Input.LogoFile.FileName;
               var logoPath =  Path.Combine(_webHostEnvironment.WebRootPath, "images", Input.LogoFile.FileName);

                var stream = new FileStream(logoPath, FileMode.Create);
                Input.LogoFile.CopyToAsync(stream);

                employerCompany.Company.Logo = folder;
                companyUpdated = true;
            }

            if (companyUpdated)
            {
                _context.Employers.Update(employerCompany);
                _context.SaveChanges();
            }

            if (employerUpdated)
            {
                await _userManager.UpdateAsync(user);
                _context.SaveChanges();

            }
       


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
    
}

