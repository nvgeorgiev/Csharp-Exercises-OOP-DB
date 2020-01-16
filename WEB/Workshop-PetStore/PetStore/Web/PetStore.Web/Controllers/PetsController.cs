namespace PetStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Pets;
    using Services;

    public class PetsController : Controller
    {
        private readonly IPetService pets;

        public PetsController(IPetService pets)
            => this.pets = pets;

        public IActionResult All(int page = 1) 
        {
            var allPets = this.pets.All(page);

            var model = new AllPetsViewModel
            {
                Pets = allPets,
                CurrentPage = page
            };

            return View(model);
        }
    }
}
