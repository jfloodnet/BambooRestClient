namespace BambooRestClient.Resources
{
    public class ResourceName
    {
        private ResourceName(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static ResourceName Plan = new ResourceName("plan");
        public static ResourceName Result = new ResourceName("result");

        public static implicit operator string(ResourceName name)
        {
            return name.Value;
        }

        public static implicit operator ResourceName(string name)
        {
            return new ResourceName(name);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ResourceName)obj;
            return other.GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}