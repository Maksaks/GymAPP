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
    internal class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDb;

        public UserRepository(AppDbContext appDb)
        {
            this.appDb = appDb;
        }

        public async Task<bool> Create(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByName(string Name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> Select()
        {
            return await appDb.User.ToListAsync();
        }

        public async Task<User> Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
