using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.Postgre.Model.Enum
{
    public enum JobTitle
    {
        [Description("Генеральный директор")]
        GeneralDirector = 1,

        [Description("Директор")]
        Director = 2,

        [Description("Бухгалтер")]
        Accountant = 3,

        [Description("Специалист")]
        Specialist = 4,

        [Description("Маркетолог")]
        Marketer = 5,

        [Description("Индивидуальный предприниматель")]
        IndividualEntrepreneur = 6,

        [Description("Менеджер по работе с клиентами")]
        CustomerServiceManager = 7,

        [Description("Руководитель отдела")]
        DepartmentHead = 8,

        [Description("Должность не указана")]
        Unknown = 9
    }
}
