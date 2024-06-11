namespace gRPC.Microservicio.Autor.Infrastructure
{
    public interface IStrategyAutorIMG
    {
        public Task<object> DoRequest(object data);
    }
}
