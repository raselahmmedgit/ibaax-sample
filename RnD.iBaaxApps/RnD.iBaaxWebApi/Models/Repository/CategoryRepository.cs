using RnD.iBaaxData;

namespace RnD.iBaaxWebApi.Models.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context)
            : base(context)
        {
        }
    }

    public interface ICategoryRepository : IBaseRepository<Category>
    {
        
    }
}