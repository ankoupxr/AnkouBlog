
using AnkouBlog.IRepository;
using AnkouBlog.Model.Models;
using AnkouBlog.Repository.BASE;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnkouBlog.Repository
{	
	/// <summary>
	/// RoleModulePermissionRepository
	/// </summary>	
	public class RoleModulePermissionRepository : BaseRepository<RoleModulePermission>, IRoleModulePermissionRepository
    {
        public async Task<List<RoleModulePermission>> WithChildrenModel()
        {
            var list = await Task.Run(() => Db.Queryable<RoleModulePermission>()
            .Mapper(it => it.Role,it => it.RoleId)
            .Mapper(it => it.Permission,it => it.PermissionId)
            .Mapper(it => it.Module,it => it.ModuleId).ToList());

            return null;

        }
       
    }
}