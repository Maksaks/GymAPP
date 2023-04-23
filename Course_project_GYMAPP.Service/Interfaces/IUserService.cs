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
        public Task<BaseResponse<bool>> CreateUser(UserRegisterViewModel userVM);
        public Task<BaseResponse<IEnumerable<User>>> GetUsers();
        public Task<BaseResponse<User>> GetUser(int id);
        public Task<BaseResponse<User>> GetUserByName(string name);
        public Task<BaseResponse<bool>> DeleteUser(int id);
        public Task<BaseResponse<User>> EditUser(int id, UserEditDataViewModel userVM);
        public Task<BaseResponse<User>> EditUserCard(User user, PersonalCard card);
    }
}
