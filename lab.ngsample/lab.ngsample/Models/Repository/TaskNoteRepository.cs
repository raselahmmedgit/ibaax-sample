using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class TaskNoteRepository : BaseRepository<TaskNote>
    {
        public TaskNoteRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}