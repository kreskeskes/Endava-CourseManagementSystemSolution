using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CourseManagementSystem.Core.ServiceContracts
{
    public interface IJwtService
    {
        Task<string> GenerateJwtToken(IdentityUser<Guid> user);
    }
}
