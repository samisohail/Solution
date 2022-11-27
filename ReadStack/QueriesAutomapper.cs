using AutoMapper;
using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// to add in DI container in Startup.cs
[assembly: InternalsVisibleTo("Web")]
namespace Queries
{
    internal class QueriesAutomapper : Profile
    {
        public QueriesAutomapper()
        {
            CreateMap<Domain.User.User, UserDto>();
        }
    }
}
