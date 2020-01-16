namespace PetStore.Services.Implementations
{
    using Data;
    using Data.Models;
    using Services.Models.Food;
    using System;
    using System.Linq;

    public class FoodService : IFoodService
    {
        private readonly PetStoreDbContext data;
        private readonly IUserService userService;

        public FoodService(PetStoreDbContext data, IUserService userService)
        {
            this.data = data;
            this.userService = userService;
        }

        public void BuyFromDistributor(string name, double weigth, decimal price, decimal profit,
            DateTime expirationDate, int brandId, int categoryId)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or whitespace!");
            }            

            // Profit should be in range 0 - 500% 
            if (profit < 0 || profit > 5)
            {
                throw new ArgumentException("Profit must be higher than zero and lower than 500%");
            }

            var food = new Food()
            {
                Name = name,
                Weight = weigth,
                DistributorPrice = price,
                Price = price + (price * profit),
                ExpirationDate = expirationDate,
                BrandId = brandId,
                CategoryId = categoryId
            };

            this.data.Food.Add(food);
            this.data.SaveChanges();
        }

        public void BuyFromDistributor(AddingFoodServiceModel model)
        {
            var food = new Food()
            {
                Name = model.Name,
                Weight = model.Weight,
                DistributorPrice = model.Price,
                Price = model.Price + (model.Price * model.Profit),
                BrandId = model.BrandId,
                CategoryId = model.CategoryId
            };

            this.data.Food.Add(food);
            this.data.SaveChanges();
        }

        public void SellFoodToUser(int foodId, int userId)
        {
            if (!this.data.Food.Any(f => f.Id == foodId))
            {
                throw new ArgumentException("There is no such food with the given Id in the database!");
            }

            if (!this.userService.Exists(userId))
            {
                throw new ArgumentException("There is no such user with the given Id in the database!");
            }

            var order = new Order() 
            {
                PurchaseDate = DateTime.Now,
                Status = OrderStatus.Done,
                UserId = userId
            };

            var foodOrder = new FoodOrder()
            {
                FoodId = foodId,
                Order = order
            };

            this.data.Orders.Add(order);
            this.data.FoodOrders.Add(foodOrder);

            this.data.SaveChanges();
        }
    }
}
