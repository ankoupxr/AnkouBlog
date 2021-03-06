﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnkouBlog.AuthHelper.OverWrite;
using AnkouBlog.Common.Helper;
using AnkouBlog.Common.HttpContextUser;
using AnkouBlog.IServices;
using AnkouBlog.Model;
using AnkouBlog.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnkouBlog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IsysUserInfoServices _sysUserInfoServices;
        readonly IUserRoleServices _userRoleServices;
        readonly IRoleServices _roleServices;
        private readonly IUser _user;
        public UserController(IsysUserInfoServices sysUserInfoServices,IUserRoleServices userRoleServices,
            IRoleServices roleServices,IUser user)
        {
            _sysUserInfoServices = sysUserInfoServices;
            _roleServices = roleServices;
            _userRoleServices = userRoleServices;
            _user = user;
        }
        [HttpGet]
        [ResponseCache(Duration = 60)]
        public async Task<MessageModel<PageModel<sysUserInfo>>> Get(int page = 1,string key="")
        {
            if(string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            int intPageSize = 50;

            var data = await _sysUserInfoServices.QueryPage(a => a.tdIsDelete != true && a.uStatus >= 0 && ((a.uLoginName != null && a.uLoginName.Contains(key)) || (a.uRealName != null && a.uRealName.Contains(key))), page, intPageSize, " uID desc ");

            var allUserRoles = await _userRoleServices.Query(d => d.IsDeleted == false);
            var allRoles = await _roleServices.Query(d => d.IsDeleted == false);

            var sysUserInfos = data.data;
            foreach (var item in sysUserInfos)
            {
                item.RID = (allUserRoles.FirstOrDefault(d => d.UserId == item.uID)?.RoleId).ObjToInt();
                item.RoleName = allRoles.FirstOrDefault(d => d.Id == item.RID)?.Name;
            }

            data.data = sysUserInfos;

            return new MessageModel<PageModel<sysUserInfo>>()
            {
                msg = "获取成功",
                success = data.dataCount >= 0,
                response = data
            };
        }
        /// <summary>
        /// 获取用户详情根据token
        /// 【无权限】
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<sysUserInfo>> GetInfoByToken(string token)
        {
            var data = new MessageModel<sysUserInfo>();
            if(!string.IsNullOrEmpty(token))
            {
                var tokenModel = JwtHelper.SerializeJwt(token);
                if(tokenModel != null && tokenModel.Uid > 0)
                {
                    var userinfo = await _sysUserInfoServices.QueryByID(tokenModel.Uid);
                    if(userinfo != null)
                    {
                        data.response = userinfo;
                        data.success = true;
                        data.msg = "获取成功";
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="sysUserInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> Post([FromBody] sysUserInfo sysUserInfo)
        {
            var data = new MessageModel<string>();

            sysUserInfo.uLoginPWD = MD5Helper.MD5Encrypt32(sysUserInfo.uLoginPWD);
            sysUserInfo.uRemark = _user.Name;

            var id = await _sysUserInfoServices.Add(sysUserInfo);
            data.success = id > 0;
            if (data.success)
            {
                data.response = id.ObjToString();
                data.msg = "添加成功";
            }

            return data;
        }
        /// <summary>
        /// 更新用户与角色
        /// </summary>
        /// <param name="sysUserInfo"></param>
        /// <returns></returns>
        // PUT: api/User/5
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] sysUserInfo sysUserInfo)
        {
            // 这里也要做后期处理，会有用户个人中心的业务

            var data = new MessageModel<string>();
            if (sysUserInfo != null && sysUserInfo.uID > 0)
            {
                if (sysUserInfo.RID > 0)
                {
                    var usrerole = await _userRoleServices.Query(d => d.UserId == sysUserInfo.uID && d.RoleId == sysUserInfo.RID);
                    if (usrerole.Count == 0)
                    {
                        await _userRoleServices.Add(new UserRole(sysUserInfo.uID, sysUserInfo.RID));
                    }
                }

                data.success = await _sysUserInfoServices.Update(sysUserInfo);
                if (data.success)
                {
                    data.msg = "更新成功";
                    data.response = sysUserInfo?.uID.ObjToString();
                }
            }

            return data;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(int id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var userDetail = await _sysUserInfoServices.QueryByID(id);
                userDetail.tdIsDelete = true;
                data.success = await _sysUserInfoServices.Update(userDetail);
                if (data.success)
                {
                    data.msg = "删除成功";
                    data.response = userDetail?.uID.ObjToString();
                }
            }

            return data;
        }
    }
}