using RecordTrack.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Abstractions
{
    public interface IReadRepository<T>: IRepository<T> where T : class
    {

        //Sorguda olduğu için, in-memory olsa IEnumarable
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        //Asenkron çalışacaklar Task olarak döner ve name convention olarak sonlarına Async eklenir.
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
}
