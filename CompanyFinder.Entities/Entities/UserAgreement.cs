using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class UserAgreement:BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
    }
}
