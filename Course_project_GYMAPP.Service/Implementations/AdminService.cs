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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }
        public async Task<BaseResponse<bool>> CreateAdmin(AdminRegisterViewModel adminVM)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var admin = new Admin()
                {
                    Name = adminVM.Name,
                    Password = adminVM.Password
                };
                baseResponse.Data = await adminRepository.Create(admin);
                baseResponse.Description = "Адміна зареєстровано";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateAdmin] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteAdmin(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var admin = await adminRepository.Get(id);
                if (admin == null)
                {
                    baseResponse.Description = "Адміна не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = await adminRepository.Delete(admin);
                baseResponse.Description = "Адміга видалено";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteAdmin] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Admin>> GetAdmin(int id)
        {
            var baseResponse = new BaseResponse<Admin>();
            try
            {
                var admin = await adminRepository.Get(id);
                if (admin == null)
                {
                    baseResponse.Description = "Адміна не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = admin;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Admin>()
                {
                    Description = $"[GetAdmin by id] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Admin>> GetAdminByName(string name)
        {
            var baseResponse = new BaseResponse<Admin>();
            try
            {
                var admin = await adminRepository.GetByName(name);
                if (admin == null)
                {
                    baseResponse.Description = "Адміна не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = admin;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Admin>()
                {
                    Description = $"[GetAdmin by name] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<Admin>>> GetAdmins()
        {
            var baseResponse = new BaseResponse<IEnumerable<Admin>>();
            try
            {
                var admins = await adminRepository.Select();
                if (admins.Count == 0)
                {
                    baseResponse.Description = "Тренерів не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = admins;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Admin>>()
                {
                    Description = $"[GetAdmins] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
