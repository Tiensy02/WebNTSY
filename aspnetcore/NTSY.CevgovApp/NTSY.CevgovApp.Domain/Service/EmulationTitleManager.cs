using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Domain
{
    public class EmulationTitleManager : IEmulationTitleManager
    {
        private readonly IEmulationTitleRepository _emulationTitleRepo;
        public EmulationTitleManager(IEmulationTitleRepository emulationTitleRepo)
        {
            _emulationTitleRepo = emulationTitleRepo;
        }
        /// <summary>
        /// hàm kiểm tra có bị trùng mã danh hiệu thi đua hay không nếu trùng thì bắn ra 1 exception
        /// </summary>
        /// <param name="code"> mã danh hiệu thi đua </param>
        /// <returns></returns>
        public async Task CheckEmulationTitleExistByCode(string code , Guid? id = null)
        {
            var EmulationTitleExit = await _emulationTitleRepo.GetEmulationTitleByCode(code);
            if (EmulationTitleExit != null && EmulationTitleExit.EmulationID != id)
            {
                throw new ConflictException("trùng mã danh hiệu thi đua", 409);
            }
        }
        /// <summary>
        /// kiểm tra xem trong những danh hiệu thi đua có id nằm trong ids có tồn tại danh hiệu thi đua cấp hệ thông không
        /// </summary>
        /// <param name="ids">danh sách id</param>
        /// <returns></returns>
        public async Task CheckEmulationTitleSystemExist(List<Guid> ids)
        {
            var emulationTitleSystems = await _emulationTitleRepo.GetEmulationTitleSystem(ids);
            if(emulationTitleSystems.Count() > 0 )
            {
                List<String> emulationTitleNames = new List<string>();
                foreach(var element in emulationTitleSystems)
                {
                    emulationTitleNames.Add(element.EmulationName);
                }
                throw new DeleteEmulationTitleException(errorCode: 403, emulationTitleNames: emulationTitleNames, message:"không thể xóa do tồn tại danh hiệu thi đua của hệ thống");
            }
        }
    }
}
