using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagementSystem.Core.Entities;
using CourseManagementSystem.Core.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Infrastructure.Repositories
{
    public class ModulesRepository : IModulesRepository
    {
        private readonly ApplicationDbContext _db;

        public ModulesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Module?> AddModule(Module module)
        {
            if (module == null)
                return null;

            if (module.CourseId == Guid.Empty)
                throw new Exception("A module cannot exist without a course");

            module.Id = Guid.NewGuid();
            module.CreatedAt = DateTime.UtcNow;
            module.UpdatedAt = DateTime.UtcNow;
            await _db.Modules.AddAsync(module);

            Course foundCourse = await _db.Courses.FirstOrDefaultAsync(c => c.Id == module.CourseId);
            if (foundCourse == null)
            {
                throw new Exception("No course was found for the specified Id.");
            }

            foundCourse.ModuleIds.Add(module.Id);
            foundCourse.Contributors.Add(module.CreatedBy); // adding contribuitors as people who added a module

            await _db.SaveChangesAsync();
            return module;
        }

        public async Task<bool> DeleteModule(Guid moduleId)
        {
            Module? module = await _db.Modules.FirstOrDefaultAsync(c => c.Id == moduleId);
            if (module == null)
                return false;


            Course courseWithModule = await _db.Courses.FirstOrDefaultAsync(c => c.ModuleIds.Any(mId => mId == moduleId));

            if (courseWithModule != null)
            {
                courseWithModule.ModuleIds.Remove(moduleId);
                courseWithModule.Contributors.Remove(courseWithModule.CreatedBy);
            }

            _db.Modules.Remove(module);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Module?> GetModuleById(Guid moduleId)
        {
            Module? module = await _db.Modules.FirstOrDefaultAsync(m => m.Id == moduleId);
            if (module == null)
                return null;
            return module;
        }

        public async Task<List<Module>> GetModules()
        {
            return await _db.Modules.ToListAsync();
        }

        public async Task<Module?> UpdateModule(Module module)
        {
            Module? foundModule = await _db.Modules.FirstOrDefaultAsync(m => m.Id == module.Id);
            if (foundModule == null)
                return null;

            if (module.CourseId == Guid.Empty)
                throw new Exception("A module cannot exist without a course");

            foundModule.Description = module.Description;
            foundModule.Content = module.Content;
            foundModule.Title = module.Title;

            foundModule.CourseId = module.CourseId;
            foundModule.Order = module.Order;
            foundModule.UpdatedAt = DateTime.UtcNow;


            await _db.SaveChangesAsync();

            return foundModule;
        }
    }
}
