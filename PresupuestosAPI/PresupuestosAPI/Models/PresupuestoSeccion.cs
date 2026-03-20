using System.ComponentModel.DataAnnotations;

namespace PresupuestosAPI.Models
{
    public class PresupuestoSeccion
    {
        [Key]
        public int IdSection { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int Order { get; set; }
        [Required]
        public string SectionType { get; set; }
        public int IdPresupuesto { get; set; }
        public Presupuesto Presupuesto { get; set; }

    }
}
