using FullCRM.Database.Sql.Filtering.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.Sql.Models
{
    public interface IPageCondition<T>
    {
        int Page { get; set; }

        int Limit { get; set; }

        string SortField { get; set; }

        SortDirectionType SortDirect { get; set; }

        Filter<T> Filter { get; set; }
    }
}
