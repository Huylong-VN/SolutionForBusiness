using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolutionForBusiness.Application.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionForBusiness.BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleService.getAll();
            return Ok(result);
        }
    }
}