using AnkouBlog.IServices.BASE;
using AnkouBlog.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnkouBlog.IServices
{	
	/// <summary>
	/// TopicDetailServices
	/// </summary>	
    public interface ITopicDetailServices :IBaseServices<TopicDetail>
	{
        Task<List<TopicDetail>> GetTopicDetails();


    }
}

	//----------TopicDetail结束----------
	