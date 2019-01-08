using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace FreelancerArticle.Web.Models
{
    public class LoginViewModel
    {
		[Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
		[Required]
        [RegularExpression(@"^[a-zA-Z0-9]{1,8}", ErrorMessage = "Некорректный логин. Поле должно состоять только из латинских букв и иметь размер до 8 символов.")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Имя")]
        [RegularExpression(@"^[а-яА-Я]{1,30}", ErrorMessage = "Некорректное имя. Поле должно состоять только из русских букв и иметь размер до 30 символов.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[а-яА-Я]{1,30}", ErrorMessage = "Некорректная фамилия. Поле должно состоять только из русских букв и иметь размер до 30 символов.")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[а-яА-Я]{1,30}", ErrorMessage = "Некорректное отчество. Поле должно состоять только из русских букв и иметь размер до 30 символов.")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required]
        [RegularExpression(@"((?=.*\d)(?=.*[A-z]).{4,8})", ErrorMessage = "Некорректный пароль. Поле должно состоять из латинских букв и цифр (обязательно хотя бы 1 цифра). Иметь размер от 4 до 8 символов.")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}