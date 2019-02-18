using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// 注册UserInfo
    /// </summary>
    public class RegUserInfo
    {
        /// <summary>
        /// 出生时间
        /// </summary>
        /// <value></value>
        public string BirthdayTime{get;set;}
        /// <summary>
        /// 头像地址
        /// </summary>
        /// <value></value>
        public string IconFilesId{get;set;}
        /// <summary>
        /// 年份类型
        /// </summary>
        /// <value></value>
        public string YearsType{get;set;}
        /// <summary>
        /// 出生地
        /// </summary>
        /// <value></value>
        public string BirthdayPlace{get;set;}
        /// <summary>
        /// 短信验证码
        /// </summary>
        /// <value></value>
        public string Code{get;set;}
        /// <summary>
        /// 排行
        /// </summary>
        /// <value></value>
        public int LevelId{get;set;}
        /// <summary>
        /// 登录名
        /// </summary>
        /// <value></value>
        public string LoginName{get;set;}
        /// <summary>
        /// 父母组,最多只有两条，第一条为本人，第二条为父亲
        /// </summary>
        /// <value></value>
        public List<KV> ParentArr{get;set;}
        
        /// <summary>
        /// 密码
        /// </summary>
        /// <value></value>
        public string Password{get;set;}
        
        /// <summary>
        /// 推荐码
        /// </summary>
        /// <value></value>
        public string PollCode{get;set;}
        //性别
        public string Sex{get;set;}
    }
}