using Course_project_GYMAPP.DAL.Interfaces;
using Course_project_GYMAPP.DAL.Repositories;
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

namespace Course_project_GYMAPP.Service.Implementations
{
    public class InGymUserService : IGymUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IGymUserRepository gymUserRepository;

        public InGymUserService(IGymUserRepository gymUserRepository, IUserRepository userRepository)
        {
            this.gymUserRepository = gymUserRepository;
            this.userRepository = userRepository;
        }

        public async Task<BaseResponse<bool>> AddUser(string Name)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var user = await userRepository.GetByName(Name);
                if (user != null)
                {
                    if(await gymUserRepository.GetByName(Name) == null)
                    {
                        if (user.CardBefore >= DateTime.Today)
                        {
                            var gymUser = new InGymUser()
                            {
                                Name = user.Name,
                                timeInGym = DateTime.Now,
                                Password = user.Password
                            };
                            user.LastVisit = DateTime.Today;
                            await userRepository.Update(user);
                            baseResponse.Data = await gymUserRepository.Create(gymUser);
                            baseResponse.Description = "Відвідувача додано";
                            baseResponse.StatusCode = StatusCode.OK;
                            return baseResponse;
                        }
                        else
                        {
                            baseResponse.Description = "Термін дії клубної картки відвідувача закінчився " + user.CardBefore.ToString();
                            baseResponse.StatusCode = StatusCode.UserNotFound;
                            return baseResponse;
                        }
                    }
                    else
                    {
                        baseResponse.Description = "Наразі відвідувач вже знаходиться у залі";
                        baseResponse.StatusCode = StatusCode.UserNotFound;
                        return baseResponse;
                    }
                    
                }
                else
                {
                    baseResponse.Description = "Користувача не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[AddUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };

            }
        }
        public async Task<BaseResponse<bool>> DeleteUser(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var gymUser = await gymUserRepository.Get(id);
                if (gymUser != null)
                {
                    baseResponse.Data = await gymUserRepository.Delete(gymUser);
                    baseResponse.Description = "Відвідувача видалено";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                else
                {
                    baseResponse.Description = "Відвідувача не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
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

        public async Task<BaseResponse<List<InGymUser>>> GetUsers()
        {
            var baseResponse = new BaseResponse<List<InGymUser>>();

            try
            {
                var gymUsers = await gymUserRepository.Select();
                if (gymUsers != null)
                {
                    baseResponse.Data = gymUsers;
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                else
                {
                    baseResponse.StatusCode = StatusCode.GymUsersListEmpty;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<InGymUser>>()
                {
                    Description = $"[GetUsers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };

            }
        }

        public async Task<BaseResponse<int>> GetCountOfUsersInGym()
        {
            var baseResponse = new BaseResponse<int>();

            try
            {
                var gymUsersCount = await gymUserRepository.GetCountOfUsersInGym();
                if (gymUsersCount != 0)
                {
                    baseResponse.Data = gymUsersCount;
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                else
                {
                    baseResponse.Data = 0;
                    baseResponse.StatusCode = StatusCode.GymUsersListEmpty;
                    return baseResponse;
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse<int>()
                {
                    Description = $"[GetCountOfUsersInGym] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };

            }
        }

        public async Task<BaseResponse<List<InGymUser>>> Search(string pattern)
        {
            var baseResponse = new BaseResponse<List<InGymUser>>();
            try
            {
                baseResponse.Data = await gymUserRepository.Search(pattern);
                baseResponse.Description = "Результати пошуку отримано";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<InGymUser>>()
                {
                    Description = $"[Search] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
