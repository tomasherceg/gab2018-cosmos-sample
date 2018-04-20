using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cosmos.SqlApiDemo.Model;
using Cosmos.SqlApiDemo.Services;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;

namespace Cosmos.SqlApiDemo.ViewModels
{
    public class ProductListViewModel : MasterPageViewModel
    {
        private readonly ProductCatalogService productCatalogService;

        public ProductListViewModel(ProductCatalogService productCatalogService)
        {
            this.productCatalogService = productCatalogService;
        }

        public List<Product> Products { get; set; } = new List<Product>();

        public string ContinuationToken { get; set; }


        public override async Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                await LoadData();
            }

            await base.PreRender();
        }

        private async Task LoadData()
        {
            var result = await productCatalogService.ListProducts();

            Products = result.Items;
            ContinuationToken = result.ContinuationToken;
        }

        public async Task LoadMore()
        {
            var result = await productCatalogService.ListProducts(ContinuationToken);

            Products.AddRange(result.Items);
            ContinuationToken = result.ContinuationToken;
        }

        public async Task Remove(string id)
        {
            await productCatalogService.DeleteProduct(id);
            await LoadData();
        }
    }
}
