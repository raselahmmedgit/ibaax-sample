using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class TaskTagMapRepository : BaseRepository<TaskTagMap>
    {
        public TaskTagMapRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}