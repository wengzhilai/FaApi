
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
// CREATE TABLE `sequence` (
//   `seq_name` varchar(50) NOT NULL,
//   `current_val` int(11) NOT NULL,
//   `increment_val` int(11) NOT NULL DEFAULT '1',
//   PRIMARY KEY (`seq_name`)
//   )
    public class SequenceRepository
    {
        DapperHelper<SequenceEntity> dbHelper = new DapperHelper<SequenceEntity>();
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetNextID<T>()where T : new()
        {
            string tableName=new ModelHelper<T> ().GetTableName();
            var single=dbHelper.SingleByKey(tableName);
            if(single==null){
                single=new SequenceEntity();
                single.seq_name=tableName;
                single.current_val=1;
                single.increment_val=1;
                dbHelper.Save(new DtoSave<SequenceEntity>{
                    Data=single,
                    IgnoreFieldList=new List<string>()
                });
            }else{
                single.current_val+=single.increment_val;
                dbHelper.Update(new DtoSave<SequenceEntity>{
                    Data=single,
                    SaveFieldList=new List<string>{"current_val"}
                });
            }
            return single.current_val;
        }


    }
}
