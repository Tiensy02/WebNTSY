using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Domain
{
    public interface IReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// lấy tất cả
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// lấy theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Guid id);
        /// <summary>
        /// lấy nhiều theo danh sách id
        /// </summary>
        /// <param name="ids"> danh sách id</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetListByIdAsync(List<Guid> ids);

    }
}
