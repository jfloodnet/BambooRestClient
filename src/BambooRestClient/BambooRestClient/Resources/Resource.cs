namespace BambooRestClient.Resources
{
    public class Resource
    {
        public ResourceName Name { get; private set; }
        public Link Link { get; private set; }

        public Resource(ResourceName name, Link link)
        {
            Name = name;
            Link = link;
        }
    }
}