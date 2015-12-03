using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class ContactDocumentCategoryRepository : BaseRepository<ContactDocumentCategory>
    {
        public ContactDocumentCategoryRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}