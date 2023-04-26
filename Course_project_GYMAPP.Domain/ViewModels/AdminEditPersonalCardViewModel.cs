using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Course_project_GYMAPP.Domain.ViewModels
{
    public class AdminEditPersonalCardViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Введіть назву")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public string Name { get; set; }
        [Display(Name = "Введіть тривалість у днях")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public string Duration { get; set; }
        [Display(Name = "Введіть вартість")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public float Price { get; set; }
    }
}
