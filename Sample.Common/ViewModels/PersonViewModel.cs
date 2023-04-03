using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.ViewModels;

public class PersonViewModel
{
        #region [Properties]

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Family { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        #endregion
}
