using Course_project_GYMAPP.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.DAL.Repositories
{
    public class InGymUserRepository
    {
        private readonly AppDbContext appDb;

        public InGymUserRepository(AppDbContext appDb)
        {
            this.appDb = appDb;
        }
        public async Task<bool> Create(User entity)
        {
            await appDb.InGymUsers.AddAsync(entity);
            await appDb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(User entity)
        {
            appDb.InGymUsers.Remove(entity);
            await appDb.SaveChangesAsync();
            return true;
        }

        public async Task<User> Get(int id)
        {
            return await appDb.InGymUsers.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<List<User>> Select()
        {
            return await appDb.InGymUsers.ToListAsync();
        }

        public async Task<User> Update(User entity)
        {
            appDb.InGymUsers.Update(entity);
            await appDb.SaveChangesAsync();
            return entity;
        }
    }
}
