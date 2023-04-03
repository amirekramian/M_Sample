using RestSharp;
using Sample.Common.Wrapper;
using Sample.ExternalServices.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ExternalServices.Services;

public class PersonDataGatewayService : BaseGatewayService
{
        #region [Constructor]

        public PersonDataGatewayService(string? accessKey, string baseAddress) :
                base(accessKey, baseAddress)
        { }

        #endregion

        #region [Method(s)]

        public async Task<EntitiesPerson?> LoadPersonData(PersonRequest personRequest, CancellationToken cancellationToken = new())
        {      
                        personRequest.BirthDate = personRequest!.BirthDate!.Replace("-", string.Empty);
                        var client = new RestClient();
                        var path = BaseAddress + $"?NationalCode={personRequest.NationalCode}&BirthDate={personRequest.BirthDate}";
                        var request = new RestRequest(path) { Timeout = 20 * 1000 };
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("X-SSP-Api-Key", "5dc78e46-8c68-407a-aaa1-3dca3044ad63");
                        var response = await client.ExecuteAsync<EntitiesPerson>(request, cancellationToken);
                        return response.Data!;                           
        }

        #endregion
}
