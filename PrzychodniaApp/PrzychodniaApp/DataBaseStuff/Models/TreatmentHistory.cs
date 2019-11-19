using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.DataBaseStuff.Models
{
    /// <summary>
    /// Database model describing patient's treatment history.
    /// </summary>
    public class TreatmentHistory : Entity
    {
        /// <summary>
        /// Provides information about last time patient had visited.
        /// </summary>
        [Required]
        public DateTime LastVisit { get; set; }

        /// <summary>
        /// Lists treatments and illnesses that patient underwent in the past.
        /// </summary>
        public IList<Treatment> Treatments { get; set; }

        /// <summary>
        /// Lists vaccinations that patient underwent in the past.
        /// </summary>
        public IList<Vaccination> PastVaccinations { get; set; }

        /// <summary>
        /// Lists vaccinations that are obligatory for the patient in the future.
        /// </summary>
        public IList<Vaccination> RequiredVaccinations { get; set; }
    }
}
