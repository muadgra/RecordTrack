using Microsoft.EntityFrameworkCore;
using RecordTrack.Application.Repositories;
using RecordTrack.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Abstractions
{
    public interface IWriteRepository<T>: IRepository<T> where T: BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> data);
        bool Remove(T model);
        Task<bool> Remove(string id);
        bool RemoveRange(List<T> data);
        bool Update(T model);
        Task<int> SaveAsync();
    }
}
