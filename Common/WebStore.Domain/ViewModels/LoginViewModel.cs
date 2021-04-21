using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels
{
    public class LoginViewModel
    {
        [Required, MaxLength(256)]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; init; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; init; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; init; }
    }
}
