using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.MongoDB.Model
{
    /// <summary>
    /// Базовый класс для всех сущностей с ключом ObjectId
    /// </summary>
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class DbModel : IDbModel
    {
        private ObjectId _id;

        [BsonId]
        public ObjectId Id
        {
            get
            {
                if (_id == ObjectId.Empty)
                {
                    _id = GenerateNewId();
                }
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        protected virtual ObjectId GenerateNewId()
        {
            return ObjectId.GenerateNewId();
        }
    }
}
