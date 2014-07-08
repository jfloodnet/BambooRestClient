namespace BambooRestClient
{
    public class Resource
    {
        public string Name { get; private set; }
        public Link Link { get; private set; }

        public Resource(string name, Link link)
        {
            Name = name;
            Link = link;
        }
    }
}