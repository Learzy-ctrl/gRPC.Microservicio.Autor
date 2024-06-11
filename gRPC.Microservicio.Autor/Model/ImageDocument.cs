using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace gRPC.Microservicio.Autor.Model
{
    public class ImageDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("AutorGuid")]
        public string AutorGuid { get; set; }

        [BsonElement("Image")]
        public byte[] Image { get; set; }
    }
}
