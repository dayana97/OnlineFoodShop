using OnlineFoodShop.Shop.Data.Models;

namespace OnlineFoodShop.Shop.Data
{
    public class IFoodProducts
    {
        IEnumerable<FoodProducts> GetAll();
        IEnumerable<FoodProducts> GetPreferred(int count);
        IEnumerable<FoodProducts> GetFoodProductsByCategoryId(int categoryId);
        IEnumerable<FoodProducts> GetFilteredFoodProducts(int id, string searchQuery);
        IEnumerable<FoodProducts> GetFilteredFoodProducts(string searchQuery);

        IFoodProducts GetById(int id);

        void NewFoodProducts(FoodProducts foodProducts);
        void EditFoodProducts(FoodProducts foodProducts);
        void DeleteFoodProducts(int id);
    }
}
