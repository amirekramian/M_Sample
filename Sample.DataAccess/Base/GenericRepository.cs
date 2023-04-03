using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sample.DataAccess.Contracts;
using Sample.Model.Base;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sieve.Extensions.MethodInfoExtended;

namespace Sample.DataAccess.Base;

public class GenericRepository<T> : IRepository<T> where T : BaseEntityWithKey, new()
{

        #region [Field(s)]

        private readonly DbContext _context;

        private readonly DbSet<T> _dbSet;

        private readonly ISieveProcessor _sieveProcessor;

        #endregion

        #region [Constructor]

        public GenericRepository(DbContext context, ISieveProcessor sieveProcessor)
        {
                _context = context;
                _dbSet = context.Set<T>();
                _sieveProcessor = sieveProcessor;
        }

        #endregion

        #region [Method(s)]

        public async Task<T> AddAsync(T t, CancellationToken cancellationToken = new()) =>
                (await _dbSet.AddAsync(t, cancellationToken)).Entity;

        public async Task<List<T>?> LoadAllAsync(SieveModel sieveModel, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null,
                 CancellationToken cancellationToken = default(CancellationToken))
        {
                IQueryable<T> query = _dbSet.AsNoTracking();
                if (include != null)
                        query = include(query);

                return await _sieveProcessor.Apply(sieveModel, query).ToListAsync(cancellationToken);
        }

        public async Task<T?> LoadByIdAsync(int id, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null, CancellationToken cancellationToken = default(CancellationToken))
        {
                IQueryable<T> query = _dbSet.AsNoTracking();
                if (include != null)
                        query = include!(query);
                
                return await query.FirstOrDefaultAsync((T x) => x.Id == id, cancellationToken);
        }

        public async Task<T?> UpdateAsync(T? t, CancellationToken cancellationToken = new())
        {
                T? t2 = t;
                if (t2 == null)
                        return null;

                t2.LastUpdated = DateTime.Now;
                return await Task.Run(() => _dbSet.Update(t2).Entity, cancellationToken);
        }

        public async Task<T?> DeleteAsync(T? t, CancellationToken cancellationToken = new())
        {
                T? t2 = t;
                if (t2 == null)
                        return null;

                t2.IsDeleted = true;
                t2.LastUpdated = DateTime.Now;
                return await Task.Run(() => _dbSet.Update(t2).Entity, cancellationToken);
        }
                
        public async Task<T?> DeleteAsync(int id, CancellationToken cancellationToken = new())
        {
                return await DeleteAsync(await LoadByIdAsync(id, null, cancellationToken), cancellationToken);
        }

        public async Task<long> CountAllAsync(CancellationToken cancellationToken = new())
        {
                return await _dbSet.LongCountAsync(cancellationToken);
        }

        public async Task<long> CountAsync(SieveModel sieveModel, CancellationToken cancellationToken)
        {
                return await _sieveProcessor.Apply(sieveModel, _dbSet.AsNoTracking(), null, applyFiltering: true, applySorting: true, applyPagination: false).LongCountAsync(cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> data, CancellationToken cancellationToken = new())
        {
                await _dbSet.AddRangeAsync(data, cancellationToken);
        }

        #endregion
}
