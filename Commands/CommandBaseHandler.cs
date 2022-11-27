using MediatR;

namespace Commands
{
    public abstract class CommandBaseHandler<TCommand, TResult> : RequestHandler<TCommand, TResult> where TCommand : IRequest<TResult>
    {
        protected CommandBaseHandler() { }
        public TResult UnitTestHandle(TCommand request)
        {
            return Handle(request);
        }
    }
}