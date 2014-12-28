using RnD.iBaaxData;

namespace RnD.iBaaxWebApi.Models.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context)
            : base(context)
        {
        }
    }

    public interface IProductRepository : IBaseRepository<Product>
    {

    }
}