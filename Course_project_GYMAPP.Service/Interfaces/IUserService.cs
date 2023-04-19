using Course_project_GYMAPP.Domain.Entity;
using Course_project_GYMAPP.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.Service.Interfaces
{
    public interface IUserService
    {
        public Task<IBaseResponse<IEnumerable<User>>> GetUsers();
    }
}
