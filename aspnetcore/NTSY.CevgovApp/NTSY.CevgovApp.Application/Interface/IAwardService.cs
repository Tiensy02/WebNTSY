using NTSY.CevgovApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Application
{
    public interface IAwardService : IBaseService<AwardDto, AwardCreateDto, AwardUpdateDto,AwardDto>
    {

    }
}
