using Google.Protobuf;
using gRPC.Microservicio.Autor.Context;
using gRPC.Microservicio.Autor.Infrastructure;
using gRPC.Microservicio.Autor.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace gRPC.Microservicio.Autor.ConcreteStrategy
{
    public class ConcreteGetAutorIMGByGuid : IStrategyAutorIMG
    {
        private readonly IMongoCollection<ImageDocument> _collection;
        public ConcreteGetAutorIMGByGuid(IMongoDatabase database)
        {
            _collection = database.GetCollection<ImageDocument>("AutorImages");
        }
        public async Task<object> DoRequest(object data)
        {
            var autor = data as Autorid;
            var filter = Builders<ImageDocument>.Filter.Eq(doc => doc.AutorGuid, autor.Id);
            var response = await _collection.Find(filter).FirstOrDefaultAsync();
            var autorFound = new AutorIMG
            {
                AutorGuid = response.AutorGuid,
                Image = ByteString.CopyFrom(response.Image),
            };
            return autorFound;
        }
    }
}
