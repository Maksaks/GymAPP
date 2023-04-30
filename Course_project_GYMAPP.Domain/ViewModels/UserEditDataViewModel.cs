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
        public string Name { get; set; }
        public string Number { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime CardBefore { get; set; }
        public DateTime LastVisit { get; set; }
        public DateTime DataReg { get; set; }
    }
}
