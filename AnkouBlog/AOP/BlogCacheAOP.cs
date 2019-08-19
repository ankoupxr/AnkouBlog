using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnkouBlog.Common;
using AnkouBlog.Common.MemoryCache;
using Castle.DynamicProxy;

namespace AnkouBlog.AOP
{
    public class BlogCacheAOP : CacheAOPbase
    {
        private readonly ICaching _cache;
        public BlogCacheAOP(ICaching cache)
        {
            _cache = cache;
        }
        public override void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            //对当前方法的特性验证
            if(method.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(CachingAttribute)) is CachingAttribute qCachingAttribute)
            {
                //获取自定义缓存键
                var cacheKey = CustomCacheKey(invocation);
                //根据key获得相应的缓存值
                var cacheValue = _cache.Get(cacheKey);
                if(cacheValue != null)
                {
                    invocation.ReturnValue = cacheValue;
                    return;
                }
                //去执行当前的方法
                invocation.Proceed();
                //存入缓存
                if(!string.IsNullOrWhiteSpace(cacheKey))
                {
                    _cache.Set(cacheKey, invocation.ReturnValue);
                }
            }
            else
            {
                invocation.Proceed();//直接执行拦截方法
            }
        }
    }
}
