using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class ContactDocumentRepository : BaseRepository<ContactDocument>
    {
        public ContactDocumentRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}