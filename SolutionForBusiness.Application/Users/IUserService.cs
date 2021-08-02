using SolutionForBusiness.Application.Common;
using SolutionForBusiness.ViewModels.Common;
using SolutionForBusiness.ViewModels.Roles;
using SolutionForBusiness.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolutionForBusiness.Application.Users
{
    public interface IUserService
    {
        Task<ApiResult<UserVM>> Authenticate(LoginRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<PagedResult<UserVM>> GetUserPaging(GetUserPagingRequest request);

        Task<ApiResult<bool>> Update(UpdateRequest request);

        Task<ApiResult<bool>> Delete(Guid Id);

        Task<ApiResult<bool>> RoleAssign(Guid Id, RoleAssignRequest request);

        Task<ApiResult<bool>> getRoleUser(Guid userId);
    }
}