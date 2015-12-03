using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class TaskStatusCategoryRepository : BaseRepository<TaskStatusCategory>
    {
        public TaskStatusCategoryRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}