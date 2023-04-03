using Dapper;
using Sample.Common.Constants;
using Sample.Common.Helpers;
using Sample.Common.ViewModels;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sieve.Extensions.MethodInfoExtended;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sample.DapperBusiness;

public class PersonBusiness
{
        #region [Field(s)]

        private readonly SqlConnection _sqlConnection;

        #endregion

        #region [Constructor]

        public PersonBusiness(SqlConnection sqlConnection)
        {
                _sqlConnection = sqlConnection;
        }

        #endregion

        public async Task<CustomResponse> LoadInfoByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken = new())
        {
                var person =
                        await _sqlConnection.QuerySingleOrDefaultAsync<PersonViewModel>(
                                new CommandDefinition(
                                QueriesConstants.PersonWithNationalCode.AddSoftDeleteWithAnd(),
                                new { nationalCode },
                                cancellationToken : cancellationToken));

                if (person == null)
                        return new CustomResponse
                        {
                                Message = "Empty",
                                IsSuccess = false
                        };

                return new CustomResponse
                {
                        Data = person,
                        Message = "Find",
                        IsSuccess = true
                };
        }

        public async Task<CustomResponse> LoadPhoneByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken = new())
        {              
                var personContactInfo =
                        await _sqlConnection.QuerySingleOrDefaultAsync<PersonContactViewModel>(
                                new CommandDefinition(
                                QueriesConstants.PersonPhoneWithNationalCode,
                                new { nationalCode },
                                cancellationToken : cancellationToken));

                if (personContactInfo == null)
                        return new CustomResponse
                        {
                                Message = "Empty",
                                IsSuccess = false
                        };

                return new CustomResponse
                {
                        Data = personContactInfo,
                        Message = "Find",
                        IsSuccess = true
                };
        }
}
