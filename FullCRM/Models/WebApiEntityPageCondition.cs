using FullCRM.Database;
using FullCRM.Database.Sql.Filtering.Abstract;
using FullCRM.Database.Sql.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Models
{
    [ModelBinder(typeof(JsonObjectModelBinder))]
    public class WebApiEntityPageCondition<T> : IPageCondition<T>
    {
        public int Limit { get; set; }

        public int Page { get; set; }

        public SortDirectionType SortDirect { get; set; }

        public string SortField { get; set; }

        public Filter<T> Filter { get; set; }
    }
}
