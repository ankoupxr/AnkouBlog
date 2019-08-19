
using AnkouBlog.IServices;
using AnkouBlog.IRepository;
using AnkouBlog.Model.Models;
using AnkouBlog.Services.BASE;

namespace AnkouBlog.Services
{	
	/// <summary>
	/// ModulePermissionServices
	/// </summary>	
	public class ModulePermissionServices : BaseServices<ModulePermission>, IModulePermissionServices
    {
	
        IModulePermissionRepository dal;
        public ModulePermissionServices(IModulePermissionRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------ModulePermission结束----------
	