using AutoMapper;
using NTSY.CevgovApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Application
{
    public class AwardService : BaseService<Award, AwardCreateDto, AwardUpdateDto, AwardDto,AwardDto>, IAwardService
    {
        public AwardService(IAwardRespository awardRespository, IMapper mapper) : base(awardRespository, mapper)
        {
        }

        
    }
}
