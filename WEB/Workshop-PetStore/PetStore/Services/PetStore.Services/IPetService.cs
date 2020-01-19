namespace PetStore.Services
{
    using Data.Models;
    using Services.Models.Pet;
    using System;
    using System.Collections.Generic;

    public interface IPetService
    {
        IEnumerable<PetListingServiceModel> All(int page = 1);

        PetDetailsServiceModel Details(int id);

        void BuyPet(Gender gender, DateTime dateTime, decimal price, 
            string description, int breedId, int categoryId);

        void SellPet(int petId, int userId);

        bool Exists(int petId);

        int Total();

        bool Delete(int id);
    }
}
