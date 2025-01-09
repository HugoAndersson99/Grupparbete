using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class WorkExperience
    {
        [Required]
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string City { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CvId { get; set; }
        public CV Cv { get; set; }

        public WorkExperience(Guid id, string companyName, string jobTitle, string jobDescription, string city, DateTime startDate, DateTime endDate)
        {
            Id = id;
            CompanyName = companyName;
            JobTitle = jobTitle;
            JobDescription = jobDescription;
            City = city;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
