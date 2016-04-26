﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lab.ngdemo.Helpers;

namespace lab.ngdemo.Models.CacheManagement
{
    public class UserCacheHelper
    {
        public CacheManager Cache { get; set; }

        public UserCacheHelper()
        {
            Cache = new CacheManager();
        }
        
        public List<User> GetUsers
        {
            get
            {
                List<User> _userList = new List<User>();
                string appConstant = SiteConfigurationReader.GetAppSettingsString(Constants.CacheKey.DefaultCacheLifeTimeInMinute);
                string cacheKey = Constants.CacheKey.UserList + appConstant;
                if (!CacheManager.ICache.IsSet(cacheKey))
                {
                    CacheManager.ICache.Set(cacheKey, _userList);
                }
                else
                {
                    _userList = CacheManager.ICache.Get(cacheKey) as List<User>;
                }

                return _userList;
            }
        }

        public User GetUser(string userName)
        {
            var user = new User();
            string appConstant = SiteConfigurationReader.GetAppSettingsString(Constants.CacheKey.DefaultCacheLifeTimeInMinute);
            string cacheKey = Constants.CacheKey.User + appConstant;
            if (!CacheManager.ICache.IsSet(cacheKey))
            {
                user = GetUsers.FirstOrDefault(item => item.UserName == userName);
                CacheManager.ICache.Set(cacheKey, user);
            }
            else
            {
                user = CacheManager.ICache.Get(cacheKey) as User;
            }
            return user;
        }

        public void AddUser(User user)
        {
            string appConstant = SiteConfigurationReader.GetAppSettingsString(Constants.CacheKey.DefaultCacheLifeTimeInMinute);
            //string cacheKey = Constants.CacheKey.UserAdd + appConstant;

            List<User> _userList = new List<User>();
            _userList = GetUsers.ToList();
            _userList.Add(user);

            string cacheKeyList = Constants.CacheKey.UserList + appConstant;
            if (CacheManager.ICache.IsSet(cacheKeyList))
            {
                CacheManager.ICache.Remove(cacheKeyList);
                CacheManager.ICache.Set(cacheKeyList, _userList);
            }
            else
            {
                CacheManager.ICache.Set(cacheKeyList, _userList);
            }

        }

        public void EditUser(User user)
        {
            string appConstant = SiteConfigurationReader.GetAppSettingsString(Constants.CacheKey.DefaultCacheLifeTimeInMinute);
            //string cacheKey = Constants.CacheKey.UserEdit + appConstant;

            List<User> _userList = new List<User>();
            _userList = GetUsers.ToList();
            var editUser = _userList.FirstOrDefault(item => item.UserName == user.UserName);
            editUser = user;

            string cacheKeyList = Constants.CacheKey.UserList + appConstant;
            if (CacheManager.ICache.IsSet(cacheKeyList))
            {
                CacheManager.ICache.Remove(cacheKeyList);
                CacheManager.ICache.Set(cacheKeyList, _userList);
            }
            else
            {
                CacheManager.ICache.Set(cacheKeyList, _userList);
            }

        }

        public void DeleteUser(User user)
        {
            string appConstant = SiteConfigurationReader.GetAppSettingsString(Constants.CacheKey.DefaultCacheLifeTimeInMinute);
            //string cacheKey = Constants.CacheKey.UserDelete + appConstant;

            List<User> _userList = new List<User>();
            _userList = GetUsers.ToList();
            var userRemove = _userList.FirstOrDefault(item => item.UserName == user.UserName);
            _userList.Remove(userRemove);

            string cacheKeyList = Constants.CacheKey.UserList + appConstant;
            if (CacheManager.ICache.IsSet(cacheKeyList))
            {
                CacheManager.ICache.Remove(cacheKeyList);
                CacheManager.ICache.Set(cacheKeyList, _userList);
            }
            else
            {
                CacheManager.ICache.Set(cacheKeyList, _userList);
            }

        }
    }
}