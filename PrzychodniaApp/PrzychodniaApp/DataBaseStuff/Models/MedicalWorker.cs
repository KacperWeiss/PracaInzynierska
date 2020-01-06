using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.DataBaseStuff.Models
{
    /// <summary>
    /// Database model describing prescribed medications.
    /// </summary>
    public abstract class MedicalWorker : Entity
    {
        /// <summary>
        /// Patient's first name
        /// </summary>
        [Required]
        [RegularExpression("^[A-Z][a-z]{2,}$", ErrorMessage = "Name starts with upper case and all other letters must be lower case, also there are allowed only latin letters.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Patient's last name
        /// </summary>
        [Required]
        [RegularExpression("^[A-Z][a-z]{2,}$", ErrorMessage = "Name starts with upper case and all other letters must be lower case, also there are allowed only latin letters.")]
        public string LastName { get; set; }

        /// <summary>
        /// Collection of specializations that this worker has
        /// </summary>
        [Required]
        public ICollection<Specialization> Specializations { get; set; }


    }
}
