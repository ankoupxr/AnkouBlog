using AnkouBlog.IServices;
using AnkouBlog.IRepository;
using AnkouBlog.Model.Models;
using AnkouBlog.Services.BASE;
using AnkouBlog.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using AnkouBlog.Model.ViewModels;
using AutoMapper;
using System;

namespace AnkouBlog.Services
{	
	/// <summary>
	/// BlogArticleServices
	/// </summary>	
	public class BlogArticleServices : BaseServices<BlogArticle>, IBlogArticleServices
    {
	
        IBlogArticleRepository _dal;
        IMapper _mapper;
        public BlogArticleServices(IBlogArticleRepository dal, IMapper mapper)
        {
            this._dal = dal;
            base.BaseDal = dal;
            this._mapper = mapper;
        }
        /// <summary>
        /// ��ȡ��ͼ����������Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BlogViewModels> GetBlogDetails(int id)
        {
            var bloglist = await base.Query(a => a.bID > 0, a => a.bID);
            var blogArticle = (await base.Query(a => a.bID == id)).FirstOrDefault();

            BlogViewModels models = null;

            if (blogArticle != null)
            {
                BlogArticle prevblog;
                BlogArticle nextblog;


                int blogIndex = bloglist.FindIndex(item => item.bID == id);
                if (blogIndex >= 0)
                {
                    try
                    {
                        prevblog = blogIndex > 0 ? (((BlogArticle)(bloglist[blogIndex - 1]))) : null;
                        nextblog = blogIndex + 1 < bloglist.Count() ? (BlogArticle)(bloglist[blogIndex + 1]) : null;


                        // ע���������,mapper
                        models = _mapper.Map<BlogViewModels>(blogArticle);

                        if (nextblog != null)
                        {
                            models.next = nextblog.btitle;
                            models.nextID = nextblog.bID;
                        }

                        if (prevblog != null)
                        {
                            models.previous = prevblog.btitle;
                            models.previousID = prevblog.bID;
                        }

                    }
                    catch (Exception) { }
                }


                blogArticle.btraffic += 1;
                await base.Update(blogArticle, new List<string> { "btraffic" });
            }

            return models;

        }
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10)]
        public async Task<List<BlogArticle>> GetBlogs()
        {
            var bloglist = await base.Query(a => a.bID > 0, a => a.bID);

            return bloglist;

        }
    }
}