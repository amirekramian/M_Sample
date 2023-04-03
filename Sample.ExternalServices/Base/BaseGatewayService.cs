using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ExternalServices.Base;

public class BaseGatewayService
{
        #region [Field(s)]

        protected readonly string? AccessKey;

        protected readonly string BaseAddress;

        #endregion

        #region [Constructor]

        public BaseGatewayService(string? accessKey, string baseAddress)
        {
                AccessKey = accessKey;
                BaseAddress = baseAddress;
        }

        #endregion
}
