using DataAccess.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Web")]
namespace DataAccess.Implementation
{
    internal class UserRepository : IUserRepository
    {
        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Get()
        {
            throw new NotImplementedException();
        }
    }
}
