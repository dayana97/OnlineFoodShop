﻿
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace OnlineFoodShop.Shop.Data.Models
{
    public class ShoppingCard
    {
        
         private readonly ApplicationDbContext _context;

         public ShoppingCard(ApplicationDbContext context)
         {
             _context = context;
         }

        public int Id { get; set; }
        public IEnumerable<ShoppingCardItem> ShoppingCardItems { get; set; }

        
        public static ShoppingCard GetCard(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();
            string cardId = session.GetString("CardId") ?? Guid.NewGuid().ToString();
            session.SetString("CardId", cardId);

            return new ShoppingCard(context) { Id = cardId };
        }


        public bool AddToCard(FoodProducts foodProducts,int Amout)
        {
            if(foodProducts.InStock == 0 || Amout == 0)
            {
                return false;
            }

            //връзката на базата данни и продуктите
            var shoppingCardItems = _context.ShoppingCardItems.SingleOrDefault(s => s.FoodProducts.Id == foodProducts.Id &&
            s.ShoppingCardId == Id);
            //променлива за наличност на количеството продукти
            var isValidAmount = true;
            //проверка дали имаме продукти в количката
            if (shoppingCardItems == null)
            {
                //ако количеството което се иска да се поръча е повече от наличното
                //в базата да дава грешка
                if (Amout > foodProducts.InStock)
                {
                    isValidAmount = false;
                }
                //по този начин се прави референцията е паметта да е вързана за обекта
                //с пълната информация в кошницата ни.
                shoppingCardItems = new ShoppingCardItem
                {
                    //какво ще виждаме в количката
                    ShoppingCardId = Id,
                    FoodProducts = foodProducts,
                    Amount = Math.Min(foodProducts.InStock, Amout)
                };
                //добавя информацията от shoppingCard в базата от данни
                _context.ShoppingCardItems.Add(shoppingCardItems);
            }else
            {
            if(foodProducts.InStock - shoppingCardItems.Amount - Amout>= 0)
                {
                    shoppingCardItems.Amount += Amout;
                }
                else
                {
                    shoppingCardItems.Amount += (foodProducts.InStock - shoppingCardItems.Amount);
                    isValidAmount = false;
                }
            }

            _context.SaveChanges();
            return isValidAmount;
        }

        public int RemoveFromCard(FoodProducts foodProducts)
        {
            //връзката на базата данни и продуктите
            var shoppingCardItems = _context.ShoppingCardItems.SingleOrDefault(s => s.FoodProducts.Id == foodProducts.Id &&
            s.ShoppingCardId == Id);
            int localAmount = 0;
            if(shoppingCardItems != null)
            {
                if(shoppingCardItems.Amount > 1)
                {
                    shoppingCardItems.Amount--;
                    localAmount = shoppingCardItems.Amount;
                }
                else
                {
                    _context.ShoppingCardItems.Remove(shoppingCardItems);
                }
            }
            _context.SaveChanges();
            return localAmount;
        }

        //Vruzkata na kolichkata ni i bazata danni za proverka i dobavqne direktno tam 
        //kudeto imame id
        public IEnumerable<ShoppingCardItem> GetShoppingCardItems()
        {
            return ShoppingCardItems ??
                (ShoppingCardItems = _context.ShoppingCardItems.Where(c => c.ShoppingCardId == Id)
                .Include(s => s.FoodProducts));
        }

        public void ClearCard()
        {
            var cardItems = _context.ShoppingCardItems.Where(c => c.ShoppingCardId == Id);
            _context.ShoppingCardItems.RemoveRange(cardItems);
            _context.SaveChanges();
        }
          
        //sumata na kolichkata obshto s kolichestvo, cena i producti 
        public decimal GetShoppingCardTotal()
        {
            return _context.ShoppingCardItems.Where(c =>c.ShoppingCardId == Id)
                .Select(c => c.FoodProducts.Price * c.Amount).Sum();
        }
        }
    }


