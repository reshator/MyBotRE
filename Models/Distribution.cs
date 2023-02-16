namespace MyBotRE.Models
{
    internal class Distribution
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? BasedOn { get; set; }
        public List<string>? DesktopEnvironments { get; set; }
        public List<string>? Categories { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
    }
}
