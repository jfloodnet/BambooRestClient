using System;

namespace BambooRestClient.Resources
{
    public class Link
    {
        public string Rel { get; set; }
        public string Href { get; set; }

        public Link(string rel, string href)
        {
            Rel = rel;
            Href = href;
        }

        public static implicit operator Uri(Link link)
        {
            return new Uri(link.Href);
        }

        public static implicit operator Link(Uri uri)
        {
            return new Link("self", uri.ToString());
        }

        public override string ToString()
        {
            return Href;
        }
    }
}