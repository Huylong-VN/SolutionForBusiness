using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SolutionForBusiness.Application.Users;
using SolutionForBusiness.ViewModels.Roles;
using SolutionForBusiness.ViewModels.Users;
using System;
using System.Threading.Tasks;

namespace SolutionForBusiness.BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        public readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            var result = await _userService.Authenticate(request);
            if (result.ResultObj == null) return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromHeader] GetUserPagingRequest request)
        {
            var result = await _userService.GetUserPaging(request);
            return Ok(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _userService.Register(request);
            return Ok(result.Message);
        }

        [HttpDelete("")]
        public async Task<IActionResult> Delete(Guid userId, Guid Id)
        {
            var userCheck = await _userService.getRoleUser(userId);
            if (userCheck.IsSuccessed)
            {
                var delete = await _userService.Delete(Id);
                return Ok(delete);
            }
            return BadRequest(userCheck.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid userId, [FromBody] UpdateRequest request)
        {
            var userCheck = await _userService.getRoleUser(userId);
            if (userCheck.IsSuccessed)
            {
                var user = await _userService.Update(request);
                if (user.IsSuccessed) return Ok(user);
            }
            return BadRequest();
        }

        [HttpPost("rolesassign")]
        public async Task<IActionResult> RoleAssign(Guid userId, Guid Id, RoleAssignRequest request)
        {
            var userCheck = await _userService.getRoleUser(userId);
            if (userCheck.IsSuccessed)
            {
                var result = await _userService.RoleAssign(Id, request);
                if (result.IsSuccessed) return Ok(result);
            }
            return BadRequest();
        }
    }
}