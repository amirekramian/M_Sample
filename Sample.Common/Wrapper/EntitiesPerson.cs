using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.Wrapper;

public class EntitiesPerson
{
        #region [Properties]

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Family { get; set; }

        public DateTime BirthDate { get; set; }

        public string? NationalCode { get; set; }

        public string? NationalNumber { get; set; }

        public string? FatherName { get; set; }

        public bool IsMale { get; set; }

        public bool IsDead { get; set; }

        #endregion
}
