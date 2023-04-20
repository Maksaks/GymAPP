using Course_project_GYMAPP.Domain.Entity;
using Course_project_GYMAPP.Domain.Response;
using Course_project_GYMAPP.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.Service.Interfaces
{
    public interface IUserService
    {
        public Task<IBaseResponse<bool>> CreateUser(UserViewModel userVM);
        public Task<IBaseResponse<IEnumerable<User>>> GetUsers();
        public Task<IBaseResponse<User>> GetUser(int id);
        public Task<IBaseResponse<User>> GetUserByName(string name);
        public Task<IBaseResponse<bool>> DeleteUser(int id);
        public Task<IBaseResponse<User>> EditUser(int id, UserViewModel userVM);
    }
}
