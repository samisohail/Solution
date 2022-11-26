using AutoMapper;
using DataAccess.Contract;
using DataTransferObjects;
using MediatR;
using ReadStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.User
{
    internal class GetAllUsersQuery : IRequest<Result<List<UserDto>>>
    {
        public sealed class Handler : ReadStackBaseHandler<GetAllUsersQuery, Result<List<UserDto>>>
        {
            private readonly IUserRepository _userRepo;
            private readonly IMapper _mapper;            

            public Handler(IMapper mapper, IUserRepository userRepo)
            {
                _mapper= mapper;
                _userRepo = userRepo;
            }
            protected override Result<List<UserDto>> Handle(GetAllUsersQuery request)
            {
                var users = new List<UserDto>
                {
                    new UserDto { UserId = 1, FirstName = "Paolo", Email="paolo@cmcmarkets.com" },
                    new UserDto { UserId = 2, FirstName = "David", Email="david@cmcmarkets.com" },
                    new UserDto { UserId = 3, FirstName = "James", Email="james@cmcmarkets.com" }
                };
                return Result.OnSuccess(users);
            }

        }
    }
}
