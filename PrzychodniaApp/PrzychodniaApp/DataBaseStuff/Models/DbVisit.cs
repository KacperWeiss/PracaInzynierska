using System;
using System.ComponentModel.DataAnnotations;

namespace PrzychodniaApp.DataBaseStuff.Models
{
    public class DbVisit : DbEntity
    {
        [Required]
        public DateTime TimeStart { get; set; }

        public string OptionalDescription { get; set; }

        public virtual DbSpecialization Specialization { get; set; }

        public virtual DbMedicalWorker MedicalWorker { get; set; }

        public virtual DbPatient Patient { get; set; }
    }
}