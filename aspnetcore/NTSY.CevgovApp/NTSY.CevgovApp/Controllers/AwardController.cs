using Microsoft.AspNetCore.Mvc;
using NTSY.CevgovApp.Application;

namespace NTSY.CevgovApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AwardController : BaseControllers<AwardDto, AwardCreateDto, AwardUpdateDto, AwardDto>
    {
        public AwardController(IAwardService awardService) : base(awardService)
        {
        }
    }
}
