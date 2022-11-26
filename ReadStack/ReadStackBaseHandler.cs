using AutoMapper;
using MediatR;

namespace ReadStack
{
    public abstract class ReadStackBaseHandler<TQuery, TResult> : RequestHandler<TQuery, TResult> where TQuery : IRequest<TResult>
    {
        protected ReadStackBaseHandler() { }
        protected abstract override TResult Handle(TQuery request);
    }
}