using System.Collections.Generic;

namespace PrzychodniaApp.DataBaseStuff.Models
{
    /// <summary>
    /// Helping class used for storing specialization types and linking them with medical workers with required specialization 
    /// </summary>
    public class Specialization : Entity
    {
        /// <summary>
        /// Type of specialization, example: Neurologist
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Easy access to all medical workers with proper specialization
        /// </summary>
        public virtual ICollection<MedicalWorker> MedicalWorkers { get; set; }
    }
}