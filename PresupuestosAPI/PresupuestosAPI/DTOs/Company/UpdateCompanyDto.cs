namespace PresupuestosAPI.DTOs.Company
{
    public class UpdateCompanyDto
    {
        public string Name { get; set; }
        public string? LogoUrl { get; set; }
        public string? ColorMain { get; set; }
        public string? ColorSecondary { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Industry { get; set; }
    }
}