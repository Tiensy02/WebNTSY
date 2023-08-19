using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Domain
{
    /// <summary>
    ///   enum của trạng thái sử dụng danh hiệu thi đua; 1-sử dụng; 0-ngừng sử dụng
    /// </summary>
    public enum EmulationStatus
    {
        Active = 1, // sử dụng
        NoneActive= 0 // Ngừng sử dụng

    }
}
