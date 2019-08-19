using System.Collections.Generic;
using System.Threading.Tasks;
using AnkouBlog.IServices;
using AnkouBlog.IServices.BASE;
using AnkouBlog.Model.Models;

namespace AnkouBlog.IServices
{
    /// <summary>
    /// RoleModulePermissionServices
    /// </summary>	
    public interface IRoleModulePermissionServices : IBaseServices<RoleModulePermission>
    {
        Task<List<RoleModulePermission>> GetRoleModule();
    }
}

	//----------RoleModulePermission结束----------
	