using AutoMapper;
using IRepository;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Models.Entity;
using Models.EntityView;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.MapperCfg.Profiles
{
    /// <summary>
    /// 
    /// </summary>
    public class UserInfoProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public UserInfoProfile()
        {
            var elder=new ElderRepository();
            var user=new UserRepository();
            var file=new FileRepository();

            CreateMap<FaUserInfo, FaUserInfoEntity>()
                ;
            CreateMap<FaUserInfoEntity, FaUserInfo>()
                // .ForMember(d => d.Family, opt => { opt.MapFrom(m => Mapper.Map<FA_FAMILY>(m.fa_family)); })
                // .ForMember(d => d.Elder, opt => { opt.MapFrom(m => Mapper.Map<FA_ELDER>(m.fa_elder)); })
                // .ForMember(d => d.FriendList, opt => { opt.MapFrom(m => Mapper.Map<IList<USER>>(m.fa_user1.ToList())); })
                // .ForMember(d => d.EventList, opt => { opt.MapFrom(m => Mapper.Map<IList<FA_USER_EVENT>>(m.fa_user_event.ToList())); })
                // .ForMember(d => d.FatherName, opt => { opt.MapFrom(m => m.fa_user_info2 != null?m.fa_user_info2.fa_user.NAME:""); })
            ;
                
            CreateMap<FaUserInfoEntity, User>()
                // .ForMember(d => d.RoleAllIDStr, opt => { opt.MapFrom(m => string.Join(",", m.fa_user.fa_role.Select(x => x.ID))); })
                // .ForMember(d => d.RoleAllNameStr, opt => { opt.MapFrom(m => string.Join(",", m.fa_user.fa_role.Select(x => x.NAME))); })
                // .ForMember(d => d.AllModuleIdStr, opt => { opt.MapFrom(m => string.Join(",", m.fa_user.fa_module.Select(x => x.ID))); })
                // .ForMember(d => d.UserDistrict, opt => { opt.MapFrom(m => string.Join(",", m.fa_user.fa_district1.Select(x => x.NAME))); })
                // .ForMember(d => d.Family, opt => { opt.MapFrom(m => Mapper.Map<FA_FAMILY>(m.fa_family)); })
                // .ForMember(d => d.Elder, opt => { opt.MapFrom(m => Mapper.Map<FA_ELDER>(m.fa_elder)); })
                // .ForMember(d => d.FriendList, opt => { opt.MapFrom(m => Mapper.Map<IList<USER>>(m.fa_user1.ToList())); })
                // .ForMember(d => d.EventList, opt => { opt.MapFrom(m => Mapper.Map<IList<FA_USER_EVENT>>(m.fa_user_event.ToList())); })
                // .ForMember(d => d.FatherName, opt => { opt.MapFrom(m => m.fa_user_info2 != null?m.fa_user_info2.fa_user.NAME:""); })
                // .ForMember(d => d.NAME, opt => { opt.MapFrom(m =>m.fa_user.NAME); })
                ;

            CreateMap<FaUserEntity, FaUserInfo>();
            CreateMap<RelativeItem, FaUserInfoEntityView>();
            CreateMap<FaUserInfoEntityView, RelativeItem>()
                .ForMember(d => d.ElderId, opt => { opt.MapFrom(m =>m.ELDER_ID); })
                .ForMember(d => d.FatherId, opt => { opt.MapFrom(m =>m.FATHER_ID); })
            ;

            CreateMap<FaUserInfoEntity, RelativeItem>()
                .ForMember(d => d.ElderId, opt => { opt.MapFrom(m =>m.ELDER_ID); })
                .ForMember(d => d.ElderName, opt => { opt.MapFrom(m =>
                elder.SingleByKey(m.ELDER_ID.Value).Result.NAME
                ); })
                .ForMember(d => d.FatherId, opt => { opt.MapFrom(m =>m.FATHER_ID); })
                .ForMember(d => d.IcoUrl, opt => { opt.MapFrom(m =>
                file.SingleByKey(user.SingleByKey(m.ID).Result.ICON_FILES_ID.Value).Result.URL
                ); })
                .ForMember(d => d.Id, opt => { opt.MapFrom(m =>m.ID); })
                .ForMember(d => d.Name, opt => { opt.MapFrom(m => user.SingleByKey(m.ID).Result.NAME); })
                .ForMember(d => d.Sex, opt => { opt.MapFrom(m =>m.SEX); })
                ;
        }
    }
}