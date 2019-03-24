
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
    public class EquipmentRepository : IEquipmentRepository
    {
        DapperHelper<FaEquipmentEntity> dbHelper = new DapperHelper<FaEquipmentEntity>();

        public Task<Result<int>> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<KTV>> GetTree()
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Save(DtoSave<FaEquipmentEntity> inEnt)
        {
            throw new NotImplementedException();
        }

        public Task<FaEquipmentEntity> SingleByKey<t>(t key)
        {
            throw new NotImplementedException();
        }
    }
}