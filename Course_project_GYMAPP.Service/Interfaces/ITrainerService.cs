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
    public interface ITrainerService
    {
        public Task<BaseResponse<bool>> CreateTrainer(TrainerRegisterViewModel userVM);
        public Task<BaseResponse<IEnumerable<Trainer>>> GetTrainers();
        public Task<BaseResponse<Trainer>> GetTrainer(int id);
        public Task<BaseResponse<Trainer>> GetTrainerByName(string name);
        public Task<BaseResponse<bool>> DeleteTrainer(int id);
        public Task<BaseResponse<Trainer>> EditTrainer(int id, TrainerEditViewModel userVM);
    }
}
