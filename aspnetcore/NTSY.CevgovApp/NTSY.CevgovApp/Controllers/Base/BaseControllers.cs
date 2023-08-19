using Microsoft.AspNetCore.Mvc;
using NTSY.CevgovApp.Application;
using NTSY.CevgovApp.Domain;

namespace NTSY.CevgovApp
{
    public class BaseControllers<TEntityDto, TEntityCreateDto, TEnityUpdateDto,TEntityReadOnlyDto> :
        BaseReadOnlyController<TEntityReadOnlyDto>
    { 
        private readonly IBaseService<TEntityDto, TEntityCreateDto, TEnityUpdateDto, TEntityReadOnlyDto> _baseService;
        public BaseControllers(IBaseService<TEntityDto, TEntityCreateDto, TEnityUpdateDto, TEntityReadOnlyDto> baseService) 
            : base(baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// hàm thêm mới 1 ban ghi
        /// </summary>
        /// <param name="emulationTitle"> ban ghi</param>
        /// <returns> </returns>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] TEntityCreateDto entityCreateDto)
        {

            await _baseService.InsertAsync(entityCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        #region UpdateAPI
        /// <summary>
        /// hàm chỉnh sửa ban ghi
        /// </summary>
        /// <param name="emulationTitle">ban ghi</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TEnityUpdateDto enityUpdateDto)
        {
            await _baseService.UpdateAsync(enityUpdateDto);
            return StatusCode(StatusCodes.Status200OK);
        }
        #endregion
        #region delete
        /// <summary>
        /// xóa bản danh sách thi đua theo id
        /// </summary>
        /// <param name="id"> id của ban ghi</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _baseService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK);

        }
        /// <summary>
        /// xóa nhiều ban ghi
        /// </summary>
        /// <param name="ids">danh sách id của ban ghi muốn xóa</param>
        /// Create by: NTSY: 15/07/2023
        /// <returns></returns>
        [HttpDelete("Many")] 
        public async Task<IActionResult> DeleteMultiAsync([FromBody] List<Guid> ids)
        {
            await _baseService.DeleteMultiAsync(ids);
            return StatusCode(StatusCodes.Status200OK);

        }
        #endregion
    }
}
