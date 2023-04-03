using FluentValidation;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.Validators;

public class PersonValidator : AbstractValidator<Person>
{
        #region [Constructor]

        public PersonValidator() =>
                RuleFor(x => x.NationalCode).NotEmpty().Length(10);

        #endregion
}
