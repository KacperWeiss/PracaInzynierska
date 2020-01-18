using PrzychodniaApp.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.DataBaseStuff.Models
{
    public class DbUser : DbEntity
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public UserAccess UserAccess { get; set; }
    }
}
