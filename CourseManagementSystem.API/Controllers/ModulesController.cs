using System.Security.Claims;
using CourseManagementSystem.API.DTOs;
using CourseManagementSystem.API.DTOs.Course;
using CourseManagementSystem.API.ServiceContracts;
using CourseManagementSystem.Core.Constants;
using CourseManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Roles.Admin},{Roles.Administrator}")]
        [HttpPut("{moduleId}")]
        public async Task<IActionResult> UpdateModule(Guid moduleId, ModuleUpdateRequest moduleUpdateRequest)
        {
            Guid userId = GetCurrentUserId();

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

           ModuleResponse foundModule =await  _moduleService.GetModuleById(moduleId);
            if (foundModule == null)
            {
                return NotFound("No module found for the specified id.");
            }
            //only administrator or creator of the module can modify it
            if ((User.Identity.IsAuthenticated && User.IsInRole("Administrator")) || userId == foundModule.CreatedBy)
            {

                ModuleResponse? module = await _moduleService.UpdateModule(moduleUpdateRequest);

                if (module == null)
                {
                    return NotFound("No module was found.");
                }

                return Ok(module);
            }

            return Unauthorized("You can't modify the resource unless you're administrator or resource owner.");

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Roles.Admin},{Roles.Administrator}")]
        [HttpDelete("{moduleId}")]
        public async Task<IActionResult> DeleteModule(Guid moduleId)
        {
            if (moduleId == Guid.Empty)
            {
                return BadRequest("Module id is null");
            }
            ModuleResponse? moduleToDelete = await _moduleService.GetModuleById(moduleId);
            if (moduleToDelete == null)
            {
                return NotFound("Module to delete not found.");
            }
            Guid userId = GetCurrentUserId();
            if ((User.Identity.IsAuthenticated && User.IsInRole("Administrator")) || userId == moduleToDelete.CreatedBy)

            {
                bool isDeletionSuccess = await _moduleService.DeleteModule(moduleId);

                if (!isDeletionSuccess)
                {
                    return StatusCode(500, "Error while deleting module.");
                }

                return Ok(isDeletionSuccess);
            }

            return Unauthorized("You can't modify the resource unless you're administrator or resource owner.");

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Roles.Admin},{Roles.Administrator}")]
        [HttpPost("{courseId}")]
        public async Task<IActionResult> AddModule(Guid courseId, ModuleAddRequest moduleAddRequest)
        {
            Guid userId = GetCurrentUserId();

            if (moduleAddRequest == null)
            {
                return BadRequest("Module add request cannot be null.");
            }


            moduleAddRequest.CreatedBy = userId; // assigning current working user as creator
            moduleAddRequest.CourseId = courseId; //getting courseId from route

            ModuleResponse? module = await _moduleService.AddModule(moduleAddRequest);

            if (module == null)
            {
                return StatusCode(500, "Error while adding module.");
            }

            return Ok(module);
        }

        #region privateMethods
        private Guid GetCurrentUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                throw new Exception("Couldn't identify current user.");
            return Guid.Parse(userId);
        }
        #endregion
    }


}
