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
    public class PrescribedMedications : Entity
    {
        /// <summary>
        /// Name of the medicine
        /// </summary>
        [Required]
        public string MedicinesName { get; set; }

        /// <summary>
        /// Describes dosage for specific medicine
        /// </summary>
        [Required]
        public string Dosage { get; set; } 
    }
}
