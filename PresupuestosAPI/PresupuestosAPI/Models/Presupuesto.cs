using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PresupuestosAPI.Models
{
    public class Presupuesto
    {
        [Key]
        public int IdPresupuesto { get; set; }
        [Required]
        public string Title { get; set; }
        public string? ClientName { get; set; }
        public string? ClientNumber { get; set; }
        public DateTime FechaPresupuesto { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public List<PresupuestoSeccion> Secciones { get; set; }
        public int IdCompany { get; set; }
        public Company Company { get; set; }
    }
}
