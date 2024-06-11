using Google.Protobuf;
using gRPC.Microservicio.Autor.Context;
using gRPC.Microservicio.Autor.Infrastructure;
using gRPC.Microservicio.Autor.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace gRPC.Microservicio.Autor.ConcreteStrategy
{
    public class ConcreteGetAutoresIMG : IStrategyAutorIMG
    {
        private readonly IMongoCollection<ImageDocument> _collection;
        public ConcreteGetAutoresIMG(IMongoDatabase database)
        {
            _collection = database.GetCollection<ImageDocument>("AutorImages");
        }
        public async Task<object> DoRequest(object data)
        {
            var list = new AutoresIMGList();
            var response = await _collection.Find(_ => true).ToListAsync();

            foreach (var imageDoc in response)
            {
                var autorIMG = new AutorIMG
                {
                    AutorGuid = imageDoc.AutorGuid,
                    Image = ByteString.CopyFrom(imageDoc.Image)
                };

                list.Autores.Add(autorIMG);
            }

            return list;
        }
    }
}
