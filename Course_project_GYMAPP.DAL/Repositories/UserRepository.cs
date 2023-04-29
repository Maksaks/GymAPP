using Course_project_GYMAPP.DAL.Interfaces;
using Course_project_GYMAPP.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDb;

        public UserRepository(AppDbContext appDb)
        {
            this.appDb = appDb;
        }

        public async Task<bool> Create(User entity)
        {
            await appDb.User.AddAsync(entity);
            await appDb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(User entity)
        {
            appDb.User.Remove(entity);
            await appDb.SaveChangesAsync();
            return true;
        }

        public async Task<User> Get(int id)
        {
            return await appDb.User.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByName(string Name)
        {
            return await appDb.User.FirstOrDefaultAsync(x => x.Name == Name);
        }

        public async Task<List<User>> Select()
        {
            return await appDb.User.ToListAsync();
        }

        public async Task<User> Update(User entity)
        {
            appDb.User.Update(entity);
            await appDb.SaveChangesAsync();
            return entity;
        }

        public async Task<List<User>> Search(string pattern)
        {
            pattern = "%" + pattern + "%";
            return await appDb.User.Where(p => EF.Functions.Like(p.Name, pattern)).ToListAsync();
        }
    }
}
