using Microsoft.AspNetCore.Mvc;
using Sample.Business.Businesses;
using Sample.Business;
using Sample.Common.ViewModels;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Api.Base;

namespace Sample.Api.Controllers;

public class PersonController : BaseController<Person>
{
        #region [Field(s)]

        private readonly PersonBusiness _business;

        #endregion

        #region [Constructor]

        public PersonController(IBaseBusiness<Person> business) : base(business) =>
                _business = (PersonBusiness)business;

        #endregion

        #region [Method(s)]

        [HttpGet, Route("GetByNationalCode/{nationalCode}")]
        public async Task<CustomResponse> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken) =>
                await _business.LoadByNationalCodeAsync(nationalCode, cancellationToken);

        #endregion
}
