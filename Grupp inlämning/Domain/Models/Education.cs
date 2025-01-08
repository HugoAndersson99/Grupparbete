using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Education
    {
        [Required]
        public Guid Id { get; set; }
        public string SchoolName { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CvId { get; set; }
        public CV Cv { get; set; }
        public Education(Guid id, string schoolName, string degree, string fieldOfStudy, string city, string description, DateTime startDate, DateTime endDate)
        {
            Id = id;
            SchoolName = schoolName;
            Degree = degree;
            FieldOfStudy = fieldOfStudy;
            City = city;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
