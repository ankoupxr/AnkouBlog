

using AnkouBlog.IServices;
using AnkouBlog.IServices.BASE;
using AnkouBlog.Model.Models;
using System.Threading.Tasks;

namespace AnkouBlog.IServices
{	
	/// <summary>
	/// RoleServices
	/// </summary>	
    public interface IRoleServices :IBaseServices<Role>
	{
        Task<Role> SaveRole(string roleName);
        Task<string> GetRoleNameByRid(int rid);


    }
}

	//----------Role结束----------
	