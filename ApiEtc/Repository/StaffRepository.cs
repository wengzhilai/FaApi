using ApiEtc.Config;
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

                if(string.IsNullOrEmpty(inObj.name) || string.IsNullOrEmpty(inObj.phone) || string.IsNullOrEmpty(inObj.idNo))
                {
                    reObj.success = false;
                    reObj.msg = "绑定的用户不能为空";
                    return reObj;
                }

                var token = RedisReadHelper.StringGet("WECHA_ACCESS_TOKEN");
                if (string.IsNullOrEmpty(token))
                {
                    token = Helper.WeiChat.Utility.GetAccessToken(AppConfig.WeiXin.Appid, AppConfig.WeiXin.Secret);
                    RedisWriteHelper.SetString("WECHA_ACCESS_TOKEN", token, new TimeSpan(2, 0, 0));
                }

                string postTicketStr = "{\"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": {\"scene\": {\"scene_str\": \"etc_87000073|" + inObj.phone + "\"}}}";

                var staff = await dapper.Single(x => x.openid == inObj.Key);
                int opNum = 0;
                if (staff == null)
                {
                    staff = await dapper.Single(x => x.phone == inObj.phone);
                    //全新用户
                    if (staff == null)
                    {

                        staff = new EtcStaffEntity()
                        {
                            name = inObj.name,
                            phone = inObj.phone,
                            idNo = inObj.idNo,
                            etcNo = "87000073",
                            ticket = Helper.WeiChat.Utility.GetQrCodeTicket(token, postTicketStr),
                            openid = inObj.Key,
                            createTime = DateTime.Now
                        };

                        opNum = await dapper.Save(new DtoSave<EtcStaffEntity>
                        {
                            data = staff
                        });
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(staff.etcNo))
                        {
                            staff.etcNo = "87000073";
                        }
                        else
                        {
                            postTicketStr = "{\"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": {\"scene\": {\"scene_str\": \"etc_" + staff.etcNo + "|" + inObj.phone + "\"}}}";
                        }
                        staff.name = inObj.name;
                        staff.phone = inObj.phone;
                        staff.idNo = inObj.idNo;
                        staff.ticket = Helper.WeiChat.Utility.GetQrCodeTicket(token, postTicketStr);
                        staff.openid = inObj.Key;
                        opNum = await dapper.Update(new DtoSave<EtcStaffEntity>
                        {
                            data = staff,
                            saveFieldList = new List<string> { "name", "openid", "idNo", "ticket", "etcNo" },
                            whereList = new List<string> { "phone" }
                        });
                    }

                }
                else
                {

                    if (!string.IsNullOrEmpty(staff.name) || !string.IsNullOrEmpty(staff.phone))
                    {
                        reObj.success = false;
                        reObj.msg = "该用户已绑定";
                        return reObj;
                    }

                    staff.name = inObj.name;
                    staff.phone = inObj.phone;
                    staff.idNo = inObj.idNo;
                    staff.ticket = Helper.WeiChat.Utility.GetQrCodeTicket(token, postTicketStr);
                    staff.openid = inObj.Key;
                    staff.etcNo = string.IsNullOrEmpty(staff.etcNo) ? "87000073" : staff.etcNo;

                    opNum = await dapper.Update(new DtoSave<EtcStaffEntity>
                    {
                        data = staff,
                        saveFieldList = new List<string> { "name", "phone", "idNo", "ticket", "etcNo" },
                        whereList = new List<string> { "openid" }
                    });
                }
                reObj.success = opNum > 0;
                if (!reObj.success)
                {
                    reObj.msg = "保存失败";
                }
                else
                {
                    await new WeixinRepository().save(new EtcWeixinEntity { openid = staff.openid, ticket = staff.ticket });
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
                reObj.success = true;
                if (staff == null)
                {
                    reObj.data = false;
                }
                else
                {
                    reObj.data = (!string.IsNullOrEmpty(staff.name) && !string.IsNullOrEmpty(staff.phone));
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
                staff.etcNoPic = string.Format("/PromotePic/{0}", staff.etcNoPic);
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
                var staff = await dapper.FindAll(x=>x.id>0);
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
                    inEnt.data.etcNo1 = inEnt.data.etcNoPic.Substring(0, inEnt.data.etcNoPic.IndexOf('.'));
                    if(inEnt.saveFieldList!=null) inEnt.saveFieldList.Add("etcNo1");
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
