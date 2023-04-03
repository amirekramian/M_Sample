using Sample.Business.Base;
using Sample.Common.ViewModels;
using Sample.DataAccess.Contracts;
using Sample.DataAccess;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sample.Business.Businesses;

public class PhoneBusiness : BaseBusiness<Phone>
{
        #region [Field]

        private readonly UnitOfWork _unitOfWork;

        #endregion

        #region [Constructor]

        public PhoneBusiness(IUnitOfWork unitOfWork) :
            base(unitOfWork, (unitOfWork as UnitOfWork)?.PhoneRepository!)
        {
                _unitOfWork = (UnitOfWork)unitOfWork;
        }

        #endregion

        #region [Method(s)]

        public async Task<CustomResponse> LoadByNationalCodeAsync(string nationalCode,
        CancellationToken cancellationToken = new())
        {
                await _unitOfWork.PhoneRepository!.LoadByNationalCodeAsync(nationalCode);
                await _unitOfWork.CommitAsync(cancellationToken);
                var data = await _unitOfWork.PhoneRepository!.LoadByNationalCodeAsync(nationalCode, cancellationToken);
                return new CustomResponse
                {
                        Data = data,
                        Message = "Data Loaded",
                        IsSuccess = true
                };
        }

        public async Task<CustomResponse> CheckPhoneExistAsync(string phone, CancellationToken cancellationToken = new())
        {
                await _unitOfWork.PhoneRepository!.CheckPhoneExistAsync(phone, cancellationToken);
                return new CustomResponse
                {
                        Message = "Duplicate",
                        IsSuccess = true
                };
        }

        #endregion
}
