using Sample.Common.ViewModels;
using Sample.DataAccess;
using Sample.DataAccess.Base;
using Sample.DataAccess.Contracts;
using Sample.Model.Base;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Sieve.Extensions.MethodInfoExtended;

namespace Sample.Business.Base;

public class BaseBusiness<T> : IBaseBusiness<T> where T : BaseEntityWithKey
{
        #region [Field(s)]

        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<T> _repository;

        #endregion

        #region [Constructor]

        public BaseBusiness(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
                _unitOfWork = unitOfWork;
                _repository = repository;
        }

        #endregion

        #region [Method(s)]

        public async Task<CustomResponse?> AddAsync(T t, CancellationToken cancellationToken = new())
        {
                await _repository.AddAsync(t, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
                return new CustomResponse
                {
                        IsSuccess = true,
                        ChangedId = t.Id,
                        Message = "Entity Saved"
                };
        }

        public async Task<CustomResponse<List<T>>?> LoadAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = default(CancellationToken))
        {
                var data = await _repository.LoadAllAsync(sieveModel, null, cancellationToken);
                return new CustomResponse<List<T>>
                {
                        Data = data,
                        RecordsTotal = data!.Count,
                        RecordsFiltered = data.Count,
                        Message = "Data Loaded",
                        IsSuccess = true
                };
        }

        public async Task<CustomResponse?> LoadByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
                var data = await _repository.LoadByIdAsync(id, null, cancellationToken);
                return new CustomResponse
                {
                        Data = data,
                        Message = "Data Loaded",
                        IsSuccess = true
                };
        }

        public async Task<CustomResponse?> UpdateAsync(T t, CancellationToken cancellationToken = new())
        {
                await _repository.UpdateAsync(t, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
                return new CustomResponse
                {
                        IsSuccess = true,
                        ChangedId = t.Id,
                        Message = "Entity Updated"
                };
        }

        public async Task<CustomResponse?> DeleteAsync(T t, CancellationToken cancellationToken = default(CancellationToken))
        {
                await _repository.UpdateAsync(t, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
                return new CustomResponse
                {
                        IsSuccess = true,
                        ChangedId = t.Id,
                        Message = "Entity Updated"
                };
        }

        public async Task<CustomResponse?> DeleteAsync(int id, CancellationToken cancellationToken = new())
        {
                await _repository.DeleteAsync(id, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
                return new CustomResponse
                {
                        IsSuccess = true,
                        ChangedId = id,
                        Message = "Entity Deleted"
                };
        }

        #endregion
}
