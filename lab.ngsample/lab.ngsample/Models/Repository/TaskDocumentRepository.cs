﻿namespace lab.ngsample.Models.Repository
{
    public class TaskDocumentRepository : BaseRepository<TaskDocument>
    {
        public TaskDocumentRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}