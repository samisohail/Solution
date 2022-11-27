using DataAccess.Contract;
using DataStore.DbEntities;
using DataTransferObjects;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// to add in DI container set up in Web project
[assembly: InternalsVisibleTo("Web")]
namespace DataAccess.Implementation
{
    internal class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SolutionDbContext _dbContext;
        private readonly Serilog.ILogger _logger;
        public UnitOfWork(SolutionDbContext dbContext, Serilog.ILogger logger)
        {
            _dbContext= dbContext;
            _logger = logger;
        }
        public Result Commit()
        {
            try
            {
                _dbContext.SaveChanges();
                return Result.OK();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Changes were not saved.");
                return Result.Fail("Changes were not saved.");
            }
        }

        public void Dispose()
        {          
            _dbContext.Dispose();
        }
    }
}
