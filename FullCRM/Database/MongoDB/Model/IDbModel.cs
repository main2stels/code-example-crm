using MongoDB.Bson;

namespace FullCRM.Database.MongoDB.Model
{
    public interface IDbModel
    {
        ObjectId Id { get; set; }
    }
}
