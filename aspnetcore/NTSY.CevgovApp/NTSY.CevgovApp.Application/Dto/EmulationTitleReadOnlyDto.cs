using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTSY.CevgovApp.Domain;


namespace NTSY.CevgovApp.Application
{
    public class EmulationTitleReadOnlyDto
    {
        public String EmulationCode { get; set; }
        public EmulationAwardRecipient EmulationAwardRecipient { get; set; }
        public EmulationMovementType EmulationMovementType { get; set; }
        public EmulationStatus EmulationStatus { get; set; }
        public String EmulationName { get; set; }
        public String AwardName { get; set; }






    }
}
