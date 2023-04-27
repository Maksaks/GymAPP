using Course_project_GYMAPP.DAL.Interfaces;
using Course_project_GYMAPP.Domain.Encryption;
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
        private readonly IPersonalCardRepository personalCardRepository;

        public UserService(IUserRepository userRepository, IPersonalCardRepository personalCardRepository)
        {
            this._userRepository = userRepository;
            this.personalCardRepository = personalCardRepository;
        }

        public async Task<BaseResponse<User>> GetUser(int id)
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
                baseResponse.StatusCode = StatusCode.OK;
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
        public async Task<BaseResponse<User>> GetUserByName(string name)
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
                baseResponse.StatusCode = StatusCode.OK;
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
        public async Task<BaseResponse<IEnumerable<User>>> GetUsers()
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
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>()
                {
                    Description = $"[GetUsers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteUser(int id)
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
        public async Task<BaseResponse<bool>> CreateUser(UserRegisterViewModel userVM)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var user = new User()
                {
                    Name = userVM.Name,
                    Password = Encryption.EncrPassowrd(userVM.Password),
                    Age = userVM.Age,
                    Number = userVM.Number,
                    CardBefore = DateTime.MinValue,
                    LastVisit = DateTime.MinValue,
                    DateReg = DateTime.Now
                };
                baseResponse.Data = await _userRepository.Create(user);
                baseResponse.Description = "Користувача зареєстровано";
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

        public async Task<BaseResponse<User>> EditUser(int id, UserEditDataViewModel userVM)
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

                user.Name = userVM.Name;
                user.Age = userVM.Age;
                user.Number = userVM.Number;

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
        public async Task<BaseResponse<User>> EditUserCard(User user, PersonalCard card)
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                if (user == null)
                {
                    baseResponse.Description = "Необхідно авторизуватися";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                if (user.CardBefore < DateTime.Today)
                {
                    user.CardBefore = DateTime.Today.AddDays(Convert.ToDouble(card.Duration));
                }
                else
                {
                    user.CardBefore = user.CardBefore.AddDays(Convert.ToDouble(card.Duration));
                }


                baseResponse.Data = await _userRepository.Update(user);
                baseResponse.Description = "Інформацію про термін дії клубної картки оновлено";
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
        public async Task<BaseResponse<bool>> EditUser(AdminEditUserViewModel userVM)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var user = await _userRepository.Get(userVM.ID);

                if (user == null)
                {
                    baseResponse.Description = "Користувача не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                user.Name = userVM.Name;
                user.Age = userVM.Age;
                user.Number = userVM.Number;
                user.CardBefore = userVM.CardBefore;
                if(userVM.Password != null)
                {
                    user.Password = Encryption.EncrPassowrd(userVM.Password);
                }
                
                await _userRepository.Update(user);
                baseResponse.Description = "Інформацію про користувача оновлено";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[EditUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<BaseResponse<bool>> NewCardForUser(NewCardViewModel cardViewModel)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var user = await _userRepository.GetByName(cardViewModel.Name);
                var cardDur = (await personalCardRepository.Get(cardViewModel.Id)).Duration;
                if (user == null)
                {
                    var newuser = new User()
                    {
                        Name = cardViewModel.Name,
                        Password = Encryption.EncrPassowrd(cardViewModel.Name),
                        Age = 0,
                        Number = "000-000-00-00",
                        CardBefore = DateTime.Today.AddDays(Convert.ToDouble(cardDur)),
                        LastVisit = DateTime.MinValue,
                        DateReg = DateTime.Now
                    };
                    await _userRepository.Create(newuser);
                    baseResponse.Description = "Користувача зареєстровано та оформлено абонемент. Тимчасовий пароль користувача - його логін";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                if (user.CardBefore < DateTime.Today)
                {
                    user.CardBefore = DateTime.Today.AddDays(Convert.ToDouble(cardDur));
                }
                else
                {
                    user.CardBefore = user.CardBefore.AddDays(Convert.ToDouble(cardDur));
                }


                await _userRepository.Update(user);
                baseResponse.Data = true;
                baseResponse.Description = "Інформацію про термін дії клубної картки оновлено";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[NewCardForUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}