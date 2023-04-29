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
    public interface IAdminService
    {
        public Task<BaseResponse<bool>> CreateAdmin(AdminRegisterViewModel adminVM);
        public Task<BaseResponse<IEnumerable<Admin>>> GetAdmins();
        public Task<BaseResponse<Admin>> GetAdmin(int id);
        public Task<BaseResponse<Admin>> GetAdminByName(string name);
        public Task<BaseResponse<bool>> DeleteAdmin(int id);
        public Task<BaseResponse<bool>> EditAdmin(AdminEditAdminViewModel userVM);
        public Task<BaseResponse<List<Admin>>> Search(string pattern);
    }
}
