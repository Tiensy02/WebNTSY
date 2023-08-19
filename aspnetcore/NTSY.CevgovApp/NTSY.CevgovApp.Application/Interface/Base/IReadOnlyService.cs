using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Application
{
    public interface IReadOnlyService<TEntityReadOnlyDto>
    {
        /// <summary>
        /// lấy tất cả
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntityReadOnlyDto>> GetAllAsync();

        /// <summary>
        /// lấy theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntityReadOnlyDto> GetAsync(Guid id);
    }
}
