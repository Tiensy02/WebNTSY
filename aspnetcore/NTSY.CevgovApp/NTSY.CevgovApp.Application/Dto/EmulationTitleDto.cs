using NTSY.CevgovApp.Domain; 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Application
{
    public class EmulationTitleDto
    {
        //ID của danh hiệu thi đua
        [Required]
        public Guid EmulationID { get; set; }

        //mã của danh hiệu thi đua
        [Required]
        public String EmulationCode { get; set; }

        // đối tượng khen thưởng; 1-cá nhân; 2- tập thể; 0 - cá nhân và tập thể  3- gia đình
        [Required]

        public EmulationAwardRecipient EmulationAwardRecipient { get; set; }

        // loại phong trào áp dụng; 1- thường xuyên; 2- theo đợt; 0- thường xuyên, theo đợt
        [Required]

        public EmulationMovementType EmulationMovementType { get; set; }
        //trạng thái sử dụng của danh hiệu thi đua; 1-sử dụng; 0-ngừng sử dụng
        [Required]

        public EmulationStatus EmulationStatus { get; set; }
        // tên của danh hiệu thi đua
        [Required]

        public String EmulationName { get; set; }

        // tên của cấp khen thưởng
        public String AwardName { get; set; }

        // id của cấp khen khen thưởng
        public Guid AwardID { get; set; }
        // ghi chú

        public string EmulationNote { get; set; }
         
        // cấp độ của danh hiệu thi đua
        public EmulationTitleLever EmulationTitleLever { get; set; }


    }
}
