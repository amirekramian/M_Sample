using FluentValidation;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.Validators;

public class PhoneValidator : AbstractValidator<Phone>
{
        public PhoneValidator() =>
                        RuleFor(x => x.Content).NotNull().NotEmpty().Length(11).Must(x => x.StartsWith("09"));
}
