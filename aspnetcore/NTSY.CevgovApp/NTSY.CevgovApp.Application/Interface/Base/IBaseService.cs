using NTSY.CevgovApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Application
{
    public interface IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto,IEntityReadOnlyDto> : IReadOnlyService<IEntityReadOnlyDto>
    {
        /// <summary>
        /// thêm bản ghi
        /// </summary>
        /// <param name="TEntityCreateDto"> bản ghi cần được thêm</param>
        /// <returns></returns>
        /// Create by: NTSY: 15/07/2023

        Task InsertAsync(TEntityCreateDto entityCreateDto);
        /// <summary>
        /// sửa bản ghi
        /// </summary>
        /// <param name="TEntityUpdateDto"> thông tin của bản ghi đã được sửa</param>
        /// <returns></returns>
        Task UpdateAsync(TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// xóa bản ghi theo id
        /// </summary>
        /// <param name="id"> id của bản ghi</param>
        /// Create by: NTSY: 15/07/2023
        /// <returns></returns>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">danh sách id của bản ghi muốn xóa</param>
        /// Create by: NTSY: 15/07/2023
        /// <returns></returns>
        Task DeleteMultiAsync(List<Guid> ids);
    }
}
