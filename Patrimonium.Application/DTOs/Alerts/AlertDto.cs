namespace Patrimonium.Application.DTOs.Alerts
{
    public class AlertDto
    {
        public string Title { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Severity { get; set; } = default!;
        public bool IsRead { get; set; } = false;
    }
}
