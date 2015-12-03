using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class TaskRepository : BaseRepository<Task>
    {
        public TaskRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}