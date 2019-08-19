using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnkouBlog.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnkouBlog.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class UserRoleController : Controller
    {
        readonly IsysUserInfoServices _sysUserInfoServices;
        readonly IUserRoleServices _userRoleServices;
        readonly IRoleServices _roleServices;
        public UserRoleController(IsysUserInfoServices sysUserInfoServices,
            IUserRoleServices userRoleServices,IRoleServices roleServices)
        {
            _roleServices = roleServices;
            _sysUserInfoServices = sysUserInfoServices;
            _userRoleServices = userRoleServices;
        }
        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> AddUser(string loginName, string loginPwd)
        {
            var model = await _sysUserInfoServices.SaveUserInfo(loginName, loginPwd);
            return Ok(new
            {
                success = true,
                data = model
            });
        }

        /// <summary>
        /// 新建Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> AddRole(string roleName)
        {
            var model = await _roleServices.SaveRole(roleName);
            return Ok(new
            {
                success = true,
                data = model
            });
        }

        /// <summary>
        /// 新建用户角色关系
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> AddUserRole(int uid, int rid)
        {
            var model = await _userRoleServices.SaveUserRole(uid, rid);
            return Ok(new
            {
                success = true,
                data = model
            });
        }




    }
}