﻿namespace lab.ngsample.Models.Repository
{
    public class CompanyDocumentCategoryRepository : BaseRepository<CompanyDocumentCategory>
    {
        public CompanyDocumentCategoryRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}