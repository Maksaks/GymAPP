﻿using System;
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
        [Display(Name = "ім'я")]
        [Required(ErrorMessage = "Ім'я - обов'язкове поле")]
        public string Name { get; set; }
        [Display(Name = "телефон")]
        [Required(ErrorMessage = "Телефон  - обов'язкове поле")]
        public string Number { get; set; }
        [Range(14,55)]
        [Display(Name = "вік")]
        [Required(ErrorMessage = "Вік  - обов'язкове поле")]
        public int Age { get; set; }
        public DateTime CardBefore { get; set; }
    }
}
