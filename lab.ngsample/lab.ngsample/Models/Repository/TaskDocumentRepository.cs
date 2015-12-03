using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class TaskDocumentRepository : BaseRepository<TaskDocument>
    {
        public TaskDocumentRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}