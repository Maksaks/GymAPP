﻿using Course_project_GYMAPP.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.DAL.Interfaces
{
    public interface IGymUserRepository : IBaseRepository<InGymUser>
    {
        public Task<InGymUser> GetByName(string Name);
        public Task<int> GetCountOfUsersInGym();
    }
}
