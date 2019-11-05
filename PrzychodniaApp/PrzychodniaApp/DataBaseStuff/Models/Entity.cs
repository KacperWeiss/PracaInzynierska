using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrzychodniaApp.DataBaseStuff.Models
{
    /// <summary>
    /// Model for entity
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Entity's ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

    }
}
