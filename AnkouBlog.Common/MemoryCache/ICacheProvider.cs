using System;
using System.Collections.Generic;
using System.Text;

namespace AnkouBlog.Common.MemoryCache
{
        public interface ICaching
        {
            object Get(string cacheKey);

            void Set(string cacheKey, object cacheValue);
        }
    }