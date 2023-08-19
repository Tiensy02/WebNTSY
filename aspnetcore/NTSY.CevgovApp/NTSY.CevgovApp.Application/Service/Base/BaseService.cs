using AutoMapper;
using NTSY.CevgovApp.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Application
{
    public abstract class BaseService<TEntity, TEntityCreateDto, TEntityUpdateDto, TEntityDto,TEntityReadOnlyDto> :
        BaseReadOnlyService<TEntity, TEntityReadOnlyDto>, IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto,TEntityReadOnlyDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;

        protected BaseService(IBaseRepository<TEntity> baseRepository,
            IMapper mapper) :
            base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
        }

        /// <summary>
        /// thêm bản ghi
        /// </summary>
        /// <param name="TEntityCreateDto"> bản ghi cần được thêm</param>
        /// <returns></returns>
        /// Create by: NTSY: 15/07/2023
        public virtual async Task InsertAsync(TEntityCreateDto entityCreateDto)
        {

            var entity = await MapCreateDtoToEntity(entityCreateDto);

            // Insert vào database
            await _baseRepository.InsertAsync(entity);
        }
        /// <summary>
        /// sửa bản ghi
        /// </summary>
        /// <param name="TEntityUpdateDto"> thông tin của bản ghi đã được sửa</param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TEntityUpdateDto entityUpdateDto)
        {
            var entity = await MapUpdateDtoToEntity(entityUpdateDto);

            // Insert vào database
            await _baseRepository.UpdateAsync(entity);
        }
        /// <summary>
        /// xóa bản ghi theo id
        /// </summary>
        /// <param name="id"> id của bản ghi</param>
        /// Create by: NTSY: 15/07/2023
        /// <returns></returns>
        public virtual async Task DeleteAsync(Guid id)
        {
            await _baseRepository.DeleteAsync(id);

        }
        /// <summary>
        /// xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">danh sách id của bản ghi muốn xóa</param>
        /// Create by: NTSY: 15/07/2023
        /// <returns></returns>
        public virtual async Task DeleteMultiAsync(List<Guid> ids)
        {
            if (ids.Count == 0)
            {
                throw new Exception("không được truyền danh sách rỗng");
            }

            await _baseRepository.DeleteMultiAsync(ids);

        }

        /// <summary>
        /// hàm thực hiện map từ kiểu TEntityCreateDto sang TEntity đồng thời thực hiện validate 
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns></returns>
        public virtual Task<TEntity> MapCreateDtoToEntity(TEntityCreateDto entityCreateDto)
        {
            var result = _mapper.Map<TEntity>(entityCreateDto);
            return Task.FromResult(result);
        }
        public virtual Task<TEntity> MapUpdateDtoToEntity(TEntityUpdateDto entityUpdateDto)
        {
            var result = _mapper.Map<TEntity>(entityUpdateDto);
            return Task.FromResult(result);
        }
    } 
    }
