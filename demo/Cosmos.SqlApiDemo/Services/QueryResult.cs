using System;
using System.Collections.Generic;
using System.Linq;

namespace Cosmos.SqlApiDemo.Services
{
    public class QueryResult<T>
    {

        public List<T> Items { get; }

        public string ContinuationToken { get; }

        public QueryResult(List<T> items, string continuationToken)
        {
            Items = items;
            ContinuationToken = continuationToken;
        }

    }
}