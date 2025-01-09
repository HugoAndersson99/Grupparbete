using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class About
    {
        [Required]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid CvId { get; set; }
        public CV Cv { get; set; }

        public About(Guid id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
