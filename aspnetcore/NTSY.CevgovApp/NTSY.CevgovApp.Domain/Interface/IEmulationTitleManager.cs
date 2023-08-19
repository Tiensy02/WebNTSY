using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Domain
{
    public interface IEmulationTitleManager
    {
        /// <summary>
        /// check trùng mã trong bản ghi
        /// </summary>
        /// <param name="code">mã của bản ghi</param>
        /// <returns></returns>
        Task CheckEmulationTitleExistByCode(string code, Guid? id = null );
        /// <summary>
        /// kiểm tra danh hiệu thi đua trong danh sách id có danh hiệu cấp hệ thống hay không
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task CheckEmulationTitleSystemExist(List<Guid> ids);
    }
}
