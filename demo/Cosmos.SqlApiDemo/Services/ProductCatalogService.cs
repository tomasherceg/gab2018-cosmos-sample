using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmos.SqlApiDemo.Model;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Cosmos.SqlApiDemo.Services
{
    public class ProductCatalogService
    {
        private readonly DocumentClient client;


        private const string DatabaseName = "GabDemo";
        private const string CollectionName = "Products";


        public ProductCatalogService(DocumentClient client)
        {
            this.client = client;
        }

        public async Task<QueryResult<Product>> ListProducts(string continuationToken = null)
        {
            //await SeedProducts();

            var collectionUri = UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName);

            var options = new FeedOptions()
            {
                MaxItemCount = 20,
                RequestContinuation = continuationToken
            };

            var query = client.CreateDocumentQuery<Product>(collectionUri, options)
                .Where(p => p.DeletedDate == null)
                .OrderBy(p => p.Name)
                .AsDocumentQuery();

            var results = new List<Product>();
            var response = await query.ExecuteNextAsync<Product>();
            foreach (var p in response)
            {
                results.Add(p);
            }

            return new QueryResult<Product>(results, response.ResponseContinuation);
        }

        private async Task SeedProducts()
        {
            var products = new[]
            {
                new Product() { Name = "Chai", QuantityPerUnit = "10 boxes x 20 bags", Categories = { "Beverages" }, Prices = { new ProductPrice() { Amount = 18.00m, Currency = "EUR" }, new ProductPrice() { Amount = 450.00m, Currency = "CZK" } } },
                new Product() { Name = "Chang", QuantityPerUnit = "24 - 12 oz bottles", Categories = { "Beverages" }, Prices = { new ProductPrice() { Amount = 19.00m, Currency = "EUR" }, new ProductPrice() { Amount = 475.00m, Currency = "CZK" } } },
                new Product() { Name = "Aniseed Syrup", QuantityPerUnit = "12 - 550 ml bottles", Categories = { "Condiments" }, Prices = { new ProductPrice() { Amount = 10.00m, Currency = "EUR" }, new ProductPrice() { Amount = 250.00m, Currency = "CZK" } } },
                new Product() { Name = "Chef Anton's Cajun Seasoning", QuantityPerUnit = "48 - 6 oz jars", Categories = { "Condiments" }, Prices = { new ProductPrice() { Amount = 22.00m, Currency = "EUR" }, new ProductPrice() { Amount = 550.00m, Currency = "CZK" } } },
                new Product() { Name = "Chef Anton's Gumbo Mix", QuantityPerUnit = "36 boxes", Categories = { "Condiments" }, Prices = { new ProductPrice() { Amount = 21.35m, Currency = "EUR" }, new ProductPrice() { Amount = 533.75m, Currency = "CZK" } } },
                new Product() { Name = "Grandma's Boysenberry Spread", QuantityPerUnit = "12 - 8 oz jars", Categories = { "Condiments" }, Prices = { new ProductPrice() { Amount = 25.00m, Currency = "EUR" }, new ProductPrice() { Amount = 625.00m, Currency = "CZK" } } },
                new Product() { Name = "Uncle Bob's Organic Dried Pears", QuantityPerUnit = "12 - 1 lb pkgs.", Categories = { "Produce" }, Prices = { new ProductPrice() { Amount = 30.00m, Currency = "EUR" }, new ProductPrice() { Amount = 750.00m, Currency = "CZK" } } },
                new Product() { Name = "Northwoods Cranberry Sauce", QuantityPerUnit = "12 - 12 oz jars", Categories = { "Condiments" }, Prices = { new ProductPrice() { Amount = 40.00m, Currency = "EUR" }, new ProductPrice() { Amount = 1000.00m, Currency = "CZK" } } },
                new Product() { Name = "Mishi Kobe Niku", QuantityPerUnit = "18 - 500 g pkgs.", Categories = { "Meat/Poultry" }, Prices = { new ProductPrice() { Amount = 97.00m, Currency = "EUR" }, new ProductPrice() { Amount = 2425.00m, Currency = "CZK" } } },
                new Product() { Name = "Ikura", QuantityPerUnit = "12 - 200 ml jars", Categories = { "Seafood" }, Prices = { new ProductPrice() { Amount = 31.00m, Currency = "EUR" }, new ProductPrice() { Amount = 775.00m, Currency = "CZK" } } },
                new Product() { Name = "Queso Cabrales", QuantityPerUnit = "1 kg pkg.", Categories = { "Dairy Products" }, Prices = { new ProductPrice() { Amount = 21.00m, Currency = "EUR" }, new ProductPrice() { Amount = 525.00m, Currency = "CZK" } } },
                new Product() { Name = "Queso Manchego La Pastora", QuantityPerUnit = "10 - 500 g pkgs.", Categories = { "Dairy Products" }, Prices = { new ProductPrice() { Amount = 38.00m, Currency = "EUR" }, new ProductPrice() { Amount = 950.00m, Currency = "CZK" } } },
                new Product() { Name = "Konbu", QuantityPerUnit = "2 kg box", Categories = { "Seafood" }, Prices = { new ProductPrice() { Amount = 6.00m, Currency = "EUR" }, new ProductPrice() { Amount = 150.00m, Currency = "CZK" } } },
                new Product() { Name = "Tofu", QuantityPerUnit = "40 - 100 g pkgs.", Categories = { "Produce" }, Prices = { new ProductPrice() { Amount = 23.25m, Currency = "EUR" }, new ProductPrice() { Amount = 581.25m, Currency = "CZK" } } },
                new Product() { Name = "Genen Shouyu", QuantityPerUnit = "24 - 250 ml bottles", Categories = { "Condiments" }, Prices = { new ProductPrice() { Amount = 15.50m, Currency = "EUR" }, new ProductPrice() { Amount = 387.50m, Currency = "CZK" } } },
                new Product() { Name = "Pavlova", QuantityPerUnit = "32 - 500 g boxes", Categories = { "Confections" }, Prices = { new ProductPrice() { Amount = 17.45m, Currency = "EUR" }, new ProductPrice() { Amount = 436.25m, Currency = "CZK" } } },
                new Product() { Name = "Alice Mutton xx", QuantityPerUnit = "20 - 1 kg tins", Categories = { "Meat/Poultry" }, Prices = { new ProductPrice() { Amount = 39.00m, Currency = "EUR" }, new ProductPrice() { Amount = 975.00m, Currency = "CZK" } } },
                new Product() { Name = "Carnarvon Tigers", QuantityPerUnit = "16 kg pkg.", Categories = { "Seafood" }, Prices = { new ProductPrice() { Amount = 62.50m, Currency = "EUR" }, new ProductPrice() { Amount = 1562.50m, Currency = "CZK" } } },
                new Product() { Name = "Teatime Chocolate Biscuits", QuantityPerUnit = "10 boxes x 12 pieces", Categories = { "Confections" }, Prices = { new ProductPrice() { Amount = 9.20m, Currency = "EUR" }, new ProductPrice() { Amount = 230.00m, Currency = "CZK" } } },
                new Product() { Name = "Sir Rodney's Marmalade", QuantityPerUnit = "30 gift boxes", Categories = { "Confections" }, Prices = { new ProductPrice() { Amount = 81.00m, Currency = "EUR" }, new ProductPrice() { Amount = 2025.00m, Currency = "CZK" } } },
                new Product() { Name = "Sir Rodney's Scones", QuantityPerUnit = "24 pkgs. x 4 pieces", Categories = { "Confections" }, Prices = { new ProductPrice() { Amount = 10.00m, Currency = "EUR" }, new ProductPrice() { Amount = 250.00m, Currency = "CZK" } } },
                new Product() { Name = "Gustaf's Knäckebröd", QuantityPerUnit = "24 - 500 g pkgs.", Categories = { "Grains/Cereals" }, Prices = { new ProductPrice() { Amount = 21.00m, Currency = "EUR" }, new ProductPrice() { Amount = 525.00m, Currency = "CZK" } } },
                new Product() { Name = "Tunnbröd", QuantityPerUnit = "12 - 250 g pkgs.", Categories = { "Grains/Cereals" }, Prices = { new ProductPrice() { Amount = 9.00m, Currency = "EUR" }, new ProductPrice() { Amount = 225.00m, Currency = "CZK" } } },
                new Product() { Name = "Guaraná Fantástica", QuantityPerUnit = "12 - 355 ml cans", Categories = { "Beverages" }, Prices = { new ProductPrice() { Amount = 4.50m, Currency = "EUR" }, new ProductPrice() { Amount = 112.50m, Currency = "CZK" } } },
                new Product() { Name = "NuNuCa Nuß-Nougat-Creme", QuantityPerUnit = "20 - 450 g glasses", Categories = { "Confections" }, Prices = { new ProductPrice() { Amount = 14.00m, Currency = "EUR" }, new ProductPrice() { Amount = 350.00m, Currency = "CZK" } } },
                new Product() { Name = "Gumbär Gummibärchen", QuantityPerUnit = "100 - 250 g bags", Categories = { "Confections" }, Prices = { new ProductPrice() { Amount = 31.23m, Currency = "EUR" }, new ProductPrice() { Amount = 780.75m, Currency = "CZK" } } },
                new Product() { Name = "Schoggi Schokolade", QuantityPerUnit = "100 - 100 g pieces", Categories = { "Confections" }, Prices = { new ProductPrice() { Amount = 43.90m, Currency = "EUR" }, new ProductPrice() { Amount = 1097.50m, Currency = "CZK" } } },
                new Product() { Name = "Rössle Sauerkraut", QuantityPerUnit = "25 - 825 g cans", Categories = { "Produce" }, Prices = { new ProductPrice() { Amount = 45.60m, Currency = "EUR" }, new ProductPrice() { Amount = 1140.00m, Currency = "CZK" } } },
                new Product() { Name = "Thüringer Rostbratwurst", QuantityPerUnit = "50 bags x 30 sausgs.", Categories = { "Meat/Poultry" }, Prices = { new ProductPrice() { Amount = 123.79m, Currency = "EUR" }, new ProductPrice() { Amount = 3094.75m, Currency = "CZK" } } },
                new Product() { Name = "Nord-Ost Matjeshering", QuantityPerUnit = "10 - 200 g glasses", Categories = { "Seafood" }, Prices = { new ProductPrice() { Amount = 25.89m, Currency = "EUR" }, new ProductPrice() { Amount = 647.25m, Currency = "CZK" } } },
                new Product() { Name = "Gorgonzola Telino", QuantityPerUnit = "12 - 100 g pkgs", Categories = { "Dairy Products" }, Prices = { new ProductPrice() { Amount = 12.50m, Currency = "EUR" }, new ProductPrice() { Amount = 312.50m, Currency = "CZK" } } },
                new Product() { Name = "Mascarpone Fabioli", QuantityPerUnit = "24 - 200 g pkgs.", Categories = { "Dairy Products" }, Prices = { new ProductPrice() { Amount = 32.00m, Currency = "EUR" }, new ProductPrice() { Amount = 800.00m, Currency = "CZK" } } },
                new Product() { Name = "Geitost", QuantityPerUnit = "500 g", Categories = { "Dairy Products" }, Prices = { new ProductPrice() { Amount = 2.50m, Currency = "EUR" }, new ProductPrice() { Amount = 62.50m, Currency = "CZK" } } },
                new Product() { Name = "Sasquatch Ale", QuantityPerUnit = "24 - 12 oz bottles", Categories = { "Beverages" }, Prices = { new ProductPrice() { Amount = 14.00m, Currency = "EUR" }, new ProductPrice() { Amount = 350.00m, Currency = "CZK" } } },
                new Product() { Name = "Steeleye Stout", QuantityPerUnit = "24 - 12 oz bottles", Categories = { "Beverages" }, Prices = { new ProductPrice() { Amount = 18.00m, Currency = "EUR" }, new ProductPrice() { Amount = 450.00m, Currency = "CZK" } } },
                new Product() { Name = "Inlagd Sill", QuantityPerUnit = "24 - 250 g  jars", Categories = { "Seafood" }, Prices = { new ProductPrice() { Amount = 19.00m, Currency = "EUR" }, new ProductPrice() { Amount = 475.00m, Currency = "CZK" } } },
                new Product() { Name = "Gravad lax", QuantityPerUnit = "12 - 500 g pkgs.", Categories = { "Seafood" }, Prices = { new ProductPrice() { Amount = 26.00m, Currency = "EUR" }, new ProductPrice() { Amount = 650.00m, Currency = "CZK" } } },
                new Product() { Name = "Côte de Blaye", QuantityPerUnit = "12 - 75 cl bottles", Categories = { "Beverages" }, Prices = { new ProductPrice() { Amount = 263.50m, Currency = "EUR" }, new ProductPrice() { Amount = 6587.50m, Currency = "CZK" } } },
                new Product() { Name = "Chartreuse verte", QuantityPerUnit = "750 cc per bottle", Categories = { "Beverages" }, Prices = { new ProductPrice() { Amount = 18.00m, Currency = "EUR" }, new ProductPrice() { Amount = 450.00m, Currency = "CZK" } } },
                new Product() { Name = "Boston Crab Meat", QuantityPerUnit = "24 - 4 oz tins", Categories = { "Seafood" }, Prices = { new ProductPrice() { Amount = 18.40m, Currency = "EUR" }, new ProductPrice() { Amount = 460.00m, Currency = "CZK" } } },
                new Product() { Name = "Jack's New England Clam Chowder", QuantityPerUnit = "12 - 12 oz cans", Categories = { "Seafood" }, Prices = { new ProductPrice() { Amount = 9.65m, Currency = "EUR" }, new ProductPrice() { Amount = 241.25m, Currency = "CZK" } } },
                new Product() { Name = "Singaporean Hokkien Fried Mee", QuantityPerUnit = "32 - 1 kg pkgs.", Categories = { "Grains/Cereals" }, Prices = { new ProductPrice() { Amount = 14.00m, Currency = "EUR" }, new ProductPrice() { Amount = 350.00m, Currency = "CZK" } } },
                new Product() { Name = "Ipoh Coffee", QuantityPerUnit = "16 - 500 g tins", Categories = { "Beverages" }, Prices = { new ProductPrice() { Amount = 46.00m, Currency = "EUR" }, new ProductPrice() { Amount = 1150.00m, Currency = "CZK" } } },
                new Product() { Name = "Gula Malacca", QuantityPerUnit = "20 - 2 kg bags", Categories = { "Condiments" }, Prices = { new ProductPrice() { Amount = 19.45m, Currency = "EUR" }, new ProductPrice() { Amount = 486.25m, Currency = "CZK" } } },
                new Product() { Name = "Rogede sild", QuantityPerUnit = "1k pkg.", Categories = { "Seafood" }, Prices = { new ProductPrice() { Amount = 9.50m, Currency = "EUR" }, new ProductPrice() { Amount = 237.50m, Currency = "CZK" } } },
                new Product() { Name = "Spegesild", QuantityPerUnit = "4 - 450 g glasses", Categories = { "Seafood" }, Prices = { new ProductPrice() { Amount = 12.00m, Currency = "EUR" }, new ProductPrice() { Amount = 300.00m, Currency = "CZK" } } },
                new Product() { Name = "Zaanse koeken", QuantityPerUnit = "10 - 4 oz boxes", Categories = { "Confections" }, Prices = { new ProductPrice() { Amount = 9.50m, Currency = "EUR" }, new ProductPrice() { Amount = 237.50m, Currency = "CZK" } } },
                new Product() { Name = "Chocolade", QuantityPerUnit = "10 pkgs.", Categories = { "Confections" }, Prices = { new ProductPrice() { Amount = 12.75m, Currency = "EUR" }, new ProductPrice() { Amount = 318.75m, Currency = "CZK" } } },
                new Product() { Name = "Maxilaku", QuantityPerUnit = "24 - 50 g pkgs.", Categories = { "Confections" }, Prices = { new ProductPrice() { Amount = 20.00m, Currency = "EUR" }, new ProductPrice() { Amount = 500.00m, Currency = "CZK" } } },
                new Product() { Name = "Valkoinen suklaa", QuantityPerUnit = "12 - 100 g bars", Categories = { "Confections" }, Prices = { new ProductPrice() { Amount = 16.25m, Currency = "EUR" }, new ProductPrice() { Amount = 406.25m, Currency = "CZK" } } },
                new Product() { Name = "Manjimup Dried Apples", QuantityPerUnit = "50 - 300 g pkgs.", Categories = { "Produce" }, Prices = { new ProductPrice() { Amount = 53.00m, Currency = "EUR" }, new ProductPrice() { Amount = 1325.00m, Currency = "CZK" } } },
                new Product() { Name = "Filo Mix", QuantityPerUnit = "16 - 2 kg boxes", Categories = { "Grains/Cereals" }, Prices = { new ProductPrice() { Amount = 7.00m, Currency = "EUR" }, new ProductPrice() { Amount = 175.00m, Currency = "CZK" } } },
                new Product() { Name = "Perth Pasties", QuantityPerUnit = "48 pieces", Categories = { "Meat/Poultry" }, Prices = { new ProductPrice() { Amount = 32.80m, Currency = "EUR" }, new ProductPrice() { Amount = 820.00m, Currency = "CZK" } } },
                new Product() { Name = "Tourtiere", QuantityPerUnit = "16 pies", Categories = { "Meat/Poultry" }, Prices = { new ProductPrice() { Amount = 7.45m, Currency = "EUR" }, new ProductPrice() { Amount = 186.25m, Currency = "CZK" } } },
                new Product() { Name = "Pâté chinois", QuantityPerUnit = "24 boxes x 2 pies", Categories = { "Meat/Poultry" }, Prices = { new ProductPrice() { Amount = 24.00m, Currency = "EUR" }, new ProductPrice() { Amount = 600.00m, Currency = "CZK" } } },
                new Product() { Name = "Gnocchi di nonna Alice", QuantityPerUnit = "24 - 250 g pkgs.", Categories = { "Grains/Cereals" }, Prices = { new ProductPrice() { Amount = 38.00m, Currency = "EUR" }, new ProductPrice() { Amount = 950.00m, Currency = "CZK" } } },
                new Product() { Name = "Ravioli Angelo", QuantityPerUnit = "24 - 250 g pkgs.", Categories = { "Grains/Cereals" }, Prices = { new ProductPrice() { Amount = 19.50m, Currency = "EUR" }, new ProductPrice() { Amount = 487.50m, Currency = "CZK" } } },
                new Product() { Name = "Escargots de Bourgogne", QuantityPerUnit = "24 pieces", Categories = { "Seafood" }, Prices = { new ProductPrice() { Amount = 13.25m, Currency = "EUR" }, new ProductPrice() { Amount = 331.25m, Currency = "CZK" } } },
                new Product() { Name = "Raclette Courdavault", QuantityPerUnit = "5 kg pkg.", Categories = { "Dairy Products" }, Prices = { new ProductPrice() { Amount = 55.00m, Currency = "EUR" }, new ProductPrice() { Amount = 1375.00m, Currency = "CZK" } } },
                new Product() { Name = "Camembert Pierrot", QuantityPerUnit = "15 - 300 g rounds", Categories = { "Dairy Products" }, Prices = { new ProductPrice() { Amount = 34.00m, Currency = "EUR" }, new ProductPrice() { Amount = 850.00m, Currency = "CZK" } } },
                new Product() { Name = "Sirop d'érable", QuantityPerUnit = "24 - 500 ml bottles", Categories = { "Condiments" }, Prices = { new ProductPrice() { Amount = 28.50m, Currency = "EUR" }, new ProductPrice() { Amount = 712.50m, Currency = "CZK" } } },
                new Product() { Name = "Tarte au sucre", QuantityPerUnit = "48 pies", Categories = { "Confections" }, Prices = { new ProductPrice() { Amount = 49.30m, Currency = "EUR" }, new ProductPrice() { Amount = 1232.50m, Currency = "CZK" } } },
                new Product() { Name = "Vegie-spread", QuantityPerUnit = "15 - 625 g jars", Categories = { "Condiments" }, Prices = { new ProductPrice() { Amount = 43.90m, Currency = "EUR" }, new ProductPrice() { Amount = 1097.50m, Currency = "CZK" } } },
                new Product() { Name = "Wimmers gute Semmelknödel", QuantityPerUnit = "20 bags x 4 pieces", Categories = { "Grains/Cereals" }, Prices = { new ProductPrice() { Amount = 33.25m, Currency = "EUR" }, new ProductPrice() { Amount = 831.25m, Currency = "CZK" } } },
                new Product() { Name = "Louisiana Fiery Hot Pepper Sauce", QuantityPerUnit = "32 - 8 oz bottles", Categories = { "Condiments" }, Prices = { new ProductPrice() { Amount = 21.05m, Currency = "EUR" }, new ProductPrice() { Amount = 526.25m, Currency = "CZK" } } },
                new Product() { Name = "Louisiana Hot Spiced Okra", QuantityPerUnit = "24 - 8 oz jars", Categories = { "Condiments" }, Prices = { new ProductPrice() { Amount = 17.00m, Currency = "EUR" }, new ProductPrice() { Amount = 425.00m, Currency = "CZK" } } },
                new Product() { Name = "Laughing Lumberjack Lager", QuantityPerUnit = "24 - 12 oz bottles", Categories = { "Beverages" }, Prices = { new ProductPrice() { Amount = 14.00m, Currency = "EUR" }, new ProductPrice() { Amount = 350.00m, Currency = "CZK" } } },
                new Product() { Name = "Scottish Longbreads", QuantityPerUnit = "10 boxes x 8 pieces", Categories = { "Confections" }, Prices = { new ProductPrice() { Amount = 12.50m, Currency = "EUR" }, new ProductPrice() { Amount = 312.50m, Currency = "CZK" } } },
                new Product() { Name = "Gudbrandsdalsost", QuantityPerUnit = "10 kg pkg.", Categories = { "Dairy Products" }, Prices = { new ProductPrice() { Amount = 36.00m, Currency = "EUR" }, new ProductPrice() { Amount = 900.00m, Currency = "CZK" } } },
                new Product() { Name = "Outback Lager", QuantityPerUnit = "24 - 355 ml bottles", Categories = { "Beverages" }, Prices = { new ProductPrice() { Amount = 15.00m, Currency = "EUR" }, new ProductPrice() { Amount = 375.00m, Currency = "CZK" } } },
                new Product() { Name = "Flotemysost", QuantityPerUnit = "10 - 500 g pkgs.", Categories = { "Dairy Products" }, Prices = { new ProductPrice() { Amount = 21.50m, Currency = "EUR" }, new ProductPrice() { Amount = 537.50m, Currency = "CZK" } } },
                new Product() { Name = "Mozzarella di Giovanni", QuantityPerUnit = "24 - 200 g pkgs.", Categories = { "Dairy Products" }, Prices = { new ProductPrice() { Amount = 34.80m, Currency = "EUR" }, new ProductPrice() { Amount = 870.00m, Currency = "CZK" } } },
                new Product() { Name = "Röd Kaviar", QuantityPerUnit = "24 - 150 g jars", Categories = { "Seafood" }, Prices = { new ProductPrice() { Amount = 15.00m, Currency = "EUR" }, new ProductPrice() { Amount = 375.00m, Currency = "CZK" } } },
                new Product() { Name = "Longlife Tofu", QuantityPerUnit = "5 kg pkg.", Categories = { "Produce" }, Prices = { new ProductPrice() { Amount = 10.00m, Currency = "EUR" }, new ProductPrice() { Amount = 250.00m, Currency = "CZK" } } },
                new Product() { Name = "Rhönbräu Klosterbier", QuantityPerUnit = "24 - 0.5 l bottles", Categories = { "Beverages" }, Prices = { new ProductPrice() { Amount = 7.75m, Currency = "EUR" }, new ProductPrice() { Amount = 193.75m, Currency = "CZK" } } },
                new Product() { Name = "Lakkalikööri", QuantityPerUnit = "500 ml", Categories = { "Beverages" }, Prices = { new ProductPrice() { Amount = 18.00m, Currency = "EUR" }, new ProductPrice() { Amount = 450.00m, Currency = "CZK" } } },
                new Product() { Name = "Original Frankfurter grüne Soße", QuantityPerUnit = "12 boxes", Categories = { "Condiments" }, Prices = { new ProductPrice() { Amount = 13.00m, Currency = "EUR" }, new ProductPrice() { Amount = 325.00m, Currency = "CZK" } } },
            };

            foreach (var product in products)
            {
                await SaveProduct(product);
            }

        }

        public async Task<Product> GetProduct(string id)
        {
            var documentUri = UriFactory.CreateDocumentUri(DatabaseName, CollectionName, id);
            return (await client.ReadDocumentAsync<Product>(documentUri)).Document;
        }

        public async Task SaveProduct(Product product)
        {
            var collectionUri = UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName);
            await client.UpsertDocumentAsync(collectionUri, product);
        }

        public async Task DeleteProduct(string id)
        {
            var product = await GetProduct(id);
            product.DeletedDate = DateTime.UtcNow;
            await SaveProduct(product);
        }


    }
}
            