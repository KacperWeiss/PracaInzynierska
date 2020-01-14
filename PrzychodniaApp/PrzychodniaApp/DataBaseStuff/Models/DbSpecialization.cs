using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrzychodniaApp.DataBaseStuff.Models
{
    /// <summary>
    /// Helping class used for storing specialization types and linking them with medical workers with required specialization 
    /// </summary>
    public class DbSpecialization : DbEntity
    {
        /// <summary>
        /// Type of specialization, example: Neurologist
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// Easy access to all medical workers with proper specialization
        /// </summary>
        public virtual ICollection<DbMedicalWorker> MedicalWorkers { get; set; }


        public virtual ICollection<DbAvailability> Availabilities { get; set; }
    }
}