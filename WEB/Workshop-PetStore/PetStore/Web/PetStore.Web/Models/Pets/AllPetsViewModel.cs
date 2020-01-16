namespace PetStore.Web.Models.Pets
{
    using Services.Models.Pet;
    using System.Collections.Generic;

    public class AllPetsViewModel
    {
        public IEnumerable<PetListingServiceModel> Pets { get; set; }

        public int CurrentPage { get; set; }
    }
}
