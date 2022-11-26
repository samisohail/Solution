using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.User
{
    public class CreateUserResponse : CreateUserModel
    {
        public int UserId { get; set; }
    }
}
