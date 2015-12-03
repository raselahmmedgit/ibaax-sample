using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}