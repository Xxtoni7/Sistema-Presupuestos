using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PresupuestosAPI.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        [Required]
        public string Mail { get; set; }

        [Required]
        public string Password { get; set; }
        public List<Company>? Companies { get; set; }
    }
}
