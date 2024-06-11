using gRPC.Microservicio.Autor.Infrastructure;

namespace gRPC.Microservicio.Autor.Context
{
    public class ContextStrategy
    {
        private IStrategyAutorIMG _strategy;

        public void SetStrategy(IStrategyAutorIMG strategy)
        {
            _strategy = strategy;
        }

        public async Task<object> ExecTask(object data)
        {
            return await _strategy.DoRequest(data);
        }
    }
}
