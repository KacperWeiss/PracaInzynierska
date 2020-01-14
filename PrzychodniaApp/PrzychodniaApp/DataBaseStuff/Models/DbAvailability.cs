using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrzychodniaApp.DataBaseStuff.Models
{
    public class DbAvailability : DbEntity
    {
        [Required]
        public DayOfWeek Day { get; set; }

        [Required]
        public DateTime TimeStart { get; set; }

        [Required]
        public DateTime TimeEnd { get; set; }

        [Required]
        public int MaxAmountOfVisits { get; set; }

        [Required]
        public double? VisitPrice { get; set; }

        [Required]
        public virtual DbSpecialization Specialization { get; set; }

        [Required]
        public virtual DbMedicalWorker MedicalWorker { get; set; }
    }
}