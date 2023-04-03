using Microsoft.EntityFrameworkCore.Query;
using Sample.Model.Base;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DataAccess.Contracts;

public interface IRepository<T> where T : BaseEntity
{
        Task<T> AddAsync(T t, CancellationToken cancellationToken);

        Task<List<T>?> LoadAllAsync(SieveModel sieveModel, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<T?> LoadByIdAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<T?> UpdateAsync(T? t, CancellationToken cancellationToken);

        Task<T?> DeleteAsync(T? t, CancellationToken cancellationToken);

        Task<T?> DeleteAsync(int id, CancellationToken cancellationToken);

        Task<long> CountAllAsync(CancellationToken cancellationToken);

        Task<long> CountAsync(SieveModel sieveModel, CancellationToken cancellationToken);
}
