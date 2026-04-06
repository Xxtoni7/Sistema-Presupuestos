using Microsoft.EntityFrameworkCore;
using PresupuestosAPI.Data;
using PresupuestosAPI.DTOs.Presupuesto;
using PresupuestosAPI.Models;

namespace PresupuestosAPI.Services
{
    public class PresupuestoService
    {
        private readonly AppDbContext _context;
        public PresupuestoService(AppDbContext context)
        {
            _context = context;
        }

        private static PresupuestoResponseDto MapToPresupuestoResponseDto(Presupuesto presupuesto)
        {
            return new PresupuestoResponseDto
            {
                IdPresupuesto = presupuesto.IdPresupuesto,
                Title = presupuesto.Title,
                BudgetNumber = presupuesto.BudgetNumber!,
                ClientName = presupuesto.ClientName,
                FechaPresupuesto = presupuesto.FechaPresupuesto,
                FechaVencimiento = presupuesto.FechaVencimiento,
                WorkAddress = presupuesto.WorkAddress,
                JobDescription = presupuesto.JobDescription,
                EstimatedTime = presupuesto.EstimatedTime,
                PaymentTerms = presupuesto.PaymentTerms,
                Observations = presupuesto.Observations,
                Total = presupuesto.Total,
                IdCompany = presupuesto.IdCompany
            };
        }

        public async Task<List<PresupuestoResponseDto>> GetAllPresupuestosAsync()
        {
            var presupuestos = await _context.Presupuestos.ToListAsync();

            return presupuestos.Select(MapToPresupuestoResponseDto).ToList();
        }

        public async Task<List<PresupuestoResponseDto>> GetPresupuestosByCompanyIdAsync(int companyId)
        {
            var presupuestos = await _context.Presupuestos
                .Where(p => p.IdCompany == companyId)
                .ToListAsync();

            return presupuestos.Select(MapToPresupuestoResponseDto).ToList();
        }

        public async Task<PresupuestoResponseDto?> GetPresupuestoByIdAsync(int id)
        {
            var p = await _context.Presupuestos.FindAsync(id);
            if (p == null)
            {
                return null;
            }

            return MapToPresupuestoResponseDto(p);
        }

        public async Task<List<PresupuestoResponseDto>> GetPresupuestosByTitleAsync(string title)
        {
            var presupuestos = await _context.Presupuestos
                .Where(p => p.Title.Contains(title))
                .ToListAsync();

            return presupuestos.Select(MapToPresupuestoResponseDto).ToList();
        }

        public async Task<PresupuestoResponseDto> CreatePresupuestoAsync(CreatePresupuestoDto dto)
        {
            var year = DateTime.Now.Year;

            var lastPresupuesto = await _context.Presupuestos
                .Where(p => p.BudgetNumber != null && p.BudgetNumber.StartsWith($"PRES-{year}"))
                .OrderByDescending(p => p.IdPresupuesto)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (lastPresupuesto != null)
            {
                var lastNumberPart = lastPresupuesto.BudgetNumber!.Split('-').Last();
                nextNumber = int.Parse(lastNumberPart) + 1;
            }

            var presupuesto = new Presupuesto
            {
                Title = dto.Title,
                BudgetNumber = $"PRES-{year}-{nextNumber.ToString("D4")}",
                ClientName = dto.ClientName,
                FechaPresupuesto = dto.FechaPresupuesto,
                FechaVencimiento = dto.FechaVencimiento,
                WorkAddress = dto.WorkAddress,
                JobDescription = dto.JobDescription,
                EstimatedTime = dto.EstimatedTime,
                PaymentTerms = dto.PaymentTerms,
                Observations = dto.Observations,
                Total = 0,
                IdCompany = dto.IdCompany
            };

            _context.Presupuestos.Add(presupuesto);
            await _context.SaveChangesAsync();

            return MapToPresupuestoResponseDto(presupuesto);
        }

        public async Task<PresupuestoResponseDto?> UpdatePresupuestoAsync(int id, UpdatePresupuestoDto dto)
        {
            var presupuesto = await _context.Presupuestos.FindAsync(id);
            if (presupuesto == null)
            {
                return null;
            }

            presupuesto.Title = dto.Title;
            presupuesto.ClientName = dto.ClientName;
            presupuesto.FechaPresupuesto = dto.FechaPresupuesto;
            presupuesto.FechaVencimiento = dto.FechaVencimiento;
            presupuesto.WorkAddress = dto.WorkAddress;
            presupuesto.JobDescription = dto.JobDescription;
            presupuesto.EstimatedTime = dto.EstimatedTime;
            presupuesto.PaymentTerms = dto.PaymentTerms;
            presupuesto.Observations = dto.Observations;

            await _context.SaveChangesAsync();

            return MapToPresupuestoResponseDto(presupuesto);
        }

        public async Task<bool> DeletePresupuestoAsync(int id)
        {
            var presupuesto = await _context.Presupuestos.FindAsync(id);
            if (presupuesto == null)
            {
                return false;
            }

            _context.Presupuestos.Remove(presupuesto);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
