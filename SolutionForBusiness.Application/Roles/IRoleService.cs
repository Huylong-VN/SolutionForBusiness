using SolutionForBusiness.ViewModels.Roles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolutionForBusiness.Application.Roles
{
    public interface IRoleService
    {
        Task<List<RoleVM>> getAll();
    }
}