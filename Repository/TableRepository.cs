
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helper;
using IRepository;
using Models;
using System.Linq;
using Models.Entity;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
using System.Linq.Expressions;

namespace Repository
{
    public class TableRepository : ITableRepository
    {
        DapperHelper<FaEquipmentEntity> dbHelper = new DapperHelper<FaEquipmentEntity>();

        public Task<Result<KTV>> GetTableSelect()
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Save(DtoSave<FaTableTypeEntity> inEnt)
        {
            throw new NotImplementedException();
        }

        public Task<FaTableTypeEntity> SingleByKey<t>(t key)
        {
            throw new NotImplementedException();
        }
    }
}