using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionForBusiness.Application.Common
{
    public class PagedResultBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int ToTalRecords { get; set; }

        public int PageCount
        {
            get
            {
                var pageCount = (double)ToTalRecords / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}