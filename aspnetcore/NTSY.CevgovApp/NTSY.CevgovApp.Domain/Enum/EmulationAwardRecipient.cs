using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Domain
{
    /// <summary>
    /// Enum của đối tượng khen thưởng; 1-cá nhân; 2- tập thể; 0 - cá nhân và tập thể  3 - gia đình
    /// </summary>
    public enum EmulationAwardRecipient
    {
        Individual = 1, // cá nhân
        Collective = 2, // tập thể
        Family =3,// Gia đình
        IndividualAndCollective = 0 // cá nhân và tập thể
    }
}
