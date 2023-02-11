﻿namespace OnlineFoodShop.Shop.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public IEnumerable<FoodProducts> FoodProducts { get; set; }
    }
}
