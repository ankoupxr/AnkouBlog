
using AnkouBlog.IServices.BASE;
using AnkouBlog.Model.Models;
using System.Threading.Tasks;

namespace AnkouBlog.IServices
{	
	/// <summary>
	/// UserRoleServices
	/// </summary>	
    public interface IUserRoleServices :IBaseServices<UserRole>
	{
        Task<UserRole> SaveUserRole(int uid, int rid);
        Task<int> GetRoleIdByUid(int uid);


    }
}	