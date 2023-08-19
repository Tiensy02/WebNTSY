using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Domain
{
    public abstract class BaseEntity
    {
        // ngày tạo đối tượng
        public DateTime? CreateDate { get; set; }
        // người tạo
        public String? CreateBy { get; set; }
        // ngày sửa đối tượng lần cuối
        public DateTime? ModifiedDate { get; set; }
        // người sửa đối tượng lần cuối
        public String? ModifiedBy { get; set; }
    }
}
