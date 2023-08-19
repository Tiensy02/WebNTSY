using NTSY.CevgovApp.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace NTSY.CevgovApp
{
    /// <summary>
    /// Middleware xử lý các ngoại lệ trong ứng dụng.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Khởi tạo mới một đối tượng ExceptionMiddleware.
        /// </summary>
        /// <param name="_next">Đối tượng RequestDelegate được tiếp tục trong pipeline.</param>
        public ExceptionMiddleware(RequestDelegate _next)
        {
            this._next = _next;
        }

        /// <summary>
        /// Phương thức Invoke của middleware.
        /// </summary>
        /// <param name="context">Đối tượng HttpContext.</param>
        /// <returns>Task</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Xử lý ngoại lệ.
        /// </summary>
        /// <param name="context">Đối tượng HttpContext.</param>
        /// <param name="ex">Ngoại lệ được ném.</param>
        /// <returns>Task</returns>
        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            Console.WriteLine(ex);
            context.Response.ContentType = "application/json";
            if (ex is NotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(text: new BaseException()
                {
                    ErrorCode = ((NotFoundException)ex).ErrorCode,
                    UserMess = "Không tìm thấy tài nguyên",
                    DevMess = ex.Message,
                    TraceID = context.TraceIdentifier,
                    MoreInfo = ex.HelpLink,

                }.ToString() ?? "");

            }
            else if (ex is ConflictException conflictExcep)
            {
                context.Response.StatusCode = conflictExcep.ErrorCode;

                await context.Response.WriteAsync(text: new BaseException()
                {
                    ErrorCode = conflictExcep.ErrorCode,
                    UserMess = ex.Message,
                    DevMess = "lỗi trùng code khi thêm mới hoặc update dữ liệu",
                    TraceID = context.TraceIdentifier,
                    MoreInfo = ex.HelpLink
                }.ToString() ?? "");
            }
            else if(ex is DeleteEmulationTitleException deleteException )
            {
                context.Response.StatusCode = deleteException.ErrorCode;

                await context.Response.WriteAsync(text: new BaseException()
                {

                    ErrorCode = deleteException.ErrorCode,
                    UserMess = deleteException.Message,
                    DevMess = deleteException.Message,
                    TraceID = context.TraceIdentifier,
                    EmulationTitleNames = deleteException.EmulationTitleNames,
                    MoreInfo = ex.HelpLink
                }.ToString() ?? "");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(text: new BaseException()
                {
                    ErrorCode = context.Response.StatusCode,
                    UserMess = "lỗi hệ thống, vui lòng liên hệ MISA để được giải quyết",
                    DevMess = ex.Message,
                    TraceID = context.TraceIdentifier,
                    MoreInfo = ex.HelpLink
                }.ToString() ?? "");
            }
        }
    }
}