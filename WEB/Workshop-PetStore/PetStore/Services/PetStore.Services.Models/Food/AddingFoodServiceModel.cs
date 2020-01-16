namespace PetStore.Services.Models.Food
{
    using System;

    public class AddingFoodServiceModel
    {
        public string Name { get; set; }

        public double Weight { get; set; }

        public decimal Price { get; set; }

        public decimal Profit { get; set; }

        public DateTime dateTime { get; set; }

        public int BrandId { get; set; }

        public int CategoryId { get; set; }
    }
}
