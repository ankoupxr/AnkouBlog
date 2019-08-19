using AnkouBlog.IRepository.BASE;
using AnkouBlog.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlSugar;

namespace AnkouBlog.IRepository
{	
	/// <summary>
	/// IRoleModulePermissionRepository
	/// </summary>	
	public interface IRoleModulePermissionRepository : IBaseRepository<RoleModulePermission>
    {
        Task<List<RoleModulePermission>> WithChildrenModel();
    }
}
	