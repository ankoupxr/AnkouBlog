using AnkouBlog.IServices.BASE;
using AnkouBlog.Model.Models;
using AnkouBlog.Model.ViewModels;
using System.Threading.Tasks;

namespace AnkouBlog.IServices
{	
	/// <summary>
	/// BlogArticleServices
	/// </summary>	
    public interface IBlogArticleServices :IBaseServices<BlogArticle>
	{
        Task<BlogViewModels> GetBlogDetails(int id);


    }
}
	