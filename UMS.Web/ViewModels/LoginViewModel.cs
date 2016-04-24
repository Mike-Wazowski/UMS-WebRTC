using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UMS.Web.Validators;

namespace UMS.Web.ViewModels
{
    [Validator(typeof(LoginViewModelValidator))]
    public class LoginViewModel
    {
        public string Email { get; set; }
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }
}