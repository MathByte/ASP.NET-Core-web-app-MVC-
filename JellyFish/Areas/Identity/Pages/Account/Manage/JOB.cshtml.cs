

using JellyFish.Areas.Identity.Data;
using JellyFish.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;

namespace JellyFish.Areas.Identity.Pages.Account.Manage.JobSeeker
{
	public class IndexModel : PageModel
	{
		private readonly UserManager<JellyFishUser> _userManager;
		private readonly SignInManager<JellyFishUser> _signInManager;
		private readonly Models.JellyFishDbContext _context;

		private readonly IWebHostEnvironment _webHostEnvironment;

		public IndexModel(JellyFishDbContext context, UserManager<JellyFishUser> userManager, SignInManager<JellyFishUser> signInManager, IWebHostEnvironment webHostEnvironment)
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

			[Required(ErrorMessage = "You must provide a phone number")]
			[DataType(DataType.PhoneNumber)]
			[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
			[Display(Name = "Phone number")]
			public string? PhoneNumber { get; set; }

			[Display(Name = "Street")]
			public string? Street { get; set; }

			[Display(Name = "City")]
			public string? City { get; set; }

			[Display(Name = "PostalCode")]
			public string? PostalCode { get; set; }

			[Display(Name = "Province")]
			public string? Province { get; set; }


			[Display(Name = "First Name")]
			public string? FirstName { get; set; }

			[Display(Name = "Last Name")]
			public string? LastName { get; set; }

			[Display(Name = "Date Of Birth")]
			public DateOnly DateOfBirth { get; set; }

			[Display(Name = "Add a Skill")]
			public string? Skill { get; set; }

			[Display(Name = "Image of Profile")]
			public string? ProfileImage { get; set; }

			[Display(Name = "Resume File")]
			public string? ResumeFile { get; set; }
		}

		private async Task LoadAsync(JellyFishUser user)
		{
			var userName = await _userManager.GetUserNameAsync(user);
			var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
			var userId = await _userManager.GetUserIdAsync(user);

			AspNetUser? _AspUser = _context.AspNetUsers.Where(n => n.Id == userId).FirstOrDefault();

			Username = userName;


			Address? addres = (Address?)_context.Addresses.Where(n => n.AddressId == userId).FirstOrDefault();
			if (addres != null)
			{
				Input = new InputModel
				{
					FirstName = _AspUser == null ? String.Empty : _AspUser.FirstName,
					LastName = _AspUser == null ? String.Empty : _AspUser.LastName,
					PhoneNumber = phoneNumber,
					Street = addres.Street,
					City = addres.City,
					PostalCode = addres.PostalCode,
					Province = addres.Province,
					DateOfBirth = DateOnly.FromDateTime(_AspUser.DateOfBirth == null ? (DateTime)DateTime.Now : (DateTime)_AspUser.DateOfBirth),
					ProfileImage = _AspUser.ProfileImage,
					ResumeFile = _AspUser.ResumeFile

				};
			}
			else
			{
				Input = new InputModel
				{
					FirstName = _AspUser == null ? String.Empty : _AspUser.FirstName,
					LastName = _AspUser == null ? String.Empty : _AspUser.LastName,
					PhoneNumber = phoneNumber,
					Street = "",
					City = "",
					PostalCode = "",
					Province = "",
					DateOfBirth = DateOnly.FromDateTime(_AspUser.DateOfBirth == null ? (DateTime)DateTime.Now : (DateTime)_AspUser.DateOfBirth),
					ProfileImage = "",
					ResumeFile = ""
				};
			}






			List<UserSkill> list = _context.UserSkills.Include(s => s.Skill).Where(s => s.UserId == userId).ToList<UserSkill>();
			ViewData["sklist"] = list;





			//List<UserSkill> list2 = _context.UserSkills.Include(s => s.Skill).ThenInclude(s => s.user) Where(s => s.UserId == userId)

			//_context.AspNetUsers.Include(s => s.UserSkills).ThenInclude(u => u.Skill).Where(y => y.Id == userId).ToList<UserSkill>();


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

		public async Task<IActionResult> OnPostAsync(List<IFormFile> files)
		{
			var user = await _userManager.GetUserAsync(User);
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

			user.DateOfBirth = Input.DateOfBirth.ToDateTime(TimeOnly.Parse("10:00 PM"));
			user.FirstName = Input.FirstName;
			user.LastName = Input.LastName;
			user.PhoneNumber = Input.PhoneNumber;

			await _userManager.UpdateAsync(user);

			//for profile image and resume file
			string profileImg = HttpContext.Request.Form["profileImg"].ToString();


			//var supportedTypes = new[] { "txt", "doc", "docx", "pdf", "xls", "xlsx" };
			var supportedTypes = new[] { "png", "jpeg", "jpg", "gif", "bmp" };


			var _AspUser = _context.AspNetUsers.Where(x => x.Id == user.Id).FirstOrDefault();
			//int skillId = _context.UserSkills.Where(x => x.UserId == user.Id).FirstOrDefault().SkillId;
			// var resume = _context.Skills.Where(x => x.SkillId == skillId).FirstOrDefault();

			string wwwRootPath = _webHostEnvironment.WebRootPath;

			if (files.Count > 0)
			{
				foreach (var file in files)
				{
					string fileName = Guid.NewGuid().ToString();
					var uploadsForProfile = Path.Combine(wwwRootPath, @"images\profile");
					var uploadsForResume = Path.Combine(wwwRootPath, @"images\resume");
					var extension = Path.GetExtension(file.FileName);
					var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);

					if (supportedTypes.Contains(fileExt))
					{
						if (_AspUser.ProfileImage != null || _AspUser.ProfileImage != "")
						{
							var oldImagePath = wwwRootPath;
							if (System.IO.File.Exists(oldImagePath))
							{
								System.IO.File.Delete(oldImagePath);
							}
							using (var fileStreams = new FileStream(Path.Combine(uploadsForProfile, fileName + extension), FileMode.Create))
							{
								file.CopyTo(fileStreams);
							}

							_AspUser.ProfileImage = @"\images\profile\" + fileName + extension;
							_context.AspNetUsers.Update(_AspUser);

						}
						else if (_AspUser.ProfileImage == null || _AspUser.ProfileImage == "")
						{
							using (var fileStreams = new FileStream(Path.Combine(uploadsForProfile, fileName + extension), FileMode.Create))
							{
								file.CopyTo(fileStreams);
							}

							_AspUser.ProfileImage = @"\images\profile\" + fileName + extension;
							_context.AspNetUsers.Add(_AspUser);

						}
					}
					else
					{
						if (_AspUser.ResumeFile != null || _AspUser.ResumeFile != "")
						{
							var oldImagePath = wwwRootPath;
							if (System.IO.File.Exists(oldImagePath))
							{
								System.IO.File.Delete(oldImagePath);
							}
							using (var fileStreams = new FileStream(Path.Combine(uploadsForResume, fileName + extension), FileMode.Create))
							{
								file.CopyTo(fileStreams);
							}

							_AspUser.ResumeFile = @"\images\resume\" + fileName + extension;
							_context.AspNetUsers.Update(_AspUser);

						}
						else if (_AspUser.ResumeFile == null || _AspUser.ResumeFile == "")
						{
							using (var fileStreams = new FileStream(Path.Combine(uploadsForResume, fileName + extension), FileMode.Create))
							{
								file.CopyTo(fileStreams);
							}

							_AspUser.ResumeFile = @"\images\resume\" + fileName + extension;
							_context.AspNetUsers.Add(_AspUser);

						}
					}

				}
				_context.SaveChanges();
			}


			var userId = await _userManager.GetUserIdAsync(user);
			Address? addres = (Address?)_context.Addresses.Where(n => n.AddressId == userId).FirstOrDefault();

			if (addres == null)
			{
				addres = new Address();
				addres.City = Input.City;
				addres.Street = Input.Street;
				addres.PostalCode = Input.PostalCode;
				addres.Province = Input.Province;
				addres.AddressId = userId;

				_context.Addresses.Add(addres);
				_context.SaveChanges();
			}
			else
			{
				if (Input.City != addres.City)
					addres.City = Input.City;

				if (Input.Street != addres.Street)
					addres.Street = Input.Street;

				if (Input.PostalCode != addres.PostalCode)
					addres.PostalCode = Input.PostalCode;

				if (Input.Province != addres.Province)
					addres.Province = Input.Province;


				_context.Addresses.Update(addres);
				_context.SaveChanges();
			}

			bool notFound = true;
			bool UserSkillsnotFound = true;

			if (Input.Skill != null)
			{
				List<Skill> skillstable = (List<Skill>)_context.Skills.ToList<Skill>();
				if (skillstable.Count != 0)
				{

					foreach (var skil in skillstable)
					{
						if (skil.Name.Equals(Input.Skill))
						{
							notFound = false;

							List<UserSkill> userSkillstable = (List<UserSkill>)_context.UserSkills.Where(w => w.UserId == userId).ToList<UserSkill>();
							foreach (var skilUser in userSkillstable)
								if (skilUser.SkillId == skil.SkillId)
								{
									UserSkillsnotFound = false;
									break;
								}


							if (UserSkillsnotFound)
							{
								UserSkill userSkill = new UserSkill()
								{
									Skill = skil,
									SkillId = skil.SkillId,
									UserId = userId
								};
								_context.UserSkills.Add(userSkill);
								_context.SaveChanges();
							}



							StatusMessage = "Skill is already exist!";

							break;
						}
					}


					if (notFound)
					{
						Skill sk = new Skill()
						{
							Name = Input.Skill
						};
						_context.Skills.Add(sk);
						_context.SaveChanges();

						UserSkill userSkill = new UserSkill()
						{
							Skill = sk,
							SkillId = sk.SkillId,
							UserId = userId
						};
						_context.UserSkills.Add(userSkill);
						_context.SaveChanges();
					}


				}
				else
				{
					Skill sk = new Skill()
					{
						Name = Input.Skill
					};
					_context.Skills.Add(sk);
					_context.SaveChanges();

					UserSkill userSkill = new UserSkill()
					{
						Skill = sk,
						SkillId = sk.SkillId,
						UserId = userId
					};
					_context.UserSkills.Add(userSkill);
					_context.SaveChanges();
				}

			}







			if (notFound == true)
			{
				StatusMessage = "Your profile has been updated";
			}



			await _signInManager.RefreshSignInAsync(user);

			return RedirectToPage();
		}
	}
}
