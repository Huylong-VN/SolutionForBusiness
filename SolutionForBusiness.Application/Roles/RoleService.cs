using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SolutionForBusiness.Data.Entities;
using SolutionForBusiness.ViewModels.Common;
using SolutionForBusiness.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionForBusiness.Application.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<RoleVM>> getAll()
        {
            var role = await _roleManager.Roles.Select(x => new RoleVM()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).ToListAsync();
            return role;
        }
    }
}