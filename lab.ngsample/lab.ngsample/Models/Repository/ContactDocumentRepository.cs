namespace lab.ngsample.Models.Repository
{
    public class ContactDocumentRepository : BaseRepository<ContactDocument>
    {
        public ContactDocumentRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}