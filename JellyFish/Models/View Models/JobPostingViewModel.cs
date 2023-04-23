using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JellyFish.Models.View_Models
{
    public class JobPostingViewModel
    {
        public Job job { get; set; }

        [Display(Name = "Category")]
        public Category category { get; set; }

        [Display(Name = "Job Type")]
        public JobType jobType { get; set; }

        [Display(Name = "Level")]
        public Level level { get; set; }

        [Display(Name = "Applicants")]
        public Applicant applicant { get; set; }

        //[ValidateNever]
        //public IEnumerable<Job> jobList { get; set; }
        //[ValidateNever]
        //public IEnumerable<Applicant> applicantList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> JobTypeList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> LevelList { get; set; }
    }
}
