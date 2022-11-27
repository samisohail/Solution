using DataStore.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contract
{
    public interface IUserRepository
    {
        IEnumerable<Domain.User.User> GetAll();
        void Create();
    }
}
