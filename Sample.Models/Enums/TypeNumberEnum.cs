using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Model.Enums;

public enum TypeNumberEnum : byte
{
        [Description("شماره تلفن خط ثابت")]
        TelephoneNumber = 1,

        [Description("شماره تلفن موبایل")]
        MobileNumber = 2,
}
