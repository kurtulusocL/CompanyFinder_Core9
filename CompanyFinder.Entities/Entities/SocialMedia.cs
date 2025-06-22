using System.ComponentModel.DataAnnotations;
using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class SocialMedia:BaseEntity
    {
        public string Name { get; set; }

        [Url]
        public string Url { get; set; }
        public string ImageUrl { get; set; }
    }
}
