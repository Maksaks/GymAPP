﻿using Course_project_GYMAPP.DAL.Interfaces;
using Course_project_GYMAPP.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.DAL.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext appDb;

        public AdminRepository(AppDbContext appDb)
        {
            this.appDb = appDb;
        }
        public async Task<bool> Create(Admin entity)
        {
            await appDb.Admin.AddAsync(entity);
            await appDb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Admin entity)
        {
            appDb.Admin.Remove(entity);
            await appDb.SaveChangesAsync();
            return true;
        }

        public async Task<Admin> Get(int id)
        {
            return await appDb.Admin.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Admin> GetByName(string Name)
        {
            return await appDb.Admin.FirstOrDefaultAsync(x => x.Name == Name);
        }

        public async Task<List<Admin>> Select()
        {
            return await appDb.Admin.ToListAsync();
        }

        public async Task<Admin> Update(Admin entity)
        {
            appDb.Admin.Update(entity);
            await appDb.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Admin>> Search(string pattern)
        {
            pattern = "%" + pattern + "%";
            return await appDb.Admin.Where(p => EF.Functions.Like(p.Name, pattern)).ToListAsync();
        }
    }
}
