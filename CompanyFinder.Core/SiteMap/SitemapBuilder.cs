﻿using System.Xml.Linq;

namespace CompanyFinder.Core.SiteMap
{
    public class SitemapBuilder
    {
        private readonly XNamespace NS = "http://www.sitemaps.org/schemas/sitemap/0.9";

        private List<SitemapUrl> _urls;

        public SitemapBuilder()
        {
            _urls = new List<SitemapUrl>();
        }

        public void AddUrl(string url, DateTime? createdDate = null, ChangeFrequency? changeFrequency = null, double? priority = null)
        {
            _urls.Add(new SitemapUrl()
            {
                Url = url,
                CreatedDate = createdDate,
                ChangeFrequency = changeFrequency,
                Priority = priority,
            });
        }

        public override string ToString()
        {
            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(NS + "urlset",
                    from item in _urls
                    select CreateItemElement(item)
                    ));

            return sitemap.ToString();
        }

        private XElement CreateItemElement(SitemapUrl url)
        {
            XElement itemElement = new XElement(NS + "url", new XElement(NS + "loc", url.Url.ToLower()));

            if (url.CreatedDate.HasValue)
            {
                itemElement.Add(new XElement(NS + "lastmod", url.CreatedDate.Value.ToString("yyyy-MM-ddTHH:mm:ss.f") + "+00:00"));
            }

            if (url.ChangeFrequency.HasValue)
            {
                itemElement.Add(new XElement(NS + "changefreq", url.ChangeFrequency.Value.ToString().ToLower()));
            }

            if (url.Priority.HasValue)
            {
                itemElement.Add(new XElement(NS + "priority", url.Priority.Value.ToString("N1")));
            }

            return itemElement;
        }

    }
}
