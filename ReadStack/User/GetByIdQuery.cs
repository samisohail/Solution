using DataAccess.Contract;
using DataTransferObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.User
{
    public class GetByIdQuery : IRequest<Result<UserDto>>
    {
        public int UserId { get; set; }
        internal class Handler : IRequestHandler<GetByIdQuery, Result<UserDto>>
        {
            private readonly IUserRepository _userRepository;
            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public Task<Result<UserDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
