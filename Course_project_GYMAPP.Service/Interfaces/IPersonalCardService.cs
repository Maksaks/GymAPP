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
    public interface IPersonalCardService
    {
        public Task<BaseResponse<bool>> CreatePersonalCard(PersonalCardViewModel cardVM);
        public Task<BaseResponse<IEnumerable<PersonalCard>>> GetPersonalCards();
        public Task<BaseResponse<PersonalCard>> GetPersonalCard(int id);
        public Task<BaseResponse<bool>> DeletePersonalCard(int id);
        public Task<BaseResponse<PersonalCard>> EditPersonalCard(int id, PersonalCardViewModel cardVM);
        public Task<BaseResponse<bool>> EditPersonalCard(AdminEditPersonalCardViewModel userVM);
        public Task<BaseResponse<List<PersonalCard>>> Search(string pattern);
    }
}
