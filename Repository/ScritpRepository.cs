
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
    public class ScritpRepository : IScritpRepository
    {
        public Task<Result> ScriptDelete(int scriptId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FaScriptEntity>> ScriptList(DtoSearch<FaScriptEntity> inEnt)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> ScriptSave(DtoSave<FaScriptEntity> inEnt)
        {
            throw new NotImplementedException();
        }

        public Task<FaScriptEntity> ScriptSingleByKey(int key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FaScriptTaskEntity>> ScriptTaskList(DtoSearch<FaScriptEntity> inEnt)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FaScriptTaskLogEntity>> ScriptTaskLogList(DtoSearch<FaScriptTaskLogEntity> inEnt)
        {
            throw new NotImplementedException();
        }

        public Task<Result> StopTask(int taskId)
        {
            throw new NotImplementedException();
        }
    }
}
