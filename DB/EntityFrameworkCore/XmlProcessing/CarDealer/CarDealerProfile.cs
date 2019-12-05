namespace CarDealer
{
    using System.Linq;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;

    using AutoMapper;
    using CarDealer.Dtos.Export;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            // Import Dtos
            this.CreateMap<ImportSupplierDto, Supplier>();
            this.CreateMap<ImportPartDto, Part>();

            // Export Dtos
            this.CreateMap<Part, ExportCarPartDto>();

            this.CreateMap<Car, ExportCarDto>()
                .ForMember(x => x.Parts, y => y.MapFrom(x => x.PartCars.Select(pc => pc.Part)));

            this.CreateMap<Supplier, ExportLocalSuppliersDto>()
                .ForMember(x => x.PartsCount, y => y.MapFrom(x => x.Parts.Count));
        }
    }
}
