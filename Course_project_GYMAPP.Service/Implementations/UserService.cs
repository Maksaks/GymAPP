using Course_project_GYMAPP.DAL.Interfaces;
using Course_project_GYMAPP.Domain.Entity;
using Course_project_GYMAPP.Domain.Enum;
using Course_project_GYMAPP.Domain.Response;
using Course_project_GYMAPP.Domain.ViewModels;
using Course_project_GYMAPP.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Course_project_GYMAPP.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<IBaseResponse<User>> GetUser(int id)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = await _userRepository.Get(id);
                if (user == null)
                {
                    baseResponse.Description = "Користувача не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = user;
                baseResponse.StatusCode = StatusCode.InternalServerError;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUser by id] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<User>> GetUserByName(string name)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = await _userRepository.GetByName(name);
                if (user == null)
                {
                    baseResponse.Description = "Користувача не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = user;
                baseResponse.StatusCode = StatusCode.InternalServerError;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUser by name] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<IEnumerable<User>>> GetUsers()
        {
            var baseResponse = new BaseResponse<IEnumerable<User>>();
            try
            {
                var users = await _userRepository.Select();
                if (users.Count == 0)
                {
                    baseResponse.Description = "Користувачів не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = users;
                baseResponse.StatusCode = StatusCode.InternalServerError;
                return baseResponse;
            }
            catch(Exception ex) 
            {
                return new BaseResponse<IEnumerable<User>>()
                {
                    Description = $"[GetUsers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUser(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var user = await _userRepository.Get(id);
                if (user == null)
                {
                    baseResponse.Description = "Користувача не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                
                baseResponse.Data = await _userRepository.Delete(user);
                baseResponse.Description = "Користувача видалено";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<bool>> CreateUser(UserViewModel userVM)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var user = new User()
                {
                    Name = userVM.Name,
                    Password = userVM.Password,
                    Age = userVM.Age,
                    Number = userVM.Number,
                    CardBefore = DateTime.MinValue,
                    LastVisit = DateTime.MinValue,
                    DateReg = DateTime.Now
                };
                baseResponse.Data = await _userRepository.Create(user);
                baseResponse.Description = "Користувача додано";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<User>> EditUser(int id, UserViewModel userVM)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = await _userRepository.Get(id);

                if(user == null)
                {
                    baseResponse.Description = "Користувача не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                user.Name = userVM.Name;
                user.Password = userVM.Password;
                user.Age = userVM.Age;
                user.Number = userVM.Number;
                user.CardBefore = userVM.CardBefore;
                user.LastVisit = userVM.LastVisit;

                baseResponse.Data = await _userRepository.Update(user);
                baseResponse.Description = "Інформацію про користувача оновлено";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[EditUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
