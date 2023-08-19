using AutoMapper;
using NTSY.CevgovApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Application
{
    public abstract class BaseReadOnlyService<TEntity,TEntityReadOnlyDto> : IReadOnlyService<TEntityReadOnlyDto>
    {
        protected readonly IReadOnlyRepository<TEntity> _readOnlyRepository;
        protected readonly IMapper _mapper;

        protected BaseReadOnlyService(IReadOnlyRepository<TEntity> readOnlyRepository,
            IMapper mapper)
        {
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// lấy tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntityReadOnlyDto>> GetAllAsync()
        {
            var entities = await _readOnlyRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<TEntityReadOnlyDto>>(entities);
            return result;
        }
        /// <summary>
        /// lấy bnar ghi theo id
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"> nếu không thì trả về exception</exception>
        public async Task<TEntityReadOnlyDto> GetAsync(Guid id)
        {
            var entity = await _readOnlyRepository.GetAsync(id);
            var result = _mapper.Map<TEntityReadOnlyDto>(entity);
            return result; 
        }
    }
}
