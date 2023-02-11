using OnlineFoodShop.Shop.Data.Models;

namespace OnlineFoodShop.Shop.Data
{
    public class ICategory
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);

        void NewCategory(Category category);
        void EditCategory(Category category);
        void DeleteCategory(int id);
    }
}
