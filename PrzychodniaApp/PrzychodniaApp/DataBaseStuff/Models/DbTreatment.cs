using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.DataBaseStuff.Models
{
    /// <summary>
    /// Database model describing treatment provided to the patient.
    /// </summary>
    public class DbTreatment : DbEntity
    {
        /// <summary>
        /// Illness' name
        /// </summary>
        [Required]
        public string IllnessName { get; set; }

        /// <summary>
        /// Short description of all symptoms.
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string SymptomsDescription { get; set; }

        /// <summary>
        /// Time of the visit
        /// </summary>
        [Required]
        public DateTime VisitDate { get; set; }

        /// <summary>
        /// List of all medications prescribed to the patient
        /// </summary>
        public virtual ICollection<DbPrescribedMedications> Prescription { get; set; }
    }
}
