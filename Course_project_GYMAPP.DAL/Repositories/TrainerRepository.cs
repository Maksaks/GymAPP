using Course_project_GYMAPP.DAL.Interfaces;
using Course_project_GYMAPP.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.DAL.Repositories
{
    public class TrainerRepository : ITrainer
    {
        private readonly AppDbContext appDb;

        public TrainerRepository(AppDbContext appDb)
        {
            this.appDb = appDb;
        }
        public async Task<bool> Create(Trainer entity)
        {
            await appDb.Trainer.AddAsync(entity);
            await appDb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Trainer entity)
        {
            appDb.Trainer.Remove(entity);
            await appDb.SaveChangesAsync();
            return true;
        }

        public async Task<Trainer> Get(int id)
        {
            return await appDb.Trainer.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Trainer> GetByName(string Name)
        {
            return await appDb.Trainer.FirstOrDefaultAsync(x => x.Name == Name);
        }

        public async Task<List<Trainer>> Select()
        {
            return await appDb.Trainer.ToListAsync();
        }

        public async Task<Trainer> Update(Trainer entity)
        {
            appDb.Trainer.Update(entity);
            await appDb.SaveChangesAsync();
            return entity;
        }
    }
}
