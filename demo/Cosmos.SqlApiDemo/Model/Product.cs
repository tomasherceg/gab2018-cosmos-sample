using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cosmos.SqlApiDemo.Model
{
    public class Product
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string QuantityPerUnit { get; set; }

        public List<string> Categories { get; set; } = new List<string>();

        public List<ProductPrice> Prices { get; set; } = new List<ProductPrice>();

        public DateTime? DeletedDate { get; set; }
    }
}
