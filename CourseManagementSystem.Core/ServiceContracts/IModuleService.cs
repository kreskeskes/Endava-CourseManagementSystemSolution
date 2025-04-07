using CourseManagementSystem.API.DTOs;

namespace CourseManagementSystem.API.ServiceContracts
{
    public interface IModuleService
    {
        Task<ModuleResponse?> AddModule(ModuleAddRequest moduleAddRequest);
        Task<bool> DeleteModule(Guid moduleId);
        Task<ModuleResponse?> UpdateModule(ModuleUpdateRequest moduleUpdateRequest);
        Task<List<ModuleResponse>> GetModules();
        Task<ModuleResponse?> GetModuleById(Guid moduleId);

    }
}
