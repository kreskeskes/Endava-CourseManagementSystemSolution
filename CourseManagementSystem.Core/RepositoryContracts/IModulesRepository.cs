﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.API.DTOs;
using CourseManagementSystem.Core.Entities;

namespace CourseManagementSystem.Core.RepositoryContracts
{
    public interface IModulesRepository
    {
        Task<Module?> AddModule(Module module);
        Task<bool> DeleteModule(Guid moduleId);
        Task<Module?> UpdateModule(Module module);
        Task<List<Module>> GetModules();
        Task<Module?> GetModuleById(Guid moduleId);
    }
}
