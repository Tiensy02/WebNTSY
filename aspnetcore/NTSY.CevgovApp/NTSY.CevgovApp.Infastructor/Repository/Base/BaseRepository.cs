using Dapper;
using NTSY.CevgovApp.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Infastructure
{
    public abstract class BaseRepository<TEntity> : BaseReadOnlyRepository<TEntity>, IBaseRepository<TEntity>
    {
        // tên của procedure thêm mới bản ghi
        public virtual string ProcedureInsert { get; protected set; } = $"Proc_{typeof(TEntity).Name}_Insert";

        // tên của procedure update
        public virtual string ProcedureUpdate { get; protected set; } = $"Proc_{typeof(TEntity).Name}_Update";

        //private readonly IEmulationTitleManager  _emulatioinTitleManager;
        public BaseRepository(IUnitOfWork uow) : base(uow)
        {
        }
       

        

        /// <summary>
        /// thêm bản ghi
        /// </summary>
        /// <param name="entity"> đối tượng cần được thêm</param>
        /// <returns></returns>
        /// Create by: NTSY: 15/07/2023
        public async Task InsertAsync(TEntity entity)
        {
            var param = new DynamicParameters();
            var type = typeof(TEntity);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);

                param.Add(propertyName, propertyValue);
            }
             await _uow.Connection.
                ExecuteAsync(ProcedureInsert,param,commandType: CommandType.StoredProcedure);

        }
        /// <summary>
        /// sửa danh bản ghi
        /// </summary>
        /// <param name="entity"> đối tượng cần được sửa</param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            var param = new DynamicParameters();
            var type = typeof(TEntity);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);

                param.Add(propertyName, propertyValue);
            }
            await _uow.Connection.
                ExecuteAsync(ProcedureUpdate, param, commandType: CommandType.StoredProcedure);
        }
        /// <summary>
        /// xóa bản ghi theo id
        /// </summary>
        /// <param name="id"> id của bản ghi</param>
        /// Create by: NTSY: 15/07/2023
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var sql = $"DELETE FROM {TableName} WHERE {TableID} = @id";
            var param = new DynamicParameters();
            param.Add("id", id);
            await _uow.Connection.ExecuteAsync(sql, param,
                transaction: _uow.Transaction);
        }
        /// <summary>
        /// xóa nhiều bản ghi theo danh sách id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task DeleteMultiAsync(List<Guid> ids) 
        {
            var sql = $"DELETE FROM {TableName} WHERE {TableID} IN @ids;";
            var param = new DynamicParameters();
            param.Add("ids", ids);
            await _uow.Connection.ExecuteAsync(sql, param);
            
        } 
    }
}
  