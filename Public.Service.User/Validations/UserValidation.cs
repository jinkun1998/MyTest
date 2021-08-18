using FluentValidation;
using Public.Service.User.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public.Service.User.Validations
{
    public class UserValidation : AbstractValidator<ApiUserModel>
    {
        public UserValidation()
        {
            RuleFor(it => it.Username)
                .NotNull().WithMessage("Username is not null")
                .NotEmpty().WithMessage("Username is not empty")
                .MinimumLength(5).WithMessage("Username's length must greater or equal 5");
            RuleFor(it => it.Name)
                .NotNull().WithMessage("Name is not null")
                .NotEmpty().WithMessage("Name is not empty")
                .MinimumLength(1).WithMessage("Name's length must equal or greater than 1");
            RuleFor(it => it.Age)
                .GreaterThan(0).WithMessage("Age must greater than 0");
        }
    }
}
