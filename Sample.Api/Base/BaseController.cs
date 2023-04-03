using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Contract;
using Sample.Business;
using Sample.Common.ViewModels;
using Sample.Model.Base;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Api.Base;

[ApiController]
[Route("api/[controller]")]
public class BaseController<T> : ControllerBase, IBaseController<T> where T : BaseEntityWithKey
{
        #region [Field(s)]

        private readonly IBaseBusiness<T> _business;

        #endregion

        #region [Constructor]

        public BaseController(IBaseBusiness<T> business) =>
                _business = business;

        #endregion

        #region [Method(s)]

        [HttpPost]
        public async Task<CustomResponse?> Add(T t, CancellationToken cancellationToken) =>
                await _business.AddAsync(t, cancellationToken);

        [HttpGet]
        [HttpHead]
        public async Task<CustomResponse<List<T>>?> GetAll([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken) =>
                await _business.LoadAllAsync(sieveModel, cancellationToken);

        [HttpGet]
        [Route("{id:int}")]
        public async Task<CustomResponse?> GetById(int id, CancellationToken cancellationToken) =>
                await _business.LoadByIdAsync(id, cancellationToken);    

        [HttpPut]
        public async Task<CustomResponse?> Update(T t, CancellationToken cancellationToken) =>
                await _business.UpdateAsync(t, cancellationToken);

        [HttpDelete]
        public async Task<CustomResponse?> Delete(T t, CancellationToken cancellationToken) =>
                await _business.DeleteAsync(t, cancellationToken);

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<CustomResponse?> Delete(int id, CancellationToken cancellationToken) =>
                await _business.DeleteAsync(id, cancellationToken);

        [HttpOptions]

        public CustomResponse Options()
        {
                Response.Headers.Add("Allow", "POST,OPTIONS,GET,PUT,DELETE,HEAD");
                return new CustomResponse
                {
                        Message = "Successful"
                };
        }

        #endregion
}
