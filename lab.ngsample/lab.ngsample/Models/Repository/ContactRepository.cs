namespace lab.ngsample.Models.Repository
{
    public class ContactRepository : BaseRepository<Contact>
    {
        public ContactRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}