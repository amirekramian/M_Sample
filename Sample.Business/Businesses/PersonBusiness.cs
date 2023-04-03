using Sample.Common.ViewModels;
using Sample.DataAccess.Contracts;
using Sample.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Sample.Business.Base;
using Sample.DataAccess.Repositories;
using Sample.Model;

namespace Sample.Business.Businesses;

public class PersonBusiness : BaseBusiness<Person>
{
        #region [Field]

        private readonly UnitOfWork _unitOfWork;

        #endregion

        #region [Constructor]

        public PersonBusiness(IUnitOfWork unitOfWork) :
            base(unitOfWork, (unitOfWork as UnitOfWork)?.PersonRepository!)
        {
                _unitOfWork = (UnitOfWork)unitOfWork;
        }

        #endregion

        #region [Method(s)]
        public async Task<CustomResponse> LoadByNationalCodeAsync(string nationalCode,
        CancellationToken cancellationToken = new())
        {
                await _unitOfWork.PersonRepository!.LoadByNationalCodeAsync(nationalCode);
                await _unitOfWork.CommitAsync(cancellationToken);
                var data = await _unitOfWork.PersonRepository!.LoadByNationalCodeAsync(nationalCode, cancellationToken);
                return new CustomResponse
                {
                        Data = data,
                        Message = "Data Loaded",
                        IsSuccess = true
                };
        }
        #endregion
}
