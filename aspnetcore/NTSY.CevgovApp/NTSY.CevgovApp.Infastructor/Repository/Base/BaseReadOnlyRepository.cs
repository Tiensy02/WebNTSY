using Dapper;
using NTSY.CevgovApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Infastructure
{
    public abstract class BaseReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
    { 
        
        protected readonly IUnitOfWork _uow;
        // tên của bảng mặc định trùng với tên của TEntity nếu không thì overide ở lớp triển khai
        public virtual string TableName { get; protected set; } = typeof(TEntity).Name;
        // tên của id bảng mặc định trùng với tên của TEntity + ID nếu không thì overide ở lớp triển khai

        public virtual string TableID { get; protected set; } = typeof(TEntity).Name + "ID";

        protected BaseReadOnlyRepository(IUnitOfWork uow)
        {
            this._uow = uow;    
        }
        /// <summary>
        /// lấy tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        public  virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {TableName}; "; 
            var result = await _uow.Connection.QueryAsync<TEntity>(sql,transaction: _uow.Transaction);
            return result;
        }
        /// <summary>
        /// lấy bản ghi theo id
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"> nếu không thì trả về exception</exception>
        public async Task<TEntity> GetAsync(Guid id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE {TableID} = @id;";
            var param = new DynamicParameters();
            param.Add("id", id);
            var result = await 
                _uow.Connection.QueryFirstOrDefaultAsync<TEntity>(sql,param,transaction: _uow.Transaction);
            if(result == null )
            {
                throw new NotFoundException("khong tim thay theo id");
            }
           return result;
;        }
        /// <summary>
        /// lấy nhiều đối tượng dựa vào danh sách id
        /// </summary>
        /// <param name="ids"></param> 
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetListByIdAsync(List<Guid> ids)
        {
            var sql = $"SELECT * FROM {TableName} WHERE {TableID} IN @ids";
            var param = new DynamicParameters();
            param.Add("ids", ids);
            var result = await _uow.Connection.QueryAsync<TEntity>(sql, param,
                transaction: _uow.Transaction);
            return result;
        }
    }
}
