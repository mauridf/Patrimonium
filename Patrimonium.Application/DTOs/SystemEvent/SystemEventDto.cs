namespace Patrimonium.Application.DTOs.SystemEvent
{
    public class SystemEventDto
    {
        public string Type { get; set; } = default!;   
        public string Source { get; set; } = default!; 
        public string Description { get; set; } = default!;
        public string Severity { get; set; } = default!;
    }
}
