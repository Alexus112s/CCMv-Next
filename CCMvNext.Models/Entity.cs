using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CCMvNext.Models
{
    /// <summary>
    /// A base class for all the entities. Enables generic ID-based processing (e.g. CRUD).
    /// </summary>
    public class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
