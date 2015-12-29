namespace lab.ngsample.Models.Repository
{
    public class CompanyDocumentRepository : BaseRepository<CompanyDocument>
    {
        public CompanyDocumentRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}