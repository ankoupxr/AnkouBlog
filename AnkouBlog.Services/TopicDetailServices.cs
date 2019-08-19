
using AnkouBlog.IServices;
using AnkouBlog.IRepository;
using AnkouBlog.Model.Models;
using AnkouBlog.Services.BASE;
using AnkouBlog.Common;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AnkouBlog.Services
{	
	/// <summary>
	/// TopicDetailServices
	/// </summary>	
	public class TopicDetailServices : BaseServices<TopicDetail>, ITopicDetailServices
    {
	
        ITopicDetailRepository _dal;
        public TopicDetailServices(ITopicDetailRepository dal)
        {
            _dal = dal;
            base.BaseDal = dal;
        }
        /// <summary>
        /// 获取开Bug数据（缓存）
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10)]
        public async Task<List<TopicDetail>> GetTopicDetails()
        {
            return await base.Query(a => !a.tdIsDelete && a.tdSectendDetail == "tbug");
        }

    }
}

	