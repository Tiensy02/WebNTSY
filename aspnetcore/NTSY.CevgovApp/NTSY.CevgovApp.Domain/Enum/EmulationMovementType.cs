using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Domain
{
    /// <summary>
    /// Enum của loại phong trào áp dụng; 1- thường xuyên; 2- theo đợt; 0 - thường xuyên, theo đợt
    /// </summary>
    public enum EmulationMovementType
    {
        Frequent = 1, // Thường xuyên
        Batch = 2, // theo đợt
        FrequentAndBatch = 0 // thường xuyên và theo đợt

    }
}
