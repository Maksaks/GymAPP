
using Course_project_GYMAPP.Domain.Entity;
using Course_project_GYMAPP.Domain.Response;
using Course_project_GYMAPP.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(UserRegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(UserLoginViewModel model);

        Task<BaseResponse<bool>> EditUserData(string lastname, UserEditDataViewModel model);
        Task<BaseUser> CheckName(string name);
    }
}
