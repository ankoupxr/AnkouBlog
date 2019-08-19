using AnkouBlog.Common.DB;
using AnkouBlog.IRepository.BASE;
using AnkouBlog.Model;
using AnkouBlog.Repository.sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AnkouBlog.Repository.BASE
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private DbContext _context;
        private SqlSugarClient _db;
        private SimpleClient<TEntity> _entityDB;

        public DbContext Context
        {
            get { return _context; }
            set { _context = value; }
        }
        internal SqlSugarClient Db
        {
            get { return _db; }
            private set { _db = value; }
        }
        internal SimpleClient<TEntity> EntityDB
        {
            get { return _entityDB; }
            private set { _entityDB = value; }
        }
        public BaseRepository()
        {
            DbContext.Init(BaseDBConfig.ConnectionString);
            _context = DbContext.GetDbContext();
            _db = _context.Db;
            _entityDB = _context.GetEntityDB<TEntity>(_db);
        }
        public async Task<int> Add(TEntity model)
        {
            var i = await Task.Run(() => _db.Insertable(model).ExecuteReturnIdentity());
            return (int)i;
        }

        public async Task<bool> Delete(TEntity model)
        {
            var i = await Task.Run(() => _db.Deleteable(model).ExecuteCommand());
            return i > 0;
        }

        public async Task<bool> DeleteById(object id)
        {
            var i = await Task.Run(() => _db.Deleteable<TEntity>(id).ExecuteCommand());
            return i > 0;
        }

        public async Task<bool> DeleteByIds(object[] ids)
        {
            var i = await Task.Run(() => _db.Deleteable<TEntity>().In(ids).ExecuteCommand());
            return i > 0;
        }

        public async Task<List<TEntity>> Query()
        {
            return await Task.Run(() => _entityDB.GetList());
        }

        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
        }

        public async Task<List<TEntity>> Query(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => _entityDB.GetList(whereExpression));
        }

        public async Task<List<TEntity>> Query(System.Linq.Expressions.Expression<Func<TEntity, bool>>
            whereExpression, string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds),
                strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToList());

        }

        public async Task<List<TEntity>> Query(System.Linq.Expressions.Expression<Func<TEntity, bool>>
            whereExpression, System.Linq.Expressions.Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(orderByExpression != null,
                orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToList());
        }

        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds),
                strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
        }

        public async Task<List<TEntity>> Query(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(!string.
                IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToList());
        }

        public async Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(!string.
                IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Take(intTop).ToList());
        }

        public async Task<List<TEntity>> Query(System.Linq.Expressions.Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
            .WhereIF(whereExpression != null, whereExpression).ToPageList(intPageIndex, intPageSize));
        }

        public async Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds),
                strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageList(intPageIndex, intPageSize));
        }

        public async Task<TEntity> QueryByID(object objId)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().InSingle(objId));
        }

        public async Task<TEntity> QueryByID(object objId, bool blnUseCache = false)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().WithCacheIF(blnUseCache).InSingle(objId));
        }

        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().In(lstIds).ToList());
        }

        /// <summary>
        /// 分页查询[使用版本，其他分页未测试]
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        {

            RefAsync<int> totalCount = 0;
            var list = await _db.Queryable<TEntity>()
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .ToPageListAsync(intPageIndex, intPageSize, totalCount);

            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal())).ObjToInt();
            return new PageModel<TEntity>() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, PageSize = intPageSize, data = list };
        }

        public async Task<bool> Update(TEntity model)
        {
            var i = await Task.Run(() => _db.Updateable(model).ExecuteCommand());
            return i > 0;
        }

        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await Task.Run(() => _db.Updateable(entity).Where(strWhere).ExecuteCommand() > 0);
        }
        public async Task<bool> Update(string strSql, SugarParameter[] parameters = null)
        {
            return await Task.Run(() => _db.Ado.ExecuteCommand(strSql, parameters) > 0);
        }

        public async Task<bool> Update(
  TEntity entity,
  List<string> lstColumns = null,
  List<string> lstIgnoreColumns = null,
  string strWhere = ""
    )
        {
            IUpdateable<TEntity> up = _db.Updateable(entity);
            if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
            {
                up = up.IgnoreColumns(lstIgnoreColumns.ToArray());
            }
            if (lstColumns != null && lstColumns.Count > 0)
            {
                up = up.UpdateColumns(lstColumns.ToArray());
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                up = up.Where(strWhere);
            }
            return await up.ExecuteCommandHasChangeAsync();
        }
    }
}
