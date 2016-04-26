using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMS.Web.ViewModels;

namespace UMS.Web.Validators
{
    public class RegisterViewModelValidator: AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().Length(0, 254);
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().Length(6, 30);
            RuleFor(x => x.PasswordConfirmation).NotEmpty().Equal(x => x.Password).WithMessage("Hasła nie są takie same.");
        }
    }
}