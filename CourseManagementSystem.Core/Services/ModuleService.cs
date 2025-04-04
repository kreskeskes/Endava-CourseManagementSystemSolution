using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.API.DTOs;
using CourseManagementSystem.API.ServiceContracts;

namespace CourseManagementSystem.Core.Services
{
    internal class ModuleService : IModuleService
    {
        public Task<ModuleResponse> AddModule(ModuleAddRequest moduleAddRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteModule(Guid moduleId)
        {
            throw new NotImplementedException();
        }

        public Task<ModuleResponse> GetModuleById(Guid moduleId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ModuleResponse>> GetModules()
        {
            throw new NotImplementedException();
        }

        public Task<ModuleResponse> UpdateModule(ModuleUpdateRequest moduleUpdateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
