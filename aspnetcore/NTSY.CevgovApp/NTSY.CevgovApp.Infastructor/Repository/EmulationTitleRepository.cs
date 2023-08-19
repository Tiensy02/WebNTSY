using Dapper;
using NTSY.CevgovApp.Domain;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace NTSY.CevgovApp.Infastructure
{
    public class EmulationTitleRepository : BaseRepository<EmulationTitleModel>, IEmulationTitleRepository
    { 
        public override string TableName { get; protected set; } = "EmulationTitle";
        public override string TableID { get; protected set; } = "EmulationID";

        public override string ProcedureInsert { get; protected set; } =  "Proc_EmulationTitle_Insert";
        public override string ProcedureUpdate { get; protected set; } = "Proc_EmulationTitle_Update";
        #region contructor

        public EmulationTitleRepository(IUnitOfWork uow)
            : base(uow)
        {
        }


        #endregion
        /// <summary>
        /// thực hiện lấy tất cả các các danh hiệu thi đua không theo phân trang
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<EmulationTitleModel>> GetAllAsync()
        {
            var result = await _uow.Connection.QueryAsync<EmulationTitleModel>("Proc_EmulationTitle_GetAll", commandType: CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// lấy ra tất cả danh 
        /// </summary>
        /// <returns></returns>
        public async Task<(IEnumerable<EmulationTitleModel>, int)> GetEmulationTitleInfoAsync(int page, int pageSize)
        {
            var param = new DynamicParameters();
            param.Add("page", page);
            param.Add("pageSize", pageSize);
            param.Add("totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output); // Thêm tham số totalRecord để nhận giá trị trả về

            var result = await _uow.Connection.QueryAsync<EmulationTitleModel>(
                "Proc_GetAllInfo", param, commandType: CommandType.StoredProcedure);

            int totalRecord = param.Get<int>("totalRecord"); // Lấy giá trị totalRecord sau khi thực hiện procedure

            return (result ,totalRecord);
        }

        /// <summary>
        /// lấy danh hiệu thi đua theo mã danh hiệu thi đua
        /// </summary> 
        /// <param name="code">mã danh hiệu thi đua</param>
        /// <returns></returns>
        public async Task<EmulationTitle> GetEmulationTitleByCode(String code)
        {
            var sql = $"SELECT * FROM emulationtitle WHERE EmulationCode = @code;";
            var param = new DynamicParameters();
            param.Add("code", code);
            var result = await _uow.Connection.QueryFirstOrDefaultAsync<EmulationTitle>
                (sql, param);
            return result;
        }
        /// <summary>
        /// hàm lấy ra những danh hiệu thi đua cấp hệ thống trong những danh hiệu thi đua ở danh sách ids
        /// </summary>
        /// <param name="ids"> danh sách id được chọn để lấy ra những danh hiệu thi đua cấp hệ thông trong đó</param>
        /// <returns>danh sách những danh hiệu thi đua cấp hệ thống</returns>
        public async Task<IEnumerable<EmulationTitle>> GetEmulationTitleSystem(List<Guid> ids)
        {
            var sql = $"SELECT * FROM emulationtitle WHERE EmulationID IN @ids AND EmulationTitleLever = 1;"; 
            var param = new DynamicParameters();
            param.Add("ids", ids);
            var result = await _uow.Connection.QueryAsync<EmulationTitle>
                (sql, param);
            return result;
        }

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
        /// <returns>danh sách danh hiệu thi đua hợp lệ</returns>
        public async Task<(IEnumerable<EmulationTitleModel>, int)> Filter(int page, int pageSize,
            string? textSearch, EmulationAwardRecipient? emulationAwardRecipient,
            EmulationMovementType? emulationMovementType, EmulationStatus? emulationStatus, Guid? awardID)
        {
            var param = new DynamicParameters();
            param.Add("page", page);
            param.Add("pageSize", pageSize);
            param.Add("textSearch", textSearch);
            param.Add("emulationAwardRecipient", emulationAwardRecipient);
            param.Add("emulationMovementType", emulationMovementType);
            param.Add("emulationStatus", emulationStatus);
            param.Add("awardID", awardID);
            param.Add("totalRecordFilter", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var result = await _uow.Connection.QueryAsync<EmulationTitleModel>(
                 "Proc_Filter", param, transaction: _uow.Transaction, commandType: CommandType.StoredProcedure);
            int totalRecordFilter = param.Get<int>("totalRecordFilter"); // Lấy giá trị totalRecord sau khi thực hiện procedure

            return (result,totalRecordFilter);
        }
        /// <summary>
        /// chỉnh sửa trạng thái của nhiều danh hiệu thi đua
        /// </summary>
        /// <param name="ids">danh sách id</param>
        /// <param name="emulationStatus">trạng thái danh hiệu thi đua</param>
        /// <returns></returns>
        public async Task UpdateManyEmulationTitleStatus(List<Guid> ids, EmulationStatus emulationStatus ) 
        {
            var sql = $" UPDATE EmulationTitle et SET et.EmulationStatus " +
                $"= @emulationStatus WHERE EmulationID IN @ids";

            var param = new DynamicParameters();
            param.Add("emulationStatus", emulationStatus);
            param.Add("ids", ids);
            await _uow.Connection.ExecuteAsync(sql, param, transaction: _uow.Transaction);

        }
    }
}
