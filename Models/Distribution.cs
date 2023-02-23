namespace MyBotRE.Models
{
    public class Distribution
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? LastUpdate { get; set; }
        public string? BasedOn { get; set; }
        public string? DesktopEnvironments { get; set; }
        public string? Categories { get; set; }
        public string? Architecture { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? MainLink { get; set; }

        public virtual string GetInfo()
        {
            return $"{Name}\n" +
                $"{LastUpdate}\n" +
                $"Based on: {BasedOn}\n" +
                $"DE: {DesktopEnvironments}\n" +
                $"Category: {Categories}\n" +
                $"Status: {Status}\n" +
                $"\n{Description}";
        }
    }
}
