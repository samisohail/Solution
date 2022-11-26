using DataTransferObjects;
using DataTransferObjects.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Commands.User
{
    public class CreateUserCommand : IRequest<Result<CreateUserResponse>>
    {
        public CreateUserModel CreateUserModel;
        internal class Handler : CommandBaseHandler<CreateUserCommand, Result<CreateUserResponse>>
        {
            
        }
    }
}
