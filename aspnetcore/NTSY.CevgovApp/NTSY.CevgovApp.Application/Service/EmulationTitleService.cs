using AutoMapper;
using NTSY.CevgovApp.Application.Dto;
using NTSY.CevgovApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Application
{
    
    public class EmulationTitleService : 
        BaseService<EmulationTitleModel, EmulationTitleCreateDto,EmulationTitleUpdateDto,EmulationTitleDto,EmulationTitleReadOnlyDto> 
        ,IEmultionTitleService
    {
        private readonly IEmulationTitleRepository _emulationTitleRepo;
        private readonly IEmulationTitleManager _emulationTitleManager;
        
        public EmulationTitleService(IEmulationTitleRepository emulationTitleRepository,
            IMapper mapper, IEmulationTitleManager emulationTitleManager) 
            : base(emulationTitleRepository, mapper)
        {
            _emulationTitleRepo = emulationTitleRepository;
            _emulationTitleManager = emulationTitleManager;
    }


        #region Methods
       

        /// <summary>
        /// Lấy danh sách danh hiệu thi đua cho người dùng, có phân trang.
        /// </summary>
        /// <param name="page">Số trang hiện tại.</param>
        /// <param name="pageSize">Số lượng bản ghi trên 1 trang.</param>
        /// <returns>Danh sách danh hiệu thi đua.</returns>
        public async Task<(IEnumerable<EmulationTitleDto>,int)> GetInfoAsync(int page, int pageSize)
        {
            var (emulationTitles,totalRecord) =
                await _emulationTitleRepo.GetEmulationTitleInfoAsync(page, pageSize);
            var result = _mapper.Map<IEnumerable<EmulationTitleDto>>(emulationTitles);

            return (result,totalRecord);
        }
        /// <summary>
        /// tìm danh hiệu thi đua theo mã danh hiệu thi đua 
        /// </summary>
        /// <param name="code"> mã danh hiệu thi đua</param>
        /// <returns></returns>
        public async Task<EmulationTitleDto> GetEmulationTitleByCode(string code)
        {
            var emulationTitle = await _emulationTitleRepo.GetEmulationTitleByCode(code);
            var result = _mapper.Map<EmulationTitleDto>(emulationTitle);
            return result;
        }
        /// <summary>
        /// xóa nhiều danh hiệu thi đua có kiểm tra xem có phải là danh hiệu thi đua hệ thống hay không
        /// </summary>
        /// <param name="ids"> danh sách id</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override async Task DeleteMultiAsync(List<Guid> ids)
        {

            if (ids.Count == 0)
            {
                throw new Exception("không được truyền danh sách rỗng");
            }
            await _emulationTitleManager.CheckEmulationTitleSystemExist(ids);
            await _baseRepository.DeleteMultiAsync(ids);

        }
        /// <summary>
        /// xóa danh hiệu thi đua có kiểm tra xem có phải danh hiệu hệ thống hay không
        /// </summary>
        /// <param name="id"> id của danh hiệu thi đua</param>
        /// <returns></returns>
        public override async Task DeleteAsync(Guid id)
        { 
            List<Guid> ids = new List<Guid>();
            ids.Add(id);
            await _emulationTitleManager.CheckEmulationTitleSystemExist(ids);
            await _baseRepository.DeleteAsync(id);

        }


        /// <summary>
        /// Tìm kiếm danh hiệu thi đua theo dữ liệu người dùng nhập và lọc bằng cấp khen thưởng, 
        /// đối tượng khen thưởng,
        /// loại phong trào, trạng thái sử dụng, kết quả lọc được có phân trang.
        /// </summary>
        /// <param name="page">Trang hiện tại.</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang.</param>
        /// <param name="textSearch">Input người dùng.</param>
        /// <param name="emulationAwardRecipient">Đối tượng khen thưởng.</param>
        /// <param name="emulationMovementType">Loại phong trào.</param>
        /// <param name="emulationStatus">Trạng thái sử dụng.</param>
        /// <param name="awardID">Id của cấp khen thưởng.</param>
        /// <returns>Danh sách danh hiệu thi đua hợp lệ.</returns>
        public async Task<(IEnumerable<EmulationTitleDto>, int)> Filter(int page, int pageSize,
            string? textSearch, EmulationAwardRecipient? emulationAwardRecipient,
            EmulationMovementType? emulationMovementType,
            EmulationStatus? emulationStatus, Guid? awardID)
        {
            var (emulationTitles,totalRecordFiler) = await _emulationTitleRepo.Filter(
                page, pageSize, textSearch,
                emulationAwardRecipient, emulationMovementType, emulationStatus, awardID);
            var result = _mapper.Map<IEnumerable<EmulationTitleDto>>(emulationTitles);
            return (result, totalRecordFiler);
        }
        /// <summary>
        /// thực hiện chuyển kiểu EmulationTitleCreateDto sang EmulationTitle và validate 
        /// check trùng mã danh hiệu thi đua nếu trùng thì bắn ra 1 exception
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns>danh hiệu thi đua</returns>
        public override async Task<EmulationTitleModel> MapCreateDtoToEntity(EmulationTitleCreateDto entityCreateDto)
        {
           await _emulationTitleManager.CheckEmulationTitleExistByCode(entityCreateDto.EmulationCode);
            var result = _mapper.Map<EmulationTitleModel>(entityCreateDto);
            return result;
        }

        public override async Task<EmulationTitleModel> MapUpdateDtoToEntity(EmulationTitleUpdateDto entityUpdateDto)
        {
            await _emulationTitleManager.CheckEmulationTitleExistByCode(entityUpdateDto.EmulationCode, 
                entityUpdateDto.EmulationID);
            var result = _mapper.Map<EmulationTitleModel>(entityUpdateDto);  
            return result;
        }
        /// <summary>
        /// thực hiện chỉnh sửa trạng thái hoạt động của nhiều danh hiệu thi đua
        /// </summary>
        /// <param name="emulationTitleUpdateMultiDto"> 1 đối tượng gồm danh sách id và trạng thài muốn cập nhập</param>
        /// <returns></returns>
        public async Task UpdateManyEmulationTitleStatus(EmulationTitleUpdateMultiDto emulationTitleUpdateMultiDto)
        {
           
            await _emulationTitleRepo.UpdateManyEmulationTitleStatus(emulationTitleUpdateMultiDto.emulationIDs, 
                emulationTitleUpdateMultiDto.emulationStatus);
        }




        #endregion
    }
}
