using Dapper;
using Microsoft.AspNetCore.Mvc;
using NTSY.CevgovApp.Application;
using NTSY.CevgovApp.Application.Dto;
using NTSY.CevgovApp.Domain; 
using MySqlConnector;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
 
namespace NTSY.CevgovApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmulationTitlesController : 
        BaseControllers<EmulationTitleDto, EmulationTitleCreateDto,EmulationTitleUpdateDto,EmulationTitleReadOnlyDto>
    {
        #region field
        private readonly IEmultionTitleService _emulationService;
        #endregion
         
        #region contructor

        public EmulationTitlesController(IEmultionTitleService emulationService) :base(emulationService)
        {
            _emulationService = emulationService;
        }
        #endregion
        #region getInfoAPI
        /// <summary>
        /// hàm lấy bản ghi theo phân trang và trả về 1 đối tượng gồm danh sách bản ghi theo phân trang và
        /// tổng số bản ghi trong database
        /// </summary>
        /// <param name="page"></param> trang hiện tại
        /// <param name="pageSize"></param> số lượng bản ghi trên 1 trang
        /// <returns></returns>
        [HttpGet("Info")]
        public async Task<EmulationTitleInfoDto> GetInfoAsync(int page, int pageSize)
        {
            var (result,totalRecord) = 
                await _emulationService.GetInfoAsync(page,pageSize);
            return new EmulationTitleInfoDto
            {
                EmulationTitles = result,
                TotalRecord = totalRecord
            };
        }
        #endregion

        #region filterApi
        /// <summary>
        /// tìm kiếm danh hiệu thi đua theo dữ liệu người dùng nhập và lọc bằng cấp khen thưởng, đối tượng
        /// khen thưởng, loại phong trào, trạng thái sử dụng, kết quả lọc được có phân trang
        /// </summary>
        /// <param name="page"> trang hiện tại </param>
        /// <param name="pageSize"> số bản ghi trên 1 trang</param>
        /// <param name="textSearch">input người dùng</param>
        /// <param name="emulationAwardRecipient">đối tượng khen thưởng</param>
        /// <param name="emulationMovementType">loại phong trào</param>
        /// <param name="emulationStatus">trạng thái sử dụng</param>
        /// <param name="awardID">id của cấp khen thưởng</param>
        /// <returns>danh sách danh hiệu thi đua hợp lệ</returns>
        [HttpGet("Filter")] 
        public async Task<EmulationTitleInfoDto> Filter(int page, int pageSize, string? textSearch,
            EmulationAwardRecipient? emulationAwardRecipient, EmulationMovementType? emulationMovementType,
            EmulationStatus? emulationStatus, Guid? awardID)
        {
            var (result,TotalRecordFilter) = await _emulationService.Filter(page, pageSize, 
                textSearch, emulationAwardRecipient,emulationMovementType, emulationStatus, awardID);
            return new EmulationTitleInfoDto
            {
                EmulationTitles = result,
                TotalRecord = TotalRecordFilter
            };
        }
        #endregion
        [HttpPut("ManyUpdate")]
        public async Task<IActionResult> UpdateManyEmulationTitleStatus(EmulationTitleUpdateMultiDto 
            emulationTitleUpdateMultiDto)
        {
            await _emulationService.UpdateManyEmulationTitleStatus(emulationTitleUpdateMultiDto);

            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpDelete("{id}")]
        public override async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _emulationService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
} 
  