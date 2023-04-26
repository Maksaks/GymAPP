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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.Service.Implementations
{
    public class PersonalCardService : IPersonalCardService
    {
        private readonly IPersonalCardRepository personalCardRepository;

        public PersonalCardService(IPersonalCardRepository personalCardRepository)
        {
            this.personalCardRepository = personalCardRepository;
        }

        public async Task<BaseResponse<bool>> CreatePersonalCard(PersonalCardViewModel cardVM)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var card = new PersonalCard()
                {
                    Name = cardVM.Name,
                    Duration = cardVM.Duration,
                    Price = cardVM.Price
                };
                baseResponse.Data = await personalCardRepository.Create(card);
                baseResponse.Description = "Картку додано";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreatePersonalCard] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> DeletePersonalCard(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var card = await personalCardRepository.Get(id);
                if (card == null)
                {
                    baseResponse.Description = "Картку не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = await personalCardRepository.Delete(card);
                baseResponse.Description = "Карту видалено";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeletePersonalCard] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<PersonalCard>> EditPersonalCard(int id, PersonalCardViewModel cardVM)
        {
            var baseResponse = new BaseResponse<PersonalCard>();
            try
            {
                var card = await personalCardRepository.Get(id);

                if (card == null)
                {
                    baseResponse.Description = "Картку не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                card.Name = cardVM.Name;
                card.Duration = cardVM.Duration;
                card.Price = cardVM.Price;

                baseResponse.Data = await personalCardRepository.Update(card);
                baseResponse.Description = "Інформацію про картку оновлено";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<PersonalCard>()
                {
                    Description = $"[EditPersonalCard] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<PersonalCard>> GetPersonalCard(int id)
        {
            var baseResponse = new BaseResponse<PersonalCard>();
            try
            {
                var card = await personalCardRepository.Get(id);
                if (card == null)
                {
                    baseResponse.Description = "Користувача не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = card;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<PersonalCard>()
                {
                    Description = $"[GetPersonalCard by id] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<PersonalCard>>> GetPersonalCards()
        {
            var baseResponse = new BaseResponse<IEnumerable<PersonalCard>>();
            try
            {
                var cards = await personalCardRepository.Select();
                if (cards.Count == 0)
                {
                    baseResponse.Description = "Картки не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = cards;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<PersonalCard>>()
                {
                    Description = $"[GetPersonalCards] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> EditPersonalCard(AdminEditPersonalCardViewModel userVM)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var user = await personalCardRepository.Get(userVM.ID);

                if (user == null)
                {
                    baseResponse.Description = "Клубну картку не знайдено";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                user.Name = userVM.Name;
                user.Duration = userVM.Duration;
                user.Price = userVM.Price;

                await personalCardRepository.Update(user);
                baseResponse.Description = "Інформацію про клубну картку оновлено";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[EditPersonalCard] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
