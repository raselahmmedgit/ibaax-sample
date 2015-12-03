using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class TaskPriorityCategoryRepository : BaseRepository<TaskPriorityCategory>
    {
        public TaskPriorityCategoryRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}