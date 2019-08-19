
using AnkouBlog.IServices;
using AnkouBlog.IRepository;
using AnkouBlog.Model.Models;
using AnkouBlog.Services.BASE;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnkouBlog.Services
{	
	/// <summary>
	/// PermissionServices
	/// </summary>	
	public class PermissionServices : BaseServices<Permission>, IPermissionServices
    {

        IPermissionRepository _dal;
        public PermissionServices(IPermissionRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

        public Task<List<RoleModulePermission>> TestModelWithChildren()
        {
            throw new System.NotImplementedException();
        }
    }
}

	//----------Permission结束----------
	