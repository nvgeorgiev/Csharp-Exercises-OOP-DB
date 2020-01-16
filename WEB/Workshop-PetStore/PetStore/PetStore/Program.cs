namespace PetStore
{
    using PetStore.Data;
    using PetStore.Data.Models;
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            using (var data = new PetStoreDbContext())
            {

                for (int i = 0; i < 10; i++)
                {
                    var breed = new Breed()
                    {
                        Name = "Breed" + i
                    };

                    data.Breeds.Add(breed);
                }

                data.SaveChanges();

                for (int i = 0; i < 30; i++)
                {
                    var category = new Category()
                    {
                        Name = "Category" + i,
                        Description = "Category description" + 1
                    };

                    data.Categories.Add(category);
                }

                data.SaveChanges();

                for (int i = 0; i < 100; i++)
                {
                    var breedId = data
                       .Breeds
                       .OrderBy(b => Guid.NewGuid()) // Generates random Ids 
                       .Select(b => b.Id)
                       .FirstOrDefault();

                    var categoryId = data
                       .Categories
                       .OrderBy(c => Guid.NewGuid())
                       .Select(c => c.Id)
                       .FirstOrDefault();
                     
                    var pet = new Pet()
                    {
                        DateOfBirth = DateTime.UtcNow.AddDays(-60),
                        Price = 50 + i,
                        Gender = (Gender)(i % 2),
                        Description = "Some random text here" + i,
                        CategoryId = categoryId,
                        BreedId = breedId
                    };

                    data.Pets.Add(pet);
                }

                data.SaveChanges();
            }
        }
    }
}
