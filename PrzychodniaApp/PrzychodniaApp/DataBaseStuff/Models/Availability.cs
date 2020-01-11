using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrzychodniaApp.DataBaseStuff.Models
{
    public class Availability : Entity
    {
        [Required]
        public Specialization Specialization { get; set; }

        [Required]
        public DateTime TimeStart { get; set; }

        [Required]
        public DateTime TimeEnd { get; set; }

        [Required]
        public int MaxAmountOfVisits { get; set; }

        [Required]
        public virtual MedicalWorker MedicalWorker { get; set; }

        [Required]
        public virtual Schedule Schedule { get; set; }

        
        public virtual ICollection<Visits> ScheduledVisits { get; set; }
    }
}