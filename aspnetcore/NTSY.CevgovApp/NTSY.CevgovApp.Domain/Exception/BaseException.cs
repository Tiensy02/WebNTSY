using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Domain
{
    /// <summary>
    /// Lớp đại diện cho đối tượng lỗi cơ bản.
    /// </summary>
    public class BaseException
    {
        /// <summary>
        /// Mã lỗi.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Thông báo lỗi cho lập trình viên.
        /// </summary>
        public string? DevMess { get; set; }

        /// <summary>
        /// Thông báo lỗi cho người dùng.
        /// </summary>
        public string? UserMess { get; set; }

        /// <summary>
        /// ID theo dõi của lỗi.
        /// </summary>
        public string? TraceID { get; set; }

        /// <summary>
        /// Thông tin bổ sung về lỗi.
        /// </summary>
        public string? MoreInfo { get; set; }

        /// <summary>
        /// Dữ liệu liên quan đến lỗi.
        /// </summary>
        public object? Error { get; set; }
        /// <summary>
        /// danh sách tên danh hiệu bị lỗi
        /// </summary>
        public List<String>? EmulationTitleNames { get; set; }

        /// <summary>
        /// Chuyển đối tượng lỗi thành chuỗi JSON.
        /// </summary>
        /// <returns>Chuỗi JSON chứa thông tin lỗi.</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this); 
        }
    }
}
