using CourseManagementSystem.API.DTOs;
using CourseManagementSystem.API.ServiceContracts;
using CourseManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService _moduleService;
        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }



        [HttpGet]
        public async Task<IActionResult> GetModules()
        {
            List<ModuleResponse> modules = await _moduleService.GetModules();

            if (!modules.Any())
            {
                return NotFound("No modules were found.");
            }

            return Ok(modules);
        }

        [HttpGet("{moduleId}")]
        public async Task<IActionResult> GetModuleById(Guid moduleId)
        {
            ModuleResponse? module = await _moduleService.GetModuleById(moduleId);

            if (module == null)
            {
                return NotFound("No module was found.");
            }

            return Ok(module);
        }

        [HttpPut("{moduleId}")]
        public async Task<IActionResult> UpdateModule(Guid moduleId, ModuleUpdateRequest moduleUpdateRequest)
        {
            if (moduleId == Guid.Empty)
            {
                return BadRequest("Module id is null");
            }

            if (moduleUpdateRequest == null)
            {
                return BadRequest("Module update request cannot be null.");
            }
            if (moduleUpdateRequest.Id != moduleId)
            {
                return BadRequest("Module update request and module Id  do not match.");

            }
            ModuleResponse? module = await _moduleService.UpdateModule(moduleUpdateRequest);

            if (module == null)
            {
                return NotFound("No module was found.");
            }

            return Ok(module);
        }


        [HttpDelete("{moduleId}")]
        public async Task<IActionResult> UpdateModule(Guid moduleId)
        {
            if (moduleId == Guid.Empty)
            {
                return BadRequest("Module id is null");
            }

            bool isDeletionSuccess = await _moduleService.DeleteModule(moduleId);

            if (!isDeletionSuccess)
            {
                return StatusCode(500, "Error while deleting module.");
            }

            return Ok(isDeletionSuccess);
        }


        [HttpPost]
        public async Task<IActionResult> AddModule(ModuleAddRequest moduleAddRequest)
        {

            if (moduleAddRequest == null)
            {
                return BadRequest("Module add request cannot be null.");
            }

            ModuleResponse? module = await _moduleService.AddModule(moduleAddRequest);

            if (module == null)
            {
                return StatusCode(500, "Error while adding module.");
            }

            return Ok(module);
        }
    }
}
