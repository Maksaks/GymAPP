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
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository trainerRepository;

        public TrainerService(ITrainerRepository trainerRepository)
        {
            this.trainerRepository = trainerRepository;
        }
        public async Task<BaseResponse<bool>> CreateTrainer(TrainerRegisterViewModel trainerVM)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var trainer = new Trainer()
                {
                    Name = trainerVM.Name,
                    Password = trainerVM.Password,
                    Age = trainerVM.Age,
                    Number = trainerVM.Number,
                    ImgPath= trainerVM.ImgPath,
                    AboutInfo= trainerVM.AboutInfo,
                    DateReg = DateTime.Now
                };
                baseResponse.Data = await trainerRepository.Create(trainer);
                baseResponse.Description = "Тренера зареєстровано";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateTrainer] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteTrainer(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var trainer = await trainerRepository.Get(id);
                if (trainer == null)
                {
                    baseResponse.Description = "Тренера не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = await trainerRepository.Delete(trainer);
                baseResponse.Description = "Тренера видалено";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteTrainer] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Trainer>> EditTrainer(int id, TrainerEditViewModel trainerVM)
        {
            var baseResponse = new BaseResponse<Trainer>();
            try
            {
                var user = await trainerRepository.Get(id);

                if (user == null)
                {
                    baseResponse.Description = "Тренера не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                user.Name = trainerVM.Name;
                user.Age = trainerVM.Age;
                user.Number = trainerVM.Number;
                user.ImgPath = trainerVM.ImgPath;
                user.AboutInfo = trainerVM.AboutInfo;

                baseResponse.Data = await trainerRepository.Update(user);
                baseResponse.Description = "Інформацію про тренера оновлено";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Trainer>()
                {
                    Description = $"[EditTrainer] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Trainer>> GetTrainer(int id)
        {
            var baseResponse = new BaseResponse<Trainer>();
            try
            {
                var trainer = await trainerRepository.Get(id);
                if (trainer == null)
                {
                    baseResponse.Description = "Тренера не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = trainer;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Trainer>()
                {
                    Description = $"[GetTrainer by id] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Trainer>> GetTrainerByName(string name)
        {
            var baseResponse = new BaseResponse<Trainer>();
            try
            {
                var trainer = await trainerRepository.GetByName(name);
                if (trainer == null)
                {
                    baseResponse.Description = "Тренера не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = trainer;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Trainer>()
                {
                    Description = $"[GetTrainer by name] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<Trainer>>> GetTrainers()
        {
            var baseResponse = new BaseResponse<IEnumerable<Trainer>>();
            try
            {
                var trainers = await trainerRepository.Select();
                if (trainers.Count == 0)
                {
                    baseResponse.Description = "Тренерів не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = trainers;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Trainer>>()
                {
                    Description = $"[GetTrainers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
