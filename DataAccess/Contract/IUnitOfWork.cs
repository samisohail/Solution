using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Web")]
[assembly: InternalsVisibleTo("Commands")]

namespace DataAccess.Contract
{
    internal interface IUnitOfWork
    {
        Result Commit();
    }
}
