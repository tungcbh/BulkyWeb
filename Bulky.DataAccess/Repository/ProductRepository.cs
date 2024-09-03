using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var productFromDB = _db.Products.FirstOrDefault(u => u.Id == product.Id);
            if (productFromDB != null)
            {
                productFromDB.Title = product.Title;
                productFromDB.ISBN = product.ISBN;
                productFromDB.Description = product.Description;
                productFromDB.Category = product.Category;
                productFromDB.ListPrice = product.ListPrice;
                productFromDB.Price = product.Price;
                productFromDB.Price50 = product.Price50;
                productFromDB.Price100 = product.Price100;
                productFromDB.ProductImages = product.ProductImages;
                //if (product.ImageURL != null)
                //{
                //    productFromDB.ImageURL = product.ImageURL;
                //}
            }
        }
    }
}
