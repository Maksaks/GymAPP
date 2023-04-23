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
    public class InGymUserRepository : IGymUserRepository
    {
        private readonly AppDbContext appDb;

        public InGymUserRepository(AppDbContext appDb)
        {
            this.appDb = appDb;
        }
        public async Task<bool> Create(InGymUser entity)
        {
            await appDb.InGymUser.AddAsync(entity);
            await appDb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(InGymUser entity)
        {
            appDb.InGymUser.Remove(entity);
            await appDb.SaveChangesAsync();
            return true;
        }

        public async Task<InGymUser> Get(int id)
        {
            return await appDb.InGymUser.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<InGymUser> GetByName(string Name)
        {
            return await appDb.InGymUser.FirstOrDefaultAsync(x => x.Name == Name);
        }

        public async Task<List<InGymUser>> Select()
        {
            return await appDb.InGymUser.ToListAsync();
        }

        public async Task<InGymUser> Update(InGymUser entity)
        {
            appDb.InGymUser.Update(entity);
            await appDb.SaveChangesAsync();
            return entity;
        }
    }
}
