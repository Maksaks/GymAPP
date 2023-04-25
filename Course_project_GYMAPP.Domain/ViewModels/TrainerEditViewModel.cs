using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Course_project_GYMAPP.Domain.ViewModels
{
    public class TrainerEditViewModel
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
        [Display(Name = "Введіть шлях до файлу")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public string ImgPath { get; set; }
        [Display(Name = "Введіть детальну інформацію")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public string AboutInfo { get; set; }

    }
}
