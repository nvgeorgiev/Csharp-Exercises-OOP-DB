namespace ProductShop
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using ProductShop.Data;
    using ProductShop.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new ProductShopContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                // We need the file reader when importin from json to C#
                // var inputJson = File.ReadAllText("./../../../Datasets/categories-products.json");

                var result = GetSoldProducts(db);

                Console.WriteLine(result);
            }
        }

        // Query 1. Import Users
        // Import the users from the provided file users.json.
        // Your method should return string with message $"Successfully imported {Users.Count}";
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);

            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        // Query 2. Import Products
        // Import the products from the provided file products.json.
        // Your method should return string with message $"Successfully imported {Products.Count}";
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);

            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        // Query 3. Import Categories
        // Import the users from the provided file categories.json.Some of the names will be null, 
        // so you don’t have to add them in the database. Just skip the record and continue.
        // Your method should return string with message $"Successfully imported {Categories.Count}";
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<Category[]>(inputJson)
                .Where(c => c.Name != null)
                .ToArray();

            context.Categories.AddRange(categories);

            context.SaveChanges();

            return $"Successfully imported {categories.Length}";
        }

        // Query 4. Import Categories and Products
        // Import the users from the provided file categories-products.json. 
        // Your method should return string with message $"Successfully imported {CategoryProducts.Count}";
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoriesProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoryProducts.AddRange(categoriesProducts);

            context.SaveChanges();

            return $"Successfully imported {categoriesProducts.Length}";
        }

        // Query 5. Export Products In Range
        // Get all products in a specified price range:  500 to 1000 (inclusive). Order them by price(from lowest to highest). 
        // Select only the product name, price and the full name of the seller.Export the result to JSON.
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                })
                .ToList();

            var productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);

            return productsJson;
        }

        // Query 6. Export Successfully Sold Products
        // Get all users who have at least 1 sold item with a buyer.
        // Order them by last name, then by first name. Select the person's first and last name. 
        // For each of the sold products (products with buyers), 
        // select the product's name, price and the buyer's first and last name.
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                                    .Where(p => p.Buyer != null)
                                    .Select(p => new
                                    {
                                        name = p.Name,
                                        price = p.Price,
                                        buyerFirstName = p.Buyer.FirstName,
                                        buyerLastName = p.Buyer.LastName
                                    })
                                    .ToList()
                })
                .ToList();

            var json = JsonConvert.SerializeObject(users, Formatting.Indented);


            return json;
        }

        // Query 7. Export Categories By Products Count
        // Get all users who have at least 1 sold product with a buyer.
        // Order them in descending order by the number of sold products with a buyer.
        // Select only their first and last name, age and for each product - name and price.Ignore all null values.
        // Export the results to JSON.Follow the format below to better understand how to structure your data.
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var exportCategories = context.Categories
                .OrderByDescending(c => c.CategoryProducts.Count())
                .Select(c => new CategoriesDto
                {
                    Category = c.Name,
                    ProductsCount = c.CategoryProducts.Count(),
                    AveragePrice = $@"{c.CategoryProducts
                    .Sum(p => p.Product.Price) / c.CategoryProducts.Count():F2}",
                    TotalRevenue = $"{c.CategoryProducts.Sum(p => p.Product.Price):F2}"
                })
                .ToList();

            var json = JsonConvert.SerializeObject(exportCategories, Formatting.Indented);

            return json;
        }



        // Query 8. Export Users and Products
        // Get all users who have at least 1 sold product with a buyer. 
        // Order them in descending order by the number of sold products with a buyer. 
        // Select only their first and last name, age and for each product - name and price. Ignore all null values.
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.ProductsSold
                                     .Where(p => p.Buyer != null)
                                     .Count(),
                        products = u.ProductsSold
                                    .Where(p => p.Buyer != null)
                                    .Select(ps => new
                                    {
                                        name = ps.Name,
                                        price = ps.Price
                                    })
                                    .ToList()
                    }
                })
                .OrderByDescending(u => u.soldProducts.count)
                .ToList();

            var userOutput = new
            {
                usersCount = users.Count,
                users = users
            };

            var json = JsonConvert.SerializeObject(userOutput, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            return json;
        }
    }
}