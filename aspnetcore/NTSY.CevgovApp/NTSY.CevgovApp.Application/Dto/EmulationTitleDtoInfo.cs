using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Application.Dto
{
    public class EmulationTitleInfoDto
    {
        /// <summary>
        /// Danh sách emulationtitle dto
        /// </summary>
        public IEnumerable<EmulationTitleDto> EmulationTitles { get; set; }
        /// <summary>
        /// tổng số bản ghi trong database  
        /// </summary>
        public int TotalRecord { get; set; }
    }
}
