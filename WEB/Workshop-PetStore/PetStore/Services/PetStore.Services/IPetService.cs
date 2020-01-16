namespace PetStore.Services
{
    using Data.Models;
    using Services.Models.Pet;
    using System;
    using System.Collections.Generic;

    public interface IPetService
    {
        IEnumerable<PetListingServiceModel> All(int page = 1);

        void BuyPet(Gender gender, DateTime dateTime, decimal price, 
            string description, int breedId, int categoryId);

        void SellPet(int petId, int userId);

        bool Exists(int petId);
    }
}
