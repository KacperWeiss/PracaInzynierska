using PrzychodniaApp.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.DataBaseStuff.Models
{
    /// <summary>
    /// Database model describing each vaccination that was given to the patient.
    /// </summary>
    public class Vaccination : Entity
    {
        /// <summary>
        /// Name of the vaccine
        /// </summary>
        [Required]
        public string VaccinesName { get; set; }

        /// <summary>
        /// Enum describing if vaccination is optional, obligatory before reaching certain age, or obligatory and patient is already vaccinated.
        /// </summary>
        [Required]
        public VaccineStatus VaccineStatus { get; set; }

        /// <summary>
        /// Describes when patient was vaccineted
        /// </summary>
        public DateTime? VaccinationDate { get; set; }

        /// <summary>
        /// By this date patient should be vaccinated if vaccine is obligatory.
        /// </summary>
        public DateTime? ObligatoryBy { get; set; }
    }
}
