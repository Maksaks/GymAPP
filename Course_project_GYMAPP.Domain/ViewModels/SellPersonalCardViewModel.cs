using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Course_project_GYMAPP.Domain.ViewModels
{
    public class SellPersonalCardViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public float Price { get; set; }
    }
}
