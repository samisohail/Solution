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
        public sealed class Handler : QueriesBaseHandler<GetAllUsersQuery, Result<List<UserDto>>>
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
                var usersDb = _userRepo.GetAll();
                var mapped = _mapper.Map<List<UserDto>>(usersDb);                
                return Result.OnSuccess(mapped);
            }
        }
    }
}
