using FullCRM.Database.MongoDB.Model.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FullCRM.Database.MongoDB.Model
{
    /// <summary>
    /// Модель записи в журнали изменений
    /// </summary>
    [BsonIgnoreExtraElements]
    public class VersionsLog : DbModel
    {
        /// <summary>
        /// Тип модели
        /// </summary>
        public string ModelType { get; set; }

        /// <summary>
        /// Идентификатор модели
        /// </summary>
        public string ModelId { get; set; }

        /// <summary>
        /// Тип операции произведённой над моделью
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// Идентификатор пользователя внёсшего изменения
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Дата внесения изменений
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Модель со внсёнными измениниями
        /// </summary>
        public object Model { get; set; }
    }
}
