using Sample.Api.Base;
using Sample.Business.Businesses;
using Sample.Business;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.Common.ViewModels;

namespace Sample.Api.Controllers;

public class PhoneController : BaseController<Phone>
{
        #region [Fied(s)]

        private readonly PhoneBusiness _phoneBusiness;

        #endregion

        #region [Constructor]

        public PhoneController(IBaseBusiness<Phone> phoneBusiness) : base(phoneBusiness)
        {
                _phoneBusiness = (PhoneBusiness)phoneBusiness;
        }

        #endregion

        #region [Method(s)]

        [HttpGet, Route("GetByNationalCode/{nationalCode}")]
        public async Task<CustomResponse> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken) =>
                await _phoneBusiness.LoadByNationalCodeAsync(nationalCode, cancellationToken);

        #endregion
}
