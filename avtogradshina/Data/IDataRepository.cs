using avtogradshina.Models.Pages;
using System.Collections.Generic;

namespace avtogradshina.Models.Admin
{
    public interface IDataRepository
    {
        IEnumerable<Product> Products { get; }

        PagedList<Product> GetProducts(QueryOptions options, long category = 0);

        Product GetProduct(long id);

        //PagedList<Product> GetProducts(QueryOptions options);

        void CreateProduct(Product newProduct);

        void UpdateProduct(Product changedProduct, Product originalProduct = null);

        void DeleteProduct(long id);
        
        
    }
}