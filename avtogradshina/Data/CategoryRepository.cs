using avtogradshina.Models;
using avtogradshina.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using avtogradshina.Models.Pages;

namespace avtogradshina.Models.Admin
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        PagedList<Category> GetCategories(QueryOptions options);

        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationContext context;
        public CategoryRepository(ApplicationContext ctx) => context = ctx;
        public IEnumerable<Category> Categories => context.Categories;
        public PagedList<Category> GetCategories(QueryOptions options) {
         return new PagedList<Category>(context.Categories, options);
        }
        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }
        public void UpdateCategory(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
        }
        public void DeleteCategory(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }
    }
}