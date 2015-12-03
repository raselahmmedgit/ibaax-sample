using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class TaskTypeCategoryRepository : BaseRepository<TaskTypeCategory>
    {
        public TaskTypeCategoryRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}