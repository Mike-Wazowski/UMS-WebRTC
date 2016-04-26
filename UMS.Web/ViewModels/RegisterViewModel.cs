using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UMS.Web.Validators;

namespace UMS.Web.ViewModels
{
    [Validator(typeof(RegisterViewModelValidator))]
    public class RegisterViewModel: BaseViewModel
    {
        [Display(Name = "Imie")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Hasło")]
        public string Password { get; set; }
        [Display(Name = "Potwierdzenie hasła")]
        public string PasswordConfirmation { get; set; }
    }
}