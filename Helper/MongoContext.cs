using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Helper
{

    /// <summary>
    /// Mongo context help developer using mongoDB on application
    /// </summary>
    public class MongoContext
    {
        private static MongoClient _client;
        private static IMongoDatabase _db;
        private static string _connectionString;

        /// <summary>
        /// Constructor init connection and database
        /// </summary>
        public MongoContext()
        {
            string databaseName = MongoUrl.Create(_connectionString).DatabaseName;
            _client = new MongoClient(_connectionString);
            _db = _client.GetDatabase(databaseName);
        }

        /// <summary>
        /// Init database on connection sting
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <returns>Context</returns>
        public static MongoContext Initialize(string connectionString)
        {
            _connectionString = connectionString;
            return new MongoContext();
        }

        /// <summary>
        /// Get collection by type of document
        /// </summary>
        /// <typeparam name="T">Document Type</typeparam>
        /// <returns>Collection</returns>
        public static IMongoCollection<T> GetCollection<T>()
        {
            return _db.GetCollection<T>(typeof(T).Name);
        }

        /// <summary>
        /// Create index for the collection
        /// </summary>
        /// <typeparam name="T">Document Type</typeparam>
        /// <param name="fields">Fields to index</param>
        public static void CreateUniqueIndex<T>(params string[] fields) where T : MongodbEntity
        {
            if (_db == null)
            {
                throw new Exception("MongoContext was not initialized. Please run Initialize first!");
            }
            IndexKeysDefinition<T> indexDefinition = null;
            List<IndexKeysDefinition<T>> listIndex = new List<IndexKeysDefinition<T>>();
            foreach (string field in fields)
            {
                var fieldDefine = new StringFieldDefinition<T>(field);
                var index = new IndexKeysDefinitionBuilder<T>().Ascending(fieldDefine);
                listIndex.Add(index);
            }
            indexDefinition = new IndexKeysDefinitionBuilder<T>().Combine(listIndex);
            var options = new CreateIndexOptions() { Unique = true };
            _db.GetCollection<T>(typeof(T).Name).Indexes.CreateOne(indexDefinition, options);
        }

        /// <summary>
        /// Get query of collection to run user condition
        /// </summary>
        /// <typeparam name="T">Document Type</typeparam>
        /// <returns>Query documents of collection</returns>
        public static IQueryable<T> All<T>() where T : MongodbEntity
        {
            return _db.GetCollection<T>(typeof(T).Name).AsQueryable();
        }

        /// <summary>
        /// Get document in collection with condition
        /// </summary>
        /// <typeparam name="T">Document Type</typeparam>
        /// <param name="predicate">Condition</param>
        /// <returns>Document match with condition</returns>
        public static T Get<T>(Expression<Func<T, bool>> predicate) where T : MongodbEntity
        {
            return _db.GetCollection<T>(typeof(T).Name).Find(predicate).FirstOrDefault();
        }

        /// <summary>
        /// Check exist any document in database with condition
        /// </summary>
        /// <typeparam name="T">Document Type</typeparam>
        /// <param name="predicate">Condition</param>
        /// <returns>True if match | False if not</returns>
        public static bool IsExist<T>(Expression<Func<T, bool>> predicate)
        {
            long count = _db.GetCollection<T>(typeof(T).Name).Find(predicate).Count();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Insert document to collection
        /// </summary>
        /// <typeparam name="T">Document Type</typeparam>
        /// <param name="document">Document to insert</param>
        public static void Insert<T>(T document) where T : MongodbEntity
        {
            _db.GetCollection<T>(typeof(T).Name).InsertOne(document);
        }

        /// <summary>
        /// Update document in collection
        /// </summary>
        /// <typeparam name="T">Document Type</typeparam>
        /// <param name="document">Document to update</param>
        public static void Update<T>(T document) where T : MongodbEntity
        {
            Type type = typeof(T);
            T dbItem = _db.GetCollection<T>(type.Name).Find(a => a._id == document._id).FirstOrDefault();
            if (dbItem == null)
            {
                throw new Exception("Cannot find record with _id = " + document._id);
            }
            _db.GetCollection<T>(type.Name).ReplaceOne<T>(a => a._id == document._id, document);
        }

        /// <summary>
        /// Delete document in collection
        /// </summary>
        /// <typeparam name="T">Document Type</typeparam>
        /// <param name="documentId">Id of document to delete</param>
        public static void Delete<T>(ObjectId documentId) where T : MongodbEntity
        {
            Type type = typeof(T);
            T dbItem = _db.GetCollection<T>(type.Name).Find(a => a._id == documentId).FirstOrDefault();
            if (dbItem == null)
            {
                throw new Exception("Cannot find record with _id = " + documentId);
            }
            _db.GetCollection<T>(type.Name).DeleteOne<T>(a => a._id == documentId);
        }
    }
}
