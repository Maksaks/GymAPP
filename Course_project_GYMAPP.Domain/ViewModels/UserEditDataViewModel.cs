using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Course_project_GYMAPP.Domain.ViewModels
{
    public class UserEditDataViewModel
    {
        [Display(Name = "Введіть ім'я")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public string Name { get; set; }
        [Display(Name = "Введіть телефон")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public string Number { get; set; }
        [Display(Name = "Введіть вік")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public int Age { get; set; }
    }
}
