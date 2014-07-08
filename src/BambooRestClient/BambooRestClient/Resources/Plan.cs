namespace BambooRestClient.Resources
{
    public class Plan
    {
        public string ShortName { get; private set; }
        public string ShortKey { get; private set; }
        public string Type { get; private set; }
        public bool Enabled { get; private set; }
        public string Key { get; private set; }
        public string Name { get; private set; }
        public bool IsBuilding { get; private set; }
        public bool IsActive { get; private set; }
        public Link Link { get; private set; }

        public Plan(
            string shortName, 
            string shortKey, 
            string type, 
            bool enabled, 
            string key, 
            string name, 
            bool isBuilding, 
            bool isActive, 
            Link link)
        {
            ShortName = shortName;
            ShortKey = shortKey;
            Type = type;
            Enabled = enabled;
            Key = key;
            Name = name;
            IsBuilding = isBuilding;
            IsActive = isActive;
            Link = link;
        }

        public override string ToString()
        {
            return string.Format("Key: {0}, Name: {1}", Key, Name);
        }
    }
}