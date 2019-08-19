
using AnkouBlog.IServices.BASE;
using AnkouBlog.Model.Models;
using System.Threading.Tasks;

namespace AnkouBlog.IServices
{	
	/// <summary>
	/// sysUserInfoServices
	/// </summary>	
    public interface IsysUserInfoServices :IBaseServices<sysUserInfo>
	{
        Task<string> GetUserRoleNameStr(string loginName, string loginPwd);
        Task<sysUserInfo> SaveUserInfo(string loginName, string loginPwd);


    }
}

	//----------sysUserInfo结束----------
	