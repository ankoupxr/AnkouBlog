using AnkouBlog.IServices;
using AnkouBlog.IRepository;
using AnkouBlog.Model.Models;
using AnkouBlog.Services.BASE;

namespace AnkouBlog.Services
{	
	/// <summary>
	/// GuestbookServices
	/// </summary>	
	public class GuestbookServices : BaseServices<Guestbook>, IGuestbookServices
    {
	
        IGuestbookRepository dal;
        public GuestbookServices(IGuestbookRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------Guestbook结束----------
	