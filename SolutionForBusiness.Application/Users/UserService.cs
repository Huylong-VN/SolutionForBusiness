using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SolutionForBusiness.Application.Common;
using SolutionForBusiness.Data.Entities;
using SolutionForBusiness.ViewModels.Common;
using SolutionForBusiness.ViewModels.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SolutionForBusiness.Application.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _usermanage;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _configuration = configuration;
            _usermanage = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<ApiResult<UserVM>> Authenticate(LoginRequest request)
        {
            var user = await _usermanage.FindByNameAsync(request.UserName);
            if (user == null) new ApiErrorResult<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);
            if (!result.Succeeded) return new ApiErrorResult<UserVM>("Đăng Nhập thất bại");
            var roles = await _usermanage.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role,string.Join(";",roles)),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                _configuration["Tokens:Issuer"], claims, expires: DateTime.Now.AddHours(3), signingCredentials: creds);

            return new ApiSuccessResult<UserVM>(new UserVM
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                LastName = user.LastName,
                Dob = user.Dob,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }

        public async Task<ApiResult<bool>> Delete(Guid Id)
        {
            var user = await _usermanage.FindByIdAsync(Id.ToString());
            if (user == null) return new ApiErrorResult<bool>("Người dùng không tồn tại");
            await _usermanage.DeleteAsync(user);
            return new ApiSuccessResult<bool>();
        }

        public async Task<PagedResult<UserVM>> GetProductPaging(GetUserPagingRequest request)
        {
            var query = _usermanage.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword) || x.Email.Contains(request.Keyword) || x.FirstName.Contains(request.Keyword));
            }

            //Total row
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).OrderBy(x => x.FirstName).Take(request.PageSize).Select(x => new UserVM()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Dob = x.Dob,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
            }).ToListAsync();
            var pagedResult = new PagedResult<UserVM>()
            {
                Items = data,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ToTalRecords = totalRow
            };
            return pagedResult;
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            if (await _usermanage.FindByNameAsync(request.UserName) == null) return new ApiErrorResult<bool>("Tên tài khoản đã tồn tại");
            if (await _usermanage.FindByEmailAsync(request.Email) == null) return new ApiErrorResult<bool>("Email đã tồn tại");
            var user = new User()
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dob = request.Dob
            };
            var result = await _usermanage.CreateAsync(user, request.Password);
            if (result.Succeeded) return new ApiSuccessResult<bool>();
            return new ApiErrorResult<bool>("Đăng kí thất bại");
        }

        public async Task<ApiResult<bool>> Update(Guid Id, UpdateRequest request)
        {
            if (await _usermanage.Users.AnyAsync(x => x.Email == request.Email && x.Id != Id)) return new ApiErrorResult<bool>("Email đã tồn tại");
            var user = await _usermanage.FindByIdAsync(Id.ToString());
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Dob = request.Dob;
            user.PhoneNumber = request.Phone;
            var result = await _usermanage.UpdateAsync(user);
            if (result.Succeeded) return new ApiSuccessResult<bool>();
            return new ApiErrorResult<bool>("Cập nhật thất bại");
        }
    }
}