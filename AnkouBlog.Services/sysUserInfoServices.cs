
using AnkouBlog.IServices;
using AnkouBlog.IRepository;
using AnkouBlog.Model.Models;
using AnkouBlog.Services.BASE;
using System.Threading.Tasks;
using System.Linq;
using AnkouBlog.Common.Helper;

namespace AnkouBlog.Services
{	
	/// <summary>
	/// sysUserInfoServices
	/// </summary>	
	public class sysUserInfoServices : BaseServices<sysUserInfo>, IsysUserInfoServices
    {

        IsysUserInfoRepository _dal;
        IUserRoleServices _userRoleServices;
        IRoleRepository _roleRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="userRoleServices"></param>
        /// <param name="roleRepository"></param>
        public sysUserInfoServices(IsysUserInfoRepository dal, IUserRoleServices userRoleServices, IRoleRepository roleRepository)
        {
            this._dal = dal;
            this._userRoleServices = userRoleServices;
            this._roleRepository = roleRepository;
            base.BaseDal = dal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <returns></returns>
        public async Task<sysUserInfo> SaveUserInfo(string loginName,string loginPwd)
        {
            sysUserInfo sysUserInfo = new sysUserInfo(loginName, loginPwd);
            sysUserInfo model = new sysUserInfo();
            var userList = await base.Query(a => a.uLoginName == sysUserInfo.uLoginName
            && a.uLoginPWD == sysUserInfo.uLoginPWD);
            if(userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await base.Add(sysUserInfo);
                model = await base.QueryByID(id);
            }
            return model;
        }
        public async Task<string> GetUserRoleNameStr(string loginName,string loginPwd)
        {
            string roleName = "";
            var user = (await base.Query(a => a.uLoginName == loginName && a.uLoginPWD == loginPwd)).FirstOrDefault();

            if(user != null)
            {
                var userRoles = await _userRoleServices.Query(ur => ur.UserId == user.uID);
                if(userRoles.Count> 0)
                {
                    var roles = await _roleRepository.QueryByIDs(userRoles.Select(ur => ur.RoleId.ObjToString())
                        .ToArray());
                    roleName = string.Join(',', roles.Select(r => r.Name).ToArray());
                }
            }
            return roleName;
        }
    }
}
	