using System.Data.Common;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository(DataContext context):ICategoryRepository
    {
        public ICollection<Category> GetCategories()
        {
            try
            {
                return context.Categories.ToList();
            }
            catch (DbException ex)
            {
                throw new Exception($"Couldn't retrieve categories: {ex.Message}");
            }
        }

        public Category GetCategory(int id)
        {
            return context.Categories.Find(id);
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return context.PokemonCategories.Where(e=>e.CategoryId==categoryId).Select(c=>c.Pokemon).ToList();
        }

        public bool CategoryExists(int id)
        { 
            return context.Categories.Any(c=>c.Id==id);
        }
    }
}
