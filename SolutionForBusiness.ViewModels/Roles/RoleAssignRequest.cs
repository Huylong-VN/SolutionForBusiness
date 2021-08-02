using SolutionForBusiness.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionForBusiness.ViewModels.Roles
{
    public class RoleAssignRequest
    {
        public Guid Id { get; set; }

        public List<SelectedItem> Roles { get; set; }
    }
}