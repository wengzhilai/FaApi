
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
    public class UserRepository : IUserRepository
    {
        DapperHelper<FaUserEntity> dbHelper = new DapperHelper<FaUserEntity>();
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<FaUserEntity> SingleByKey(int key)
        {
            var ent=await dbHelper.SingleByKey(key);
            ent.roleIdList = (await new DapperHelper<FaUserRoleEntityView>().FindAll(i => i.USER_ID == key)).Select(x => x.ROLE_ID).ToList();
            return ent;
        }
        

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        public Task<IEnumerable<FaUserEntity>> FindAll(Expression<Func<FaUserEntity, bool>> inParm = null)
        {
            return dbHelper.FindAll(inParm);
        }

        public Task<int> Update(DtoSave<FaUserEntity> inObj)
        {
            return dbHelper.Update(inObj);
        }

        public async Task<Result<FaUserEntity>> UserLogin(string username, string password)
        {
            Result<FaUserEntity> reObj = new Result<FaUserEntity>();
            DapperHelper<FaLoginEntity> dapper=new DapperHelper<FaLoginEntity>();
            var login=await dapper.Single(x=>x.LOGIN_NAME==username);
            if (login != null)
            {
                if (login.PASSWORD.ToLower().Equals(Helper.StringHelp.Get32MD5One(password).ToLower()))
                {
                    reObj.data =await new DapperHelper<FaUserEntity>().Single(x=>x.LOGIN_NAME==username);
                }
                else
                {
                    reObj.msg = "密码错误";
                }
            }
            else
            {
                reObj.msg = "用户名不存在";
            }
            return reObj;
        }

        public async Task<Result<int>> Save(DtoSave<FaUserEntity> inEnt)
        {
            Result<int> reObj = new Result<int>();
            try
            {
                dbHelper.TranscationBegin();
                if (inEnt.Data.ID == 0)
                {
                    inEnt.Data.ID = await new SequenceRepository().GetNextID<FaUserEntity>();
                    reObj.data = await dbHelper.Save(inEnt);
                }
                else
                {
                    reObj.data = await dbHelper.Update(inEnt);
                }

                reObj.success = reObj.data > 0;
                if (!reObj.success)
                {
                    reObj.msg = "保存角色失败";
                    dbHelper.TranscationRollback();
                }
                else
                {
                    
                    DapperHelper.Init(dbHelper.GetConnection(), dbHelper.GetTransaction());
                    await DapperHelper.Exec("delete from fa_user_role where USER_ID = " + inEnt.Data.ID);
                    var opNum = await DapperHelper.Exec(string.Format("insert into fa_user_role(ROLE_ID,USER_ID) values({0},{1}) ",inEnt.Data.roleIdList, inEnt.Data.ID));
                    if (opNum != 1)
                    {
                        reObj.success = false;
                        reObj.msg = "保存用户模块失败";
                        dbHelper.TranscationRollback();
                    }
                    else
                    {
                        reObj.success=true;
                        dbHelper.TranscationCommit();
                    }
                }

            }
            catch (Exception e)
            {
                reObj.success = false;
                reObj.msg = "保存用户失败";
                LogHelper.WriteErrorLog(this.GetType(), reObj.msg, e);
                dbHelper.TranscationRollback();
            }
            return reObj;
        }

        public async Task<Result<int>> Delete(int keyId)
        {
            Result<int> reObj = new Result<int>();
            reObj.data = await dbHelper.Delete(i => i.ID == keyId);
            reObj.success = reObj.data > 0;
            return reObj;
        }
    }
}
