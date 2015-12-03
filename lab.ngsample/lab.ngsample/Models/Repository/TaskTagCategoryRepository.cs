using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class TaskTagCategoryRepository : BaseRepository<TaskTagCategory>
    {
        public TaskTagCategoryRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}