
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
	/// TopicServices
	/// </summary>	
	public class TopicServices : BaseServices<Topic>, ITopicServices
    {
	
        ITopicRepository _dal;
        public TopicServices(ITopicRepository dal)
        {
            _dal = dal;
            base.BaseDal = dal;
        }

        /// <summary>
        /// 获取开Bug专题分类（缓存）
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 60)]
        public async Task<List<Topic>> GetTopics()
        {
            return await base.Query(a => !a.tIsDelete && a.tSectendDetail == "tbug");
        }

    }
}

	//----------Topic结束----------
	