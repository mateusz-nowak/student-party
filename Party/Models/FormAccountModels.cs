using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Party.Models
{

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "To pole jest obowiązkowe")]
        [DataType(DataType.Password)]
        [Display(Name = "Obecne hasło")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "To pole jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć conajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwórz hasło")]
        [Compare("NewPassword", ErrorMessage = "Hasła nie są identyczne.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required(ErrorMessage = "To pole jest obowiązkowe")]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "To pole jest obowiązkowe")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło użytkownika")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage="To pole jest obowiązkowe")]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "To pole jest obowiązkowe")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "To pole jest obowiązkowe")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Potwórz adres e-mail")]
        [Compare("Email", ErrorMessage = "Adresy e-mail nie są identyczne.")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "To pole jest obowiązkowe")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć conajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie są identyczne.")]
        public string ConfirmPassword { get; set; }
    }
}
