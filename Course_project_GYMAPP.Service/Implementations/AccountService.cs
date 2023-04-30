using Course_project_GYMAPP.DAL.Interfaces;
using Course_project_GYMAPP.DAL.Repositories;
using Course_project_GYMAPP.Domain.Encryption;
using Course_project_GYMAPP.Domain.Entity;
using Course_project_GYMAPP.Domain.Enum;
using Course_project_GYMAPP.Domain.Response;
using Course_project_GYMAPP.Domain.ViewModels;
using Course_project_GYMAPP.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository;
        private readonly ITrainerRepository trainerRepository;
        private readonly IAdminRepository adminRepository;
        private readonly IGymUserRepository gymUserRepository;

        public AccountService(IUserRepository userRepository, ITrainerRepository trainerRepository,
            IAdminRepository adminRepository, IGymUserRepository gymUserRepository)
        {
            this.userRepository = userRepository;
            this.trainerRepository = trainerRepository;
            this.adminRepository = adminRepository;
            this.gymUserRepository = gymUserRepository;
        }

        public async Task<BaseResponse<bool>> EditUserData(string lastname, UserEditDataViewModel model)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var user = new BaseUser();
                if(lastname != model.Name)
                {
                    user = await CheckName(model.Name);
                    if (user != null)
                    {
                        baseResponse.Description = "Це імя вже зайнято";
                        baseResponse.StatusCode = StatusCode.ChangeName;
                        return baseResponse;
                    }
                    var gymUser = await gymUserRepository.GetByName(lastname);
                    if (gymUser != null)
                    {
                       gymUser.Name = model.Name;
                        await gymUserRepository.Update(gymUser);
                    }
                }
                
                var eduser = await userRepository.GetByName(lastname);
                eduser.Name = model.Name;
                eduser.Age= model.Age;
                eduser.Number = model.Number;
                if (model.Password != null)
                {
                    if(model.Password.Length <= 8)
                    {
                        baseResponse.Description = "Пароль має бути понад 8 символів";
                        baseResponse.StatusCode = StatusCode.InternalServerError;
                        return baseResponse;
                    }
                    if (model.ConfirmPassword != null)
                    {
                        if (model.Password == model.ConfirmPassword)
                        {
                            user.Password = Encryption.EncrPassowrd(model.Password);
                        }
                        else
                        {
                            baseResponse.Description = "Паролі не співпадають";
                            baseResponse.StatusCode = StatusCode.InternalServerError;
                            return baseResponse;
                        }
                    }
                    else
                    {
                        baseResponse.Description = "Підтвердіть пароль";
                        baseResponse.StatusCode = StatusCode.InternalServerError;
                        return baseResponse;
                    }
                }


                await userRepository.Update(eduser);
                return new BaseResponse<bool>()
                {
                    Data= true,
                    Description = "Дані оновлено",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[EditUserData] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(UserLoginViewModel model)
        {
            try
            {
                var user = await CheckName(model.Name);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Користувача з таким імям не знайдено. Необхідно зареєструватися"
                    };
                }

                if (user.Password != Encryption.EncrPassowrd(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Некоректні дані для входу!"
                    };
                }
                if(user is User)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Data = Authenticate(user, "User"),
                        Description = "Користувача авторизовано",
                        StatusCode = StatusCode.OK
                    };
                }
                else if (user is Trainer)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Data = Authenticate(user, "Trainer"),
                        Description = "Вас авторизовано як тренера",
                        StatusCode = StatusCode.OK
                    };
                }
                else
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Data = Authenticate(user, "Admin"),
                        Description = "Вас авторизовано як адміністратора",
                        StatusCode = StatusCode.OK
                    };
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"[Login] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(UserRegisterViewModel model)
        {
            try
            {
                if (await CheckName(model.Name) != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Користувача з таким імям вже зареєстровано у системі"
                    };
                }

                var newUser = new User()
                {
                    Name = model.Name,
                    Number = model.Number,
                    Age = model.Age,
                    Password = Encryption.EncrPassowrd(model.Password),
                    CardBefore = DateTime.MinValue,
                    LastVisit = DateTime.MinValue,
                    DateReg = DateTime.Now
                };
                await userRepository.Create(newUser);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = Authenticate(newUser, "User"),
                    Description = "Користувача зареєстровано",
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"[Register] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<BaseUser> CheckName(string name)
        {
            var user = await userRepository.GetByName(name);
            if (user != null)
            {
                return user;
            }
            else
            {
                var trainer = await trainerRepository.GetByName(name);
                if (trainer != null)
                {
                    return trainer;
                }
                else
                {
                    var admin = await adminRepository.GetByName(name);
                    if (admin != null)
                    {
                        return admin;
                    }
                }
            }
            return null;
        }
        private ClaimsIdentity Authenticate(BaseUser user, string Role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, Role)
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
