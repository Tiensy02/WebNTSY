using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Domain
{
    public class ConflictException : Exception
    {

        public ConflictException() { }
        /// <summary>
        /// Mã lỗi của ngoại lệ.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Hàm khởi tạo ngoại lệ với mã lỗi đã cho.
        /// </summary>
        /// <param name="errorCode">Mã lỗi.</param>
        public ConflictException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Hàm khởi tạo ngoại lệ với thông báo đã cho.
        /// </summary>
        /// <param name="message">Thông báo lỗi.</param>
        public ConflictException(string message) : base(message) { }

        /// <summary>
        /// Hàm khởi tạo ngoại lệ với thông báo và mã lỗi đã cho.
        /// </summary>
        /// <param name="message">Thông báo lỗi.</param>
        /// <param name="errorCode">Mã lỗi.</param>
        public ConflictException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

    }
}
