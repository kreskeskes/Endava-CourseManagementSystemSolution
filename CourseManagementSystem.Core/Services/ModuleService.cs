using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CourseManagementSystem.API.DTOs;
using CourseManagementSystem.API.ServiceContracts;
using CourseManagementSystem.Core.Entities;
using CourseManagementSystem.Core.RepositoryContracts;

namespace CourseManagementSystem.Core.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IModulesRepository _modulesRepository;
        private readonly IMapper _mapper;

        public ModuleService(IModulesRepository modulesRepository, IMapper mapper)
        {
            _modulesRepository = modulesRepository;
            _mapper = mapper;
        }
        public async Task<ModuleResponse?> AddModule(ModuleAddRequest moduleAddRequest)
        {
            Module module = _mapper.Map<Module>(moduleAddRequest);

            module.CreatedAt = DateTime.UtcNow;
            module.UpdatedAt = DateTime.UtcNow;

            Module? addedModule = await _modulesRepository.AddModule(module);

            if (addedModule != null)
            {
                return _mapper.Map<ModuleResponse>(addedModule);
            }
            return null;
        }

        public async Task<bool> DeleteModule(Guid moduleId)
        {
            return await _modulesRepository.DeleteModule(moduleId);
        }

        public async Task<ModuleResponse?> GetModuleById(Guid moduleId)
        {
            return _mapper.Map<ModuleResponse>(await _modulesRepository.GetModuleById(moduleId));
        }

        public async Task<List<ModuleResponse>> GetModules()
        {
            return _mapper.Map<List<ModuleResponse>>(await _modulesRepository.GetModules());
        }

        public async Task<ModuleResponse> UpdateModule(ModuleUpdateRequest moduleUpdateRequest)
        {
            Module module = _mapper.Map<Module>(moduleUpdateRequest);
            module.UpdatedAt = DateTime.UtcNow;
            return _mapper.Map<ModuleResponse>(await _modulesRepository.UpdateModule(module));
        }
    }
}
