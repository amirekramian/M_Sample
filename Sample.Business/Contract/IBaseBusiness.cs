using Sample.Common.ViewModels;
using Sample.Model.Base;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business;

public interface IBaseBusiness<T> where T : BaseEntityWithKey
{
        Task<CustomResponse?> AddAsync(T t, CancellationToken cancellationToken);

        Task<CustomResponse<List<T>>?> LoadAllAsync(SieveModel sieveModel, CancellationToken cancellationToken);

        Task<CustomResponse?> LoadByIdAsync(int id, CancellationToken cancellationToken);

        Task<CustomResponse?> UpdateAsync(T t, CancellationToken cancellationToken);

        Task<CustomResponse?> DeleteAsync(T t, CancellationToken cancellationToken);

        Task<CustomResponse?> DeleteAsync(int id, CancellationToken cancellationToken);
}
