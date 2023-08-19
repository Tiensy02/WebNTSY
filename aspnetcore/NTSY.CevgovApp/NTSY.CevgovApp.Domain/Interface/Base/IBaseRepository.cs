using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Domain
{
    public interface IBaseRepository<TEntity> : IReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// thêm bản ghi
        /// </summary>
        /// <param name="entity"> đối tượng cần được thêm</param>
        /// <returns></returns>
        /// Create by: NTSY: 15/07/2023

        Task InsertAsync(TEntity entity);
        /// <summary>
        /// sửa danh bản ghi
        /// </summary>
        /// <param name="entity"> đối tượng cần được sửa</param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// xóa bản ghi theo id
        /// </summary>
        /// <param name="id"> id của bản ghi</param>
        /// Create by: NTSY: 15/07/2023
        /// <returns></returns>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// xóa nhiều bản ghi theo danh sách id
        /// </summary>
        /// <param name="ids">danh sách id</param>
        /// <returns></returns>

        Task DeleteMultiAsync(List<Guid> ids);
    }
}
