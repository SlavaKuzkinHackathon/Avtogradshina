using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using avtogradshina.Models.Pages;

namespace avtogradshina.Models.Admin
{
    public class EFDataRepository : IDataRepository
    {
      
        private ApplicationContext context;
        IHostingEnvironment _appEnvironment;

        public EFDataRepository(ApplicationContext ctx, IHostingEnvironment appEnvironment)
        {
            context = ctx;
            _appEnvironment = appEnvironment;
        }

        // Привязка перечня продуктов к категории
        public IEnumerable<Product> Products => context.Products
            .Include(p => p.Category).ToArray();

         public PagedList<Product> GetProducts(QueryOptions options,
           long category = 0) {
          IQueryable<Product> query = context.Products.Include(p => p.Category);
          if (category !=- 0) {
           query = query.Where(p => p.CategoryId == category);
                 }
                    return new PagedList<Product>(query, options);
      }


        //public PagedList<Product> GetProducts(QueryOptions options)
        //{
          //  return new PagedList<Product>(context.Products
            //.Include(p => p.Category), options);
        //}

        public Product GetProduct(long id) => context.Products
          .Include(p => p.Category).First(p => p.Id == id);

        

        public void CreateProduct(Product newProduct)
        {
            newProduct.Id = 0;
            context.Products.Add(newProduct);
            context.SaveChanges();
            Console.WriteLine($"New Key: {newProduct.Id}");
        }
    

        public void UpdateProduct(Product changedProduct, Product originalProduct = null)
        {
            if (originalProduct == null)
            {
                originalProduct = context.Products.Find(changedProduct.Id);
            }
            else
            {
                context.Products.Attach(originalProduct);
            }
            originalProduct.Name = changedProduct.Name;
            originalProduct.Price = changedProduct.Price;
            originalProduct.InQuantity = changedProduct.InQuantity;
            originalProduct.NameImage = changedProduct.NameImage;
            originalProduct.Path = changedProduct.Path;
            originalProduct.CategoryId = changedProduct.CategoryId;
            context.SaveChanges();
        }

        public void DeleteProduct(long id)
        {
            context.Products.Remove(new Product { Id = id });
            context.SaveChanges();
        }

       
    }
}