using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Domain
{
    public class EmulationTitleModel:BaseEntity
    {
        // id của danh hiệu thi đua

        public Guid EmulationID { get; set; }
        //mã của danh hiệu thi đua

        public String EmulationCode { get; set; }
        //id của cấp khen khen thưởng

        public Guid AwardID { get; set; }

        // đối tượng khen thưởng; 1-cá nhân; 2- tập thể; 0 - cá nhân và tập thể 

        public EmulationAwardRecipient EmulationAwardRecipient { get; set; }

        // loại phong trào áp dụng; 1- thường xuyên; 2- theo đợt; 0- thường xuyên, theo đợt

        public EmulationMovementType EmulationMovementType { get; set; }
        //trạng thái sử dụng của danh hiệu thi đua; 1-sử dụng; 0-ngừng sử dụng

        public EmulationStatus EmulationStatus { get; set; }
        // tên của danh hiệu thi đua

        public String EmulationName { get; set; }
        // ghi chú
        public String EmulationNote { get; set; }

        // tên của cấp khen thưởng
        public String AwardName { get; set; } 

        // cấp độ của danh hiệu thi đua
        public EmulationTitleLever EmulationTitleLever { get; set; }
    }
}
