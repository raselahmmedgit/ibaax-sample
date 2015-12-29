namespace lab.ngsample.Models.Repository
{
    public class ContactDocumentCategoryRepository : BaseRepository<ContactDocumentCategory>
    {
        public ContactDocumentCategoryRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}