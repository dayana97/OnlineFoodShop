namespace OnlineFoodShop.Shop.Data.Models
{
    public class ShoppingCardItem
    {
        public int Id { get; set; }
        public FoodProducts FoodProducts { get; set; }
        public int Amount { get; set; }
         public string ShoppingCardId { get; set; }
    }
}
