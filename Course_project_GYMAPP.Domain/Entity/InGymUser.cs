using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.Domain.Entity
{
    public class InGymUser : BaseUser
    {
        public DateTime timeInGym { get; set; }
    }
}
