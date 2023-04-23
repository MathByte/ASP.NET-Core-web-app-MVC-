using System.ComponentModel;

namespace JellyFish.Models.View_Models
{
	public class ApplyJobsViewModel
	{
	
		public int job_ID { get; set; }

		[DisplayName("Job Title")]
		public string job_title { get; set; }

		[DisplayName("Job Description")]
		public string job_desc { get; set; }

        public string user_ID { get; set;}
	}
}
