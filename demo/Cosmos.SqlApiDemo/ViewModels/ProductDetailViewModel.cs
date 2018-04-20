using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.SqlApiDemo.Model;
using Cosmos.SqlApiDemo.Services;
using DotVVM.Framework.ViewModel;

namespace Cosmos.SqlApiDemo.ViewModels
{
    public class ProductDetailViewModel : MasterPageViewModel
    {
        private readonly ProductCatalogService productCatalogService;


        [FromRoute("id")]
        public string Id { get; set; }

        public bool IsNew => string.IsNullOrEmpty(Id);

        public Product Product { get; set; }


        [Bind(Direction.ServerToClientFirstRequest)]
        public List<string> Categories => new List<string>()
        {
            "Beverages",
            "Condiments",
            "Sweets",
            "Food"
        };

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<string> Currencies => new List<string>()
        {
            "EUR",
            "USD",
            "CZK"
        };


        public ProductDetailViewModel(ProductCatalogService productCatalogService)
        {
            this.productCatalogService = productCatalogService;
        }


        public override async Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                if (IsNew)
                {
                    Product = new Product()
                    {
                        Prices =
                        {
                            new ProductPrice()
                        }
                    };
                }
                else
                {
                    Product = await productCatalogService.GetProduct(Id);
                }
            }

            await base.PreRender();
        }


        public async Task Save()
        {
            await productCatalogService.SaveProduct(Product);
            Context.RedirectToRoute("ProductList");
        }

        public void AddPrice()
        {
            Product.Prices.Add(new ProductPrice());
        }

        public void RemovePrice(ProductPrice price)
        {
            Product.Prices.Remove(price);
        }
    }
}
