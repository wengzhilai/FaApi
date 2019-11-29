using Models;
using Models.Entity;
using System.Threading.Tasks;


namespace ApiUser.Controllers
{
    public interface ILoginController
    {
        /// <summary>
        /// 密码登录
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<ResultObj<string>> userLogin(UserLoginDto inEnt);

        /// <summary>
        /// 验证码登录
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<ResultObj<string>> UserCodeLogin(UserCodeLoginDto inEnt);

        Task<ResultObj<int>> loginReg(LogingDto inEnt);

        Task<Result> resetPassword(ResetPasswordDto inEnt);

        Task<Result> deleteUser(DtoKey userName);

        Task<ResultObj<bool>> userEditPwd(EditPwdDto inEnt);

    }

    public class UserLoginDto
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }

    public class UserCodeLoginDto
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
    }
}
