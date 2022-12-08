using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAdvisor.DataAccess.MSSQL.Functions.Interfaces
{
    public interface ICrud
    {
        Task<T> Create<T>(T ObjectForDB) where T : class;
        Task<T> Update<T>(Guid EntityID, T NewEntity) where T : class;
        Task<T> Find<T>(Guid EntityID) where T : class;
        Task<List<T>> FindAll<T>() where T : class;
        Task<bool> Delete<T>(Guid EntityID) where T : class;
    }
}
