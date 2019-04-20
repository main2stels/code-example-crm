using FullCRM.Database.Postgre.Model;
using FullCRM.Database.Postgre.Model.Enum;
using FullCRM.Extensions;
using FullCRM.Models.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Settings.Frontend
{
    public class FrontendSettings
    {
        public Type[] EnumTypes
        {
            get
            {
                return new Type[]
                {
                    typeof(PaymentStatus),
                    typeof(ProductUnit),
                    typeof(OrderStatus),
                    typeof(Condition),
                    typeof(UserAccessEnum),
                    typeof(AddressType),
                    typeof(ContractorType),
                    typeof(ContractStatus),
                    typeof(JobTitle),
                    typeof(Models.Document.DocumentFormat),
                    typeof(Mode)
                };
            }
        }

        public IDictionary<string, IEnumerable<EnumModel>> Enums
        {
            get
            {
                return EnumTypes.ToDictionary(x => x.Name, y => Enum.GetValues(y).Cast<Enum>()
                    .Select(x => new EnumModel {
                        Id = Convert.ChangeType(x, x.GetTypeCode()).ToString(),
                        Name = Enum.GetName(y, x),
                        Value = x.ToName()
                    }));
            }
        }
    }
}
