using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Course_project_GYMAPP.Domain.ViewModels
{
    public class UserLoginViewModel
    {
        [Display(Name = "Введіть ім'я")]
        [Required(ErrorMessage = "Обов'язкове поле ім'я")]
        public string Name { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Вкажіть пароль")]
        public string Password { get; set; }
    }
}
