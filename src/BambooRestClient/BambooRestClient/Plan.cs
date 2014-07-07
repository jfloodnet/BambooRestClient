namespace BambooRestClient
{
    public class Plan
    {
        public string ShortName { get; set; }
        public string ShortKey { get; set; }
        public string Type { get; set; }
        public bool Enabled { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public bool IsBuilding { get; set; }
        public bool IsActive { get; set; }
        public Link Link { get; set; }
    }
}