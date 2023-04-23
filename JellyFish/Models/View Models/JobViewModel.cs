namespace JellyFish.Models.View_Models
{
    public class JobViewModel
    {
        public List<Job> Jobs { get; set; }  
        public int? JobTypeFilterId { get; set; }
        public int? CategoryFilterId { get; set; }
        public int? LevelFilterId { get; set; }
        public string? IsRemote { get; set; }
    }
}
