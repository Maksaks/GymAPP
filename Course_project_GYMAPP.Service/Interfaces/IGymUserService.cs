
using Course_project_GYMAPP.Domain.Entity;
using Course_project_GYMAPP.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.Service.Interfaces
{
    public interface IGymUserService
    {
        public Task<BaseResponse<bool>> AddUser(string Name);
        public Task<BaseResponse<List<InGymUser>>> GetUsers();
        public Task<BaseResponse<bool>> DeleteUser(int id);
        public Task<BaseResponse<int>> GetCountOfUsersInGym();
    }
}
