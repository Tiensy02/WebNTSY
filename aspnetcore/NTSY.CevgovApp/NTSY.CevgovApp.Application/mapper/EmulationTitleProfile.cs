using AutoMapper;
using NTSY.CevgovApp.Application.Dto;
using NTSY.CevgovApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Application
{
    public class EmulationTitleProfile : Profile
    {
        public EmulationTitleProfile() {

            // Ánh xạ từ EmulationTitle sang EmulationTitleDto
            CreateMap<EmulationTitle, EmulationTitleDto>();

            // Ánh xạ từ EmulationTitleCreateDto sang EmulationTitle
            CreateMap<EmulationTitleCreateDto, EmulationTitle>();
            // Ánh xạ từ EmulationTitleCreateDto sang EmulationTitleModel

            CreateMap<EmulationTitleCreateDto,EmulationTitleModel>();

            // Ánh xạ từ EmulationTitleUpdateDto sang EmulationTitleModel

            CreateMap<EmulationTitleUpdateDto, EmulationTitleModel>();

            // Ánh xạ từ EmulationTitleUpdateDto sang EmulationTitle
            CreateMap<EmulationTitleUpdateDto, EmulationTitle>();

            // Ánh xạ từ EmulationTitleModel sang EmulationTitleDto
            CreateMap<EmulationTitleModel, EmulationTitleDto>();
            // Ánh xạ từ EmulationTitleModel sang EmulationTitleReadOnlyDto
            CreateMap<EmulationTitleModel, EmulationTitleReadOnlyDto>();
        }
    }
}
