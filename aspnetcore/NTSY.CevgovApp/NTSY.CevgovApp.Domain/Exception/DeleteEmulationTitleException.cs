using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Domain
{
    public class DeleteEmulationTitleException : Exception
    {
        // mã lỗi
        public int ErrorCode { get; set; }
        // mảng chứa mã danh hiệu thi đua của những danh hiệu thi đua không xóa được
        public List<String> EmulationTitleNames { get; set; }

        /// <summary>
        /// Hàm khởi tạo ngoại lệ với mã lỗi đã cho.
        /// </summary>
        /// <param name="errorCode">Mã lỗi.</param>
        public DeleteEmulationTitleException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Hàm khởi tạo ngoại lệ với thông báo đã cho.
        /// </summary>
        /// <param name="message">Thông báo lỗi.</param>
        public DeleteEmulationTitleException(string message) : base(message) { }

        /// <summary>
        /// Hàm khởi tạo ngoại lệ với thông báo và mã lỗi đã cho.
        /// </summary>
        /// <param name="message">Thông báo lỗi.</param>
        /// <param name="errorCode">Mã lỗi.</param>
        public DeleteEmulationTitleException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// hàm khởi tạo ngoại lệ với mã lỗi và danh sách chứ mã danh hiệu thi đua
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        /// <param name="emulationTitleNames"></param>
        public DeleteEmulationTitleException(string message, int errorCode , List<String> emulationTitleNames) :base(message)
        {
            ErrorCode = errorCode;
            EmulationTitleNames = emulationTitleNames;
        }

    }
}
