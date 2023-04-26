using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Course_project_GYMAPP.Domain.ViewModels
{
    public class AdminEditUserViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Введіть ім'я")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public string Name { get; set; }
        [Display(Name = "Введіть телефон")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public string Number { get; set; }
        [Display(Name = "Введіть вік")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public int Age { get; set; }
        [Display(Name = "Введіть термін дії клубної картки")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public DateTime CardBefore { get; set; }
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
