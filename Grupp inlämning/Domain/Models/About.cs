using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
