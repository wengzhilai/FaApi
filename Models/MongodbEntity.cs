using MongoDB.Bson;

namespace Models
{
    /// <summary>
    /// Entity, the other class will inheritance to use MongoContext
    /// </summary>
    public abstract class MongodbEntity
    {
        /// <summary>
        /// _id key
        /// </summary>
        public ObjectId _id { get; set; }

        /// <summary>
        /// Constructor, _id will generate here
        /// </summary>
        public MongodbEntity()
        {
            _id = ObjectId.GenerateNewId();
        }
    }
}
