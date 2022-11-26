using MediatR;
using Serilog;

namespace Web.Setup
{
    public class LoggingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Serilog.ILogger _logger;
        public LoggingPipeline(Serilog.ILogger logger) 
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.Information($"Request received at {System.DateTime.UtcNow}");
            var response = await next();
            _logger.Information("{@request} {@response}", request, response);
            return response;
        }
    }
}
