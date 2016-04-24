using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMS.Web.ViewModels;

namespace UMS.Web.Validators
{
    public class LoginViewModelValidator: AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().Length(0, 254);
            RuleFor(x => x.Password).NotEmpty().Length(6, 30);
        }
    }
}