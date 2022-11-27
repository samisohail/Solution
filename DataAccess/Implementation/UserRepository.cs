using DataAccess.Contract;
using DataStore.CustomMappers;
using DataStore.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Web")]
namespace DataAccess.Implementation
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(SolutionDbContext dbContext, Serilog.ILogger logger) : base(dbContext, logger)
        {

        }
        public void Create()
        {

        }

        public IEnumerable<Domain.User.User> GetAll()
        {
            var users = base.FindAll().ToList();
            var mapped = UserMapping.MapToUser(users);
            return mapped;
        }
    }
}
