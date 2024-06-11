using gRPC.Microservicio.Autor.Context;
using gRPC.Microservicio.Autor.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;

namespace gRPC.Microservicio.Autor.ConcreteStrategy
{
    public class ConcreteAddAutorIMG : IStrategyAutorIMG
    {
        private readonly IMongoCollection<BsonDocument> _collection;
        public ConcreteAddAutorIMG(IMongoDatabase database)
        {
            _collection = database.GetCollection<BsonDocument>("AutorImages");
        }
        public async Task<object> DoRequest(object data)
        {
            var autor = data as AutorIMG;
            var document = new BsonDocument
            {
                {"AutorGuid", autor.AutorGuid },
                {"Image", new BsonBinaryData(autor.Image.ToByteArray()) }
            };
            try
            {
                await _collection.InsertOneAsync(document);
                return true;
            }
            catch
            {
                return null;
            }
        }
    }
}
