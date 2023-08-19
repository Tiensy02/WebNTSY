using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Application
{
    public class AwardDto
    {
        // id của cấp khen khen thưởng
        public Guid AwardID { get; set; }
        // tên của cấp khen thưởng
        public String AwardName { get; set; }
    }
}
