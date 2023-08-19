using AutoMapper;
using NTSY.CevgovApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Application
{
    public class AwardProfile : Profile
    {
        public AwardProfile() {
            // Ánh xạ từ Award sang AwardDto
            CreateMap<Award, AwardDto>();

            // Ánh xạ từ AwardCreateDto sang Award
            CreateMap<AwardCreateDto, Award>();

            // Ánh xạ từ AwardUpdateDto sang Award
            CreateMap<AwardUpdateDto, Award>();



        }
    }
}
