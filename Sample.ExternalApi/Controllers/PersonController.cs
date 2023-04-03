using Microsoft.AspNetCore.Mvc;
using Sample.Common.ViewModels;
using Sample.DapperBusiness;

namespace Sample.ExternalApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
        #region [Field(s)]

        private readonly PersonBusiness _business;

        #endregion

        #region [Constructor]

        public PersonController(PersonBusiness business) =>
                _business = business;

        #endregion

        #region [Method(s)]

        [HttpGet]
        [Route("GetPersonInfo")]
        public async Task<CustomResponse> GetPersonInfoAsync([FromQuery] PersonInquiryViewModel inquiryViewModel, CancellationToken cancellationToken) =>
                await _business.LoadInfoByNationalCodeAsync(inquiryViewModel.NationalCode!, cancellationToken);

        [HttpGet]
        [Route("GetPersonMobile")]
        public async Task<CustomResponse> GetPersonMobileAsync([FromQuery] PersonInquiryViewModel inquiryViewModel, CancellationToken cancellationToken) =>
                await _business.LoadPhoneByNationalCodeAsync(inquiryViewModel.NationalCode!, cancellationToken);

        #endregion
}
