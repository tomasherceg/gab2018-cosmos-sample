using System;
using System.Collections.Generic;
using System.Linq;

namespace Cosmos.SqlApiDemo.Model
{
    public class ProductPrice
    {

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public DateTime ValidityBeginDate { get; set; }

        public DateTime? ValidityEndDate { get; set; }

    }
}