using NTSY.CevgovApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Application
{
    /// <summary>
    /// thông tin để chỉnh sửa  trạng thái danh hiệu thi đua
    /// </summary>
    public class EmulationTitleUpdateMultiDto
    {

        // danh sách id của danh hiệu thi đua cần chỉnh sửa
        public List<Guid> emulationIDs { set; get; }  
        // trạng thái danh hiệu thi đua
        public EmulationStatus emulationStatus { set; get; }
    }
}
