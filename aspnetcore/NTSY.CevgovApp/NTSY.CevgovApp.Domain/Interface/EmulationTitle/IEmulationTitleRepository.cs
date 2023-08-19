namespace NTSY.CevgovApp.Domain
{
    public interface IEmulationTitleRepository : IBaseRepository<EmulationTitleModel>
    {

        /// <summary>
        /// trả về danh sách danh hiệu thi đua cho người dùng, có phân trang
        /// </summary>
        /// <param name="page"> số trang hiện tại </param>
        /// <param name="pageSize"> số lượng bản ghi trên 1 trang </param>
        /// <returns> danh sách danh hiệu thi đua</returns>
        /// create by: ntsy 18-07-2023

        Task<(IEnumerable<EmulationTitleModel>, int)> GetEmulationTitleInfoAsync(int page, int pageSize);

        /// <summary>
        /// lấy danh hiệu thi đua theo mã danh hiệu thi đua
        /// </summary>
        /// <param name="code">mã danh hiệu thi đua</param>
        /// <returns></returns>
        Task<EmulationTitle> GetEmulationTitleByCode(String code);


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
        /// <returns>danh sách danh hiệu thi đua hợp lệ có phân trang và tổng số bản ghi tìm kiếm được</returns>
        Task<(IEnumerable<EmulationTitleModel>, int)> Filter(int page, int pageSize,
            string? textSearch, EmulationAwardRecipient? emulationAwardRecipient,
            EmulationMovementType? emulationMovementType,
            EmulationStatus? emulationStatus, Guid? awardID);
        /// <summary>
        /// cập nhập trạng thái cảu nhiều danh hiệu thi đua
        /// </summary>
        /// <param name="ids"> danh sách id cảu danh hiệu cần được thay đổi</param>
        /// <param name="emulationStatus">trạng thái muốn thay đổi</param>
        /// <returns></returns>
        Task UpdateManyEmulationTitleStatus(List<Guid> ids , EmulationStatus emulationStatus);
        Task<IEnumerable<EmulationTitle>> GetEmulationTitleSystem(List<Guid> ids);
    }
}
