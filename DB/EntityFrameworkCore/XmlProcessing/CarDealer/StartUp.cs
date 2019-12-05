namespace CarDealer
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using CarDealer.Data;
    using CarDealer.Dtos.Export;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());

            using (var db = new CarDealerContext())
            {
                // db.Database.EnsureDeleted();
                // db.Database.EnsureCreated();

                // var inputXml = File.ReadAllText("./../../../Datasets/cars.xml");

                var result = GetCarsWithTheirListOfParts(db);

                Console.WriteLine(result);
            }
        }

        // Query 9. Import Suppliers
        // Import the suppliers from the provided file suppliers.xml. 
        // Your method should return string with message $"Successfully imported {suppliers.Count}";
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportSupplierDto[]),
                new XmlRootAttribute("Suppliers"));

            ImportSupplierDto[] supplierDtos;

            using (var reader = new StringReader(inputXml))
            {
                supplierDtos = (ImportSupplierDto[])xmlSerializer.Deserialize(reader);
            }

            var suppliers = Mapper.Map<Supplier[]>(supplierDtos);

            context.AddRange(suppliers);
            //context.SaveChanges();

            return $"Successfully imported {suppliers.Length}";
        }

        // Query 10. Import Parts
        // Import the parts from the provided file parts.xml. If the supplierId doesn’t exists, skip the record.
        // Your method should return string with message $"Successfully imported {parts.Count}";
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportPartDto[]), new XmlRootAttribute("Parts"));

            ImportPartDto[] partDtos;

            using (var reader = new StringReader(inputXml))
            {
                partDtos = ((ImportPartDto[])xmlSerializer.Deserialize(reader))
                    .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
                    .ToArray();
            }

            var parts = Mapper.Map<Part[]>(partDtos);

            context.AddRange(parts);
            //context.SaveChanges();

            return $"Successfully imported {parts.Length}";
        }

        // Query 11. Import Cars
        // Import the cars from the provided file cars.xml.Select unique car part ids. 
        // If the part id doesn’t exists, skip the part record.
        // Your method should return string with message $"Successfully imported {cars.Count}";
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportCarDto[]),
                new XmlRootAttribute("Cars"));

            ImportCarDto[] carDtos;

            using (var reader = new StringReader(inputXml))
            {
                carDtos = ((ImportCarDto[])xmlSerializer.Deserialize(reader));
            }

            List<Car> cars = new List<Car>();
            List<PartCar> partCars = new List<PartCar>();

            foreach (var carDto in carDtos)
            {
                var car = new Car()
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TravelledDistance
                };

                var parts = carDto
                    .Parts
                    .Where(pdto => context.Parts.Any(p => p.Id == pdto.Id))
                    .Select(p => p.Id)
                    .Distinct();

                foreach (var partId in parts)
                {
                    var partCar = new PartCar()
                    {
                        PartId = partId,
                        Car = car
                    };

                    partCars.Add(partCar);
                }

                cars.Add(car);
            }
            context.AddRange(cars);
            context.AddRange(partCars);

            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        // Query 16. Local Suppliers
        // Get all suppliers that do not import parts from abroad. 
        // Get their id, name and the number of parts they can offer to supply. 
        // Return the list of suppliers to XML in the format provided below.
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            var suppliers = context
                .Suppliers
                .Where(s => !s.IsImporter)
                .ProjectTo<ExportLocalSuppliersDto>()
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportLocalSuppliersDto[]),
                new XmlRootAttribute("suppliers"));

            // Removes the namespaces on top of the xml document. Example: 
            // <suppliers xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(String.Empty, String.Empty);

            using (var writer = new StringWriter(sb))
            {
                xmlSerializer.Serialize(writer, suppliers, namespaces);
            }

            return sb.ToString().TrimEnd();
        }

        // Query 17. Cars with Their List of Parts
        // Get all cars along with their list of parts.
        // For the car get only make, model and travelled distance 
        // and for the parts get only name and price and sort all parts by price(descending).
        // Sort all cars by travelled distance(descending) then by model(ascending). Select top 5 records.
        // Return the list of suppliers to XML in the format provided below.
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();

            var cars = context
                .Cars
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ProjectTo<ExportCarDto>()
                .ToArray();

            foreach (var car in cars)
            {
                car.Parts = car.Parts
                    .OrderByDescending(p => p.Price)
                    .ToArray();
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportCarDto[]), new XmlRootAttribute("cars"));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);

            using (var writer = new StringWriter(sb))
            {
                xmlSerializer.Serialize(writer, cars, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}