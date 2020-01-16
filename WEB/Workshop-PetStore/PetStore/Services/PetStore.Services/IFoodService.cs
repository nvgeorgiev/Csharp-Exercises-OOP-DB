namespace PetStore.Services
{
    using Services.Models.Food;
    using System;

    public interface IFoodService
    {
        void BuyFromDistributor(string name, double weigth, decimal price, decimal profit,
            DateTime expirationDate, int brandId, int categoryId);

        void BuyFromDistributor(AddingFoodServiceModel model);

        void SellFoodToUser(int foodId, int userId);
    }
}
