using Microsoft.EntityFrameworkCore;
using PresupuestosAPI.Data;
using PresupuestosAPI.Models;

namespace PresupuestosAPI.Services
{
    public class PresupuestoSeccionService
    {
        private readonly AppDbContext _context;

        public PresupuestoSeccionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PresupuestoSeccion?> GetSeccionByIdAsync(int id)
        {
            return await _context.PresupuestoSecciones.FindAsync(id);
        }

        public async Task<List<PresupuestoSeccion>> GetSeccionesByPresupuestoIdAsync(int presupuestoId)
        {
            return await _context.PresupuestoSecciones
                .Where(s => s.IdPresupuesto == presupuestoId)
                .OrderBy(s => s.Order)
                .ToListAsync();
        }

        public async Task<PresupuestoSeccion> CreateSeccionAsync(PresupuestoSeccion seccion)
        {
            _context.PresupuestoSecciones.Add(seccion);
            await _context.SaveChangesAsync();
            return seccion;
        }

        public async Task<PresupuestoSeccion> UpdateSeccionAsync(int id, PresupuestoSeccion updateSeccion)
        {
            var seccion = await _context.PresupuestoSecciones.FindAsync(id);
            if (seccion == null)
            {
                return null;
            }

            seccion.Title = updateSeccion.Title;
            seccion.Content = updateSeccion.Content;
            seccion.Order = updateSeccion.Order;
            seccion.SectionType = updateSeccion.SectionType;

            await _context.SaveChangesAsync();
            return seccion;
        }

        public async Task<bool> DeleteSeccionAsync(int id)
        {
            var seccion = await _context.PresupuestoSecciones.FindAsync(id);
            if (seccion == null) 
            { 
                return false;
            }

            _context.PresupuestoSecciones.Remove(seccion);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
