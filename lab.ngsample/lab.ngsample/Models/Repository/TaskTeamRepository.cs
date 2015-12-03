using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.ngsample.Models.Repository
{
    public class TaskTeamRepository : BaseRepository<TaskTeam>
    {
        public TaskTeamRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}