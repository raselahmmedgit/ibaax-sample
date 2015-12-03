using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class CompanyDocumentRepository : BaseRepository<CompanyDocument>
    {
        public CompanyDocumentRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}