using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Domain
{
 
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        
        DbConnection Connection { get; }

        
        DbTransaction? Transaction { get; }

        /// <summary>
        /// Bắt đầu một Transaction mới.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Bắt đầu một Transaction mới với bất đồng bộ.
        /// </summary>
        Task BeginTransactionAsync();

        /// <summary>
        /// Xác nhận và lưu các thay đổi vào cơ sở dữ liệu.
        /// </summary>
        void Commit();

        /// <summary>
        /// Xác nhận và lưu các thay đổi vào cơ sở dữ liệu với bất đồng bộ.
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Hủy bỏ Transaction hiện tại và không lưu các thay đổi.
        /// </summary>
        void RollBack();

        /// <summary>
        /// Hủy bỏ Transaction hiện tại và không lưu các thay đổi với bất đồng bộ.
        /// </summary>
        Task RollBackAsync();
    }
}
