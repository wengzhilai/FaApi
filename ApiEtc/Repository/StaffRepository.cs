using ApiEtc.Controllers.Interface;
using ApiEtc.Models.Entity;
using Helper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEtc.Repository
{
    /// <summary>
    /// 员工
    /// </summary>
    public class StaffRepository : IStaff
    {
        DapperHelper<EtcStaffEntity> dapper = new DapperHelper<EtcStaffEntity>();

        public async Task<ResultObj<bool>> regStaff(RegStaffDto inObj)
        {
            var reObj = new ResultObj<bool>();
            try
            {
                if (string.IsNullOrEmpty(inObj.Key))
                {
                    reObj.success = false;
                    reObj.msg = "openid不能为空";
                    return reObj;
                }

                if (string.IsNullOrEmpty(inObj.ticket) )
                {
                    reObj.success = false;
                    reObj.msg = "ticket不能为空";
                    return reObj;
                }

                var staff = await dapper.Single(x => x.openid == inObj.Key);

                int opNum = 0;
                if (staff == null)
                {
                    opNum = await dapper.Save(new DtoSave<EtcStaffEntity>
                    {
                        data = new EtcStaffEntity
                        {
                            ticket=inObj.ticket,
                            parentTicket=inObj.parentTicket,
                            openid = inObj.Key,
                            createTime = DateTime.Now
                        }
                    });
                }
                else
                {
                    if (!string.IsNullOrEmpty(staff.name) || !string.IsNullOrEmpty(staff.phone))
                    {
                        reObj.success = false;
                        reObj.msg = "该用户已绑架";
                        return reObj;
                    }
                    opNum = await dapper.Update(new DtoSave<EtcStaffEntity>
                    {
                        data = new EtcStaffEntity
                        {
                            ticket = inObj.ticket,
                            parentTicket=inObj.parentTicket,
                            openid = inObj.Key
                        },
                        saveFieldList = new List<string> { "ticket", "parentTicket" },
                        whereList = new List<string> { "openid" }
                    });
                }
                reObj.success = opNum > 0;
                if (!reObj.success)
                {
                    reObj.msg = "保存失败";
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        public async Task<Result> bindUser(BindUserDto inObj)
        {
            var reObj = new Result();
            try
            {
                if (string.IsNullOrEmpty(inObj.Key))
                {
                    reObj.success = false;
                    reObj.msg = "openid不能为空";
                    return reObj;
                }

                if(string.IsNullOrEmpty(inObj.name) || string.IsNullOrEmpty(inObj.phone))
                {
                    reObj.success = false;
                    reObj.msg = "绑定的用户不能为空";
                    return reObj;
                }

                var staff = await dapper.Single(x => x.openid == inObj.Key);
                int opNum = 0;
                if (staff == null)
                {
                    staff = await dapper.Single(x => x.phone == inObj.phone);
                    if (staff == null)
                    {
                        opNum = await dapper.Save(new DtoSave<EtcStaffEntity>
                        {
                            data = new EtcStaffEntity
                            {
                                name = inObj.name,
                                phone = inObj.phone,
                                openid = inObj.Key,
                                createTime = DateTime.Now
                            }
                        });
                    }
                    else
                    {
                        opNum = await dapper.Update(new DtoSave<EtcStaffEntity>
                        {
                            data = new EtcStaffEntity
                            {
                                name = inObj.name,
                                phone = inObj.phone,
                                openid = inObj.Key
                            },
                            saveFieldList = new List<string> { "name", "openid" },
                            whereList = new List<string> { "phone" }
                        });
                    }

                    
                }
                else
                {
                    if (!string.IsNullOrEmpty(staff.name) || !string.IsNullOrEmpty(staff.phone))
                    {
                        reObj.success = false;
                        reObj.msg = "该用户已绑架";
                        return reObj;
                    }
                    opNum = await dapper.Update(new DtoSave<EtcStaffEntity>
                    {
                        data = new EtcStaffEntity
                        {
                            name = inObj.name,
                            phone = inObj.phone,
                            openid = inObj.Key
                        },
                        saveFieldList = new List<string> { "name", "phone" },
                        whereList = new List<string> { "openid" }
                    });
                }
                reObj.success = opNum > 0;
                if (!reObj.success)
                {
                    reObj.msg = "保存失败";
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 检测用户,data为true表示，存在
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        public async Task<ResultObj<bool>> checkIsBind(DtoKey inObj)
        {
            var reObj = new ResultObj<bool>();
            try
            {
                if (string.IsNullOrEmpty(inObj.Key))
                {
                    reObj.success = false;
                    reObj.msg = "openId不能为空";
                    return reObj;
                }
                var staff = await dapper.Single(x => x.openid == inObj.Key);
                int opNum = 0;
                if (staff == null)
                {
                    opNum = await dapper.Save(new DtoSave<EtcStaffEntity>
                    {
                        data = new EtcStaffEntity
                        {
                            openid = inObj.Key,
                            createTime=DateTime.Now
                        }
                    });
                    reObj.success = opNum > 0;
                    reObj.data = false;
                }
                else
                {
                    reObj.data = (!string.IsNullOrEmpty(staff.name) && !string.IsNullOrEmpty(staff.phone));
                    reObj.success = true;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 获取员工的信息，包括二维码地址，Key为OpenId
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        public async Task<ResultObj<EtcStaffEntity>> getStaff(DtoKey inObj)
        {
            var reObj = new ResultObj<EtcStaffEntity>();
            try
            {
                if (string.IsNullOrEmpty(inObj.Key))
                {
                    reObj.success = false;
                    reObj.msg = "openId不能为空";
                    return reObj;
                }

                var staff = await dapper.Single(x => x.openid == inObj.Key);
                if(staff == null)
                {
                    staff = new EtcStaffEntity { openid = inObj.Key,createTime=DateTime.Now };
                    staff.id=await dapper.Save(new DtoSave<EtcStaffEntity>
                    {
                        data = staff,
                        ignoreFieldList=null
                    });
                }

                staff.qrCode = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + staff.ticket;

                reObj.data = staff;
                reObj.success = true;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        public async Task<ResultObj<EtcStaffEntity>> singleByKey(DtoDo<int> inEnt)
        {
            var reObj = new ResultObj<EtcStaffEntity>();
            try
            {
                var staff = await dapper.SingleByKey(inEnt.Key);
                reObj.data = staff;
                reObj.success = true;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        public async Task<ResultObj<EtcStaffEntity>> getStaffList()
        {
            var reObj = new ResultObj<EtcStaffEntity>();
            try
            {
                var staff = await dapper.FindAll(x=>x.id>10);
                reObj.dataList = staff.ToList();
                reObj.success = true;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        public async Task<ResultObj<EtcStaffEntity>> updateTicket(EtcStaffEntity inObj)
        {
            var reObj = new ResultObj<EtcStaffEntity>();
            try
            {
                var opNum = await dapper.Update(new DtoSave<EtcStaffEntity>
                {
                    data = inObj,
                    saveFieldList = new List<string> { "ticket"},
                    whereList = new List<string> { "id" }
                });
                reObj.success = true;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        public async Task<ResultObj<EtcStaffEntity>> getStaffByTicket(DtoKey inObj)
        {
            var reObj = new ResultObj<EtcStaffEntity>();
            try
            {
                if (string.IsNullOrEmpty(inObj.Key))
                {
                    reObj.success = false;
                    reObj.msg = "openId不能为空";
                    return reObj;
                }

                var staff = await dapper.Single(x => x.ticket == inObj.Key);
                if (staff == null)
                {
                    staff = new EtcStaffEntity { openid = inObj.Key, createTime = DateTime.Now };
                    staff.id = await dapper.Save(new DtoSave<EtcStaffEntity>
                    {
                        data = staff,
                        ignoreFieldList = null
                    });
                }

                staff.qrCode = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + staff.ticket;

                reObj.data = staff;
                reObj.success = true;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        public async Task<ResultObj<int>> save(DtoSave<EtcStaffEntity> inEnt)
        {
            var reObj = new ResultObj<int>();
            try
            {
                var client = await dapper.SingleByKey(inEnt.data.id);
                if (client == null)
                {
                    reObj.success = false;
                    reObj.msg = "Id有误";
                    return reObj;
                }

                if (!string.IsNullOrEmpty(inEnt.data.etcNoPic) && inEnt.data.etcNoPic.IndexOf('.')>0)
                {
                    inEnt.data.etcNo = inEnt.data.etcNoPic.Substring(0, inEnt.data.etcNoPic.IndexOf('.'));
                    if(inEnt.saveFieldList!=null) inEnt.saveFieldList.Add("etcNo");
                }

                //inEnt.saveFieldList = new List<string> { "remark", "carNum", "carType", "submitTime", "status", "opuserName" };
                inEnt.ignoreFieldList = null;
                inEnt.whereList = new List<string> { "id" };
                inEnt.token = inEnt.token.Replace("#", "");
                var opNum = await dapper.Update(inEnt);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }
    }
}
