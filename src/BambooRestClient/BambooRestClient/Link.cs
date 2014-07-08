using System;
using System.Net.Sockets;

namespace BambooRestClient
{
    public class Link
    {
        public string Rel { get; set; }
        public string Href { get; set; }

        public static implicit operator Uri(Link link)
        {
            return new Uri(link.Href);
        }

        public static implicit operator Link(Uri uri)
        {
            return new Link
            {
                Rel = "self",
                Href = uri.ToString()
            };
        }
    }
}