using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.Domain.Enum
{
    public enum StatusCode
    {
        UserNotFound=0,
        ChangeName=1,
        GymUsersListEmpty = 2,
        OK = 200,
        InternalServerError = 500
    }
}
