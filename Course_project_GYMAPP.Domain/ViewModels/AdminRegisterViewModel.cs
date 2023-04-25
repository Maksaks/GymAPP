using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Course_project_GYMAPP.Domain.ViewModels
{
    public class AdminRegisterViewModel
    {
        [Display(Name = "Введіть ім'я")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public string Name { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Вкажіть пароль")]
        [MinLength(8, ErrorMessage = "Пароль повинен мати довжину понад 8 символів")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Пароли не співпадають")]
        public string ConfirmPassword { get; set; }
    }
}
