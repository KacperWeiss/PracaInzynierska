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
    /// Database model describing patient.
    /// </summary>
    public class DbPatient : DbEntity
    {
        /// <summary>
        /// Describes if patient is male or female.
        /// </summary>
        [Required]
        public Pronouns PatientsPronounce { get; set; }

        /// <summary>
        /// Describes how much email contact is allowed by patient.
        /// </summary>
        [Required]
        public EmailContact EmailContact { get; set; }

        /// <summary>
        /// Patient's first name
        /// </summary>
        [Required]
        //[RegularExpression("^[A-Z][a-z]{2,}$", ErrorMessage = "Name starts with upper case and all other letters must be lower case, also there are allowed only latin letters.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Patient's last name
        /// </summary>
        [Required]
        //[RegularExpression("^[A-Z][a-z]{2,}$", ErrorMessage = "Name starts with upper case and all other letters must be lower case, also there are allowed only latin letters.")]
        public string LastName { get; set; }

        public string EmailAdress { get; set; }

        /// <summary>
        /// Date of birth of the patient
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Allows access to patient's treatment history
        /// </summary>
        public DbTreatmentHistory TreatmentHistory { get; set; }

        public ICollection<DbVisit> Visits { get; set; }
    }
}
