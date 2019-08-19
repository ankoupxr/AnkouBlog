
using AnkouBlog.IServices;
using AnkouBlog.IRepository;
using AnkouBlog.Model.Models;
using AnkouBlog.Services.BASE;

namespace AnkouBlog.Services
{	
	/// <summary>
	/// OperateLogServices
	/// </summary>	
	public class OperateLogServices : BaseServices<OperateLog>, IOperateLogServices
    {
	
        IOperateLogRepository dal;
        public OperateLogServices(IOperateLogRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------OperateLog结束----------
	