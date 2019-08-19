
using AnkouBlog.IServices;
using AnkouBlog.IRepository;
using AnkouBlog.Model.Models;
using AnkouBlog.Services.BASE;
using System.Threading.Tasks;
using System.Linq;
using AnkouBlog.Common;

namespace AnkouBlog.Services
{	
	/// <summary>
	/// RoleServices
	/// </summary>	
	public class RoleServices : BaseServices<Role>, IRoleServices
    {
	
        IRoleRepository _dal;
        public RoleServices(IRoleRepository dal)
        {
            _dal = dal;
            base.BaseDal = dal;
        }

        public async Task<Role> SaveRole(string roleName)
        {
            Role role = new Role(roleName);
            Role model = new Role();
            var userList = await base.Query(a => a.Name == role.Name && a.Enabled);
            if(userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await base.Add(role);
                model = await base.QueryByID(id);
            }
            return model;
        }
        [Caching(AbsoluteExpiration = 30)]
        public async Task<string> GetRoleNameByRid(int rid)
        {
            return ((await base.QueryByID(rid))?.Name);
        }

    }
}
	