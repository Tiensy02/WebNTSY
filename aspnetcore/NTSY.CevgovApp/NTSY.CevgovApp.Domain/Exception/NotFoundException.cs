using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace NTSY.CevgovApp.Domain
{
    /// <summary>
    /// Lớp đại diện cho ngoại lệ thông báo không tìm thấy.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Mã lỗi của ngoại lệ.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Hàm khởi tạo ngoại lệ không tìm thấy với mã lỗi đã cho.
        /// </summary>
        /// <param name="errorCode">Mã lỗi.</param>
        public NotFoundException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Hàm khởi tạo ngoại lệ không tìm thấy với thông báo đã cho.
        /// </summary>
        /// <param name="message">Thông báo lỗi.</param>
        public NotFoundException(string message) : base(message) { }

        /// <summary>
        /// Hàm khởi tạo ngoại lệ không tìm thấy với thông báo và mã lỗi đã cho.
        /// </summary>
        /// <param name="message">Thông báo lỗi.</param>
        /// <param name="errorCode">Mã lỗi.</param>
        public NotFoundException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}

