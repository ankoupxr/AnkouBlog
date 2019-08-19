using AnkouBlog.IServices;
using AnkouBlog.IServices.BASE;
using AnkouBlog.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnkouBlog.IServices
{	
	/// <summary>
	/// PermissionServices
	/// </summary>	
    public interface IPermissionServices :IBaseServices<Permission>
	{
        Task<List<RoleModulePermission>> TestModelWithChildren();


    }
}

	//----------Permission结束----------
	