using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class ContactRepository : BaseRepository<Contact>
    {
        public ContactRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}