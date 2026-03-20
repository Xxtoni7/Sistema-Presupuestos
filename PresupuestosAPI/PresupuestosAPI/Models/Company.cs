using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PresupuestosAPI.Models
{
    public class Company
    {
        [Key]
        public int IdCompany{ get; set; }
        [Required]
        public string Name { get; set; }
        public string? LogoUrl { get; set; }
        public string? ColorMain { get; set; }
        public string? ColorSecondary { get; set; }
        public List<Presupuesto> Presupuestos { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }
    }
}
