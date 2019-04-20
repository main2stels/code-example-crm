using LinqToDB.Mapping;
using System;

namespace FullCRM.Database.Postgre.Model
{
    [Table("Order", Schema = "fullCRM")]
    public class Order : DbModel
    {
        [Column, NotNull]
        public string Number { get; set; }

        /// <summary>
        /// Отчетная дата
        /// </summary>
        [Column, NotNull]
        public DateTime Date { get; set; }

        [Column]
        public DateTime? StartDate { get; set; }

        [Column]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// премия
        /// </summary>
        [Column, NotNull]
        public Decimal Prize { get; set; }

        [Column]
        public int Tariff { get; set; }

        /// <summary>
        /// Ответственный
        /// </summary>
        [Column]
        public string Responsible { get; set; }

        /// <summary>
        /// id ответственного
        /// </summary>
        [Column]
        public long ResponsibleId { get; set; }

        /// <summary>
        /// Бюджет
        /// </summary>
        [Column]
        public decimal Budget { get; set; }

        /// <summary>
        /// id контрагента
        /// </summary>
        [Column]
        public long ContractorId { get; set; }

        /// <summary>
        /// Теги
        /// </summary>
        [Column]
        public string[] Tegs { get; set; }

        /// <summary>
        /// На какое юр лицо заключается договор
        /// </summary>
        [Column]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        [Column]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Суперкоммиссия
        /// </summary>
        [Column]
        public decimal Supercommission { get; set; }

        /// <summary>
        /// Статус платежа
        /// </summary>
        [Column]
        public PaymentStatus PaymentStatus { get; set; }

        /// <summary>
        /// Номер приложения
        /// </summary>
        [Column]
        public int AttachmentNumber { get; set; }

        /// <summary>
        /// id контракта
        /// </summary>
        [Column]
        public long ContractId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [Column]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        [Column]
        public DateTime? EditDate { get; set; }

        /// <summary>
        /// Признак удаления
        /// </summary>
        [Column]
        public Condition Condition { get; set; }

        /// <summary>
        /// Клиент
        /// </summary>
        [Column]
        public string Name { get; set; }
    }
}
