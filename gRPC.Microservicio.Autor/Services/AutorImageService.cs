using gRPC.Microservicio.Autor.ConcreteStrategy;
using gRPC.Microservicio.Autor.Context;
using Grpc.Core;
using MongoDB.Driver;

namespace gRPC.Microservicio.Autor.Services
{   
    public class AutorImageService : AutorImage.AutorImageBase
    {
        private readonly ContextStrategy _contextStrategy;
        private readonly IMongoDatabase _database;
        public AutorImageService( ContextStrategy contextStrategy, IMongoDatabase database)
        {
            _contextStrategy = contextStrategy;
            _database = database;
        }

        public override async Task<response> AddAutorIMG(AutorIMG request, ServerCallContext context)
        {
            _contextStrategy.SetStrategy(new ConcreteAddAutorIMG(_database));
            var response = await _contextStrategy.ExecTask(request);
            if(response != null)
            {
                return new response
                {
                    Message = "succesfull"
                };
            }
            else
            {
                return new response
                {
                   Message = "failed to load"
                };
            }
            
        }

        public override async Task<AutoresIMGList> GetAutoresIMG(Empty request, ServerCallContext context)
        {
            _contextStrategy.SetStrategy(new ConcreteGetAutoresIMG(_database));
            var response = await _contextStrategy.ExecTask(request);
            var Autores = response as AutoresIMGList;
            return Autores;
        }

        public override async Task<AutorIMG> GetAutorIMGByGuid(Autorid request, ServerCallContext context)
        {
            _contextStrategy.SetStrategy(new ConcreteGetAutorIMGByGuid(_database));
            var response = await _contextStrategy.ExecTask(request);
            var Autor = response as AutorIMG;
            return Autor;
        }
    }
}
