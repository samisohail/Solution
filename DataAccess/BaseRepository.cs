using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using DataTransferObjects;
using Serilog;
using DataStore.DbEntities;

namespace DataAccess
{
    internal abstract class BaseRepository<T> where T : class
    {
        protected readonly SolutionDbContext _dbContext;
        protected readonly DbSet<T> _entity;
        protected readonly Serilog.ILogger _logger;
        protected BaseRepository(SolutionDbContext dbContext, ILogger logger)
        {
            _dbContext= dbContext;
            _entity = dbContext.Set<T>();
            _logger = logger;
        }

        protected T GetById(int id)
        {
            return _dbContext.Find<T>(id);
        }

        protected IEnumerable<T> FindAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        protected T Create(T entity)
        {
            _dbContext.Add(entity);
            return entity;
        }

        protected T Update(T entity)
        {
            _dbContext.Update(entity);
            return entity;
        }

        protected void Delete(T entity) 
        {
            _dbContext.Remove(entity);
        }
      
        protected Result Try(Action action) 
        {
            try
            {
                action();
                return Result.OK();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occured");
                return Result.Fail("Error occured");
            }
        }

        protected Result Try<T>(Func<T> func)
        {
            try
            {
                func();
                return Result.OK();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occured");
                return Result.Fail("Error occured");
            }
        }
        
    }
}
