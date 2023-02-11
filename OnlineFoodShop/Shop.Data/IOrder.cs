using System;
using OnlineFoodShop.Shop.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineFoodShop.Shop.Data.Enums;

namespace OnlineFoodShop.Shop.Data
{
    public class IOrder
    {
        void CreateOrder(Order order);
        Order GetById(int orderId);

        IEnumerable<Order> GetByUserId(string userId);
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetUserLatestOrders(int count, string userId);
        IEnumerable<Order> GetUserMostPopular(string userId);
        IEnumerable<Order> GetFilteredOrders(
            string userId = null,
            OrderBy orderBy = OrderBy.None,
            int offsetForBeggining=0,
            int limit=0,
            decimal? minPrice = null,
            decimal?maxPrice= null,
            DateTime?minDate = null,
            DateTime?maxDate = null
            );

    }
}
