using Microsoft.AspNetCore.Mvc;
using NTSY.CevgovApp.Application;
using NTSY.CevgovApp.Domain;

namespace NTSY.CevgovApp
{
    public abstract class BaseReadOnlyController<TEntityReadOnlyDto> : ControllerBase
    {
        private readonly IReadOnlyService<TEntityReadOnlyDto> _readOnlyService;

        protected BaseReadOnlyController(IReadOnlyService<TEntityReadOnlyDto> readOnlyService)
        {
            _readOnlyService = readOnlyService;
        }

        /// <summary>
        /// lấy tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<TEntityReadOnlyDto>> GetAllAsync()
        {
            var result = await _readOnlyService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// lấy bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result  = await _readOnlyService.GetAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
