﻿using Course_project_GYMAPP.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_GYMAPP.DAL.Interfaces
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        public Task<Admin> GetByName(string Name);
    }
}
