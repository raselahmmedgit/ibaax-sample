using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class CompanyRepository : BaseRepository<Company>
    {
        public CompanyRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}