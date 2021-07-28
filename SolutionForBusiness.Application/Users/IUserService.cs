using SolutionForBusiness.Application.Common;
using SolutionForBusiness.ViewModels.Common;
using SolutionForBusiness.ViewModels.Users;
using System;
using System.Threading.Tasks;

namespace SolutionForBusiness.Application.Users
{
    public interface IUserService
    {
        Task<ApiResult<UserVM>> Authenticate(LoginRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<PagedResult<UserVM>> GetProductPaging(GetUserPagingRequest request);

        Task<ApiResult<bool>> Update(Guid Id, UpdateRequest request);

        Task<ApiResult<bool>> Delete(Guid Id);
    }
}