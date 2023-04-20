using Course_project_GYMAPP.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.DAL.Repositories
{
    public class PersonalCardRepository
    {
        private readonly AppDbContext appDb;

        public PersonalCardRepository(AppDbContext appDb)
        {
            this.appDb = appDb;
        }
        public async Task<bool> Create(PersonalCard entity)
        {
            await appDb.PersonalCards.AddAsync(entity);
            await appDb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(PersonalCard entity)
        {
            appDb.PersonalCards.Remove(entity);
            await appDb.SaveChangesAsync();
            return true;
        }

        public async Task<PersonalCard> Get(int id)
        {
            return await appDb.PersonalCards.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<List<PersonalCard>> Select()
        {
            return await appDb.PersonalCards.ToListAsync();
        }

        public async Task<PersonalCard> Update(PersonalCard entity)
        {
            appDb.PersonalCards.Update(entity);
            await appDb.SaveChangesAsync();
            return entity;
        }
    }
}
