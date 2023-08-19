using Dapper;
using NTSY.CevgovApp.Domain;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.CevgovApp.Infastructure
{
    public class AwardRespository : BaseRepository<Award>, IAwardRespository
    {
        public AwardRespository(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
