using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.Domain.Entity
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Number { get; set; }
        public string ImgPath { get; set; }
        public string AboutInfo { get; set; }
        public DateTime DateReg  { get; set; }
    }
}
