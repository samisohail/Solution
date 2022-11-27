using AutoMapper;
using MediatR;

namespace ReadStack
{
    public abstract class QueriesBaseHandler<TQuery, TResult> : RequestHandler<TQuery, TResult> where TQuery : IRequest<TResult>
    {
        protected QueriesBaseHandler() { }
        protected abstract override TResult Handle(TQuery request);
    }
}