
using AnkouBlog.IServices.BASE;
using AnkouBlog.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnkouBlog.IServices
{	
	/// <summary>
	/// TopicServices
	/// </summary>	
    public interface ITopicServices :IBaseServices<Topic>
	{
        Task<List<Topic>> GetTopics();


    }
}

	//----------Topic结束----------
	