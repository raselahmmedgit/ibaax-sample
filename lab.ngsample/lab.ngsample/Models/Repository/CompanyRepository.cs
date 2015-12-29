namespace lab.ngsample.Models.Repository
{
    public class CompanyRepository : BaseRepository<Company>
    {
        public CompanyRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}