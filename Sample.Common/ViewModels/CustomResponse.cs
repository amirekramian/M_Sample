using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.ViewModels;

public class CustomResponse<T>
{
        #region [Properties]

        public T? Data { get; set; }

        public long RecordsTotal { get; set; }

        public long RecordsFiltered { get; set; }

        public bool IsSuccess { get; set; }

        public short Result { get; set; }

        public string? Message { get; set; }

        public dynamic? ChangedId { get; set; }

        #endregion
}

public class CustomResponse
{
        #region [Properties]

        public dynamic? Data { get; set; }

        public long RecordsTotal { get; set; }

        public long RecordsFiltered { get; set; }

        public bool IsSuccess { get; set; }

        public short Result { get; set; }

        public string? Message { get; set; }

        public dynamic? ChangedId { get; set; }

        #endregion
}