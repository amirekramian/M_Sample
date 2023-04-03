using Sample.Common.ViewModels;
using Sample.Model.Base;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Api.Contract;

public interface IBaseController<T> where T : BaseEntityWithKey
{
        Task<CustomResponse?> Add(T t, CancellationToken token);

        Task<CustomResponse<List<T>>?> GetAll(SieveModel sieveModel, CancellationToken token);

        Task<CustomResponse?> GetById(int id, CancellationToken token);

        Task<CustomResponse?> Update(T t, CancellationToken token);

        Task<CustomResponse?> Delete(T t, CancellationToken token);

        Task<CustomResponse?> Delete(int id, CancellationToken token);

        CustomResponse Options();
}
