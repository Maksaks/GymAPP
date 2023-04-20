using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.Domain.Enum
{
    public enum StatusCode
    {
        UserNotFound,
        OK = 200,
        InternalServerError = 500
    }
}
