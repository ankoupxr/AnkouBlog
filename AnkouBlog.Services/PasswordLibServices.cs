
using AnkouBlog.IServices;
using AnkouBlog.IRepository;
using AnkouBlog.Model.Models;
using AnkouBlog.Services.BASE;

namespace AnkouBlog.Services
{	
	/// <summary>
	/// PasswordLibServices
	/// </summary>	
	public class PasswordLibServices : BaseServices<PasswordLib>, IPasswordLibServices
    {
	
        IPasswordLibRepository dal;
        public PasswordLibServices(IPasswordLibRepository dal)
        {
            this.dal = dal;
            base.BaseDal = dal;
        }
       
    }
}

	//----------PasswordLib结束----------
	