using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class CompanyDocumentCategoryRepository : BaseRepository<CompanyDocumentCategory>
    {
        public CompanyDocumentCategoryRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}