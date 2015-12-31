using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lab.ngdemo.Helpers;

namespace lab.ngdemo.Models.CacheManagement
{
    public class UserCacheHelper
    {
        public CacheManager Cache { get; set; }
        private AppDbContext _db = new AppDbContext();
        List<User> _userList = new List<User>();
        public UserCacheHelper()
        {
            Cache = new CacheManager();
        }
        public List<User> GetUsers
        {
            get
            {
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

        public User AddUser(User user)
        {
            string appConstant = SiteConfigurationReader.GetAppSettingsString(Constants.CacheKey.DefaultCacheLifeTimeInMinute);
            string cacheKey = Constants.CacheKey.UserAdd + appConstant;
            if (!CacheManager.ICache.IsSet(cacheKey))
            {
                _userList.Add(user);

                string cacheKeyList = Constants.CacheKey.UserList + appConstant;
                if (!CacheManager.ICache.IsSet(cacheKeyList))
                {
                    CacheManager.ICache.Set(cacheKeyList, _userList);
                }
                else
                {
                    _userList = CacheManager.ICache.Get(cacheKeyList) as List<User>;
                }

                CacheManager.ICache.Set(cacheKey, user);
            }
            else
            {
                user = CacheManager.ICache.Get(cacheKey) as User;
            }
            return user;
        }

        public User EditUser(User user)
        {
            string appConstant = SiteConfigurationReader.GetAppSettingsString(Constants.CacheKey.DefaultCacheLifeTimeInMinute);
            string cacheKey = Constants.CacheKey.UserEdit + appConstant;
            if (!CacheManager.ICache.IsSet(cacheKey))
            {
                var userRemove = GetUsers.FirstOrDefault(item => item.UserName == user.UserName);
                GetUsers.Remove(userRemove);

                CacheManager.ICache.Set(cacheKey, user);
            }
            else
            {
                user = CacheManager.ICache.Get(cacheKey) as User;
            }
            return user;
        }

        public void DeleteUser(User user)
        {
            string appConstant = SiteConfigurationReader.GetAppSettingsString(Constants.CacheKey.DefaultCacheLifeTimeInMinute);
            string cacheKey = Constants.CacheKey.UserDelete + appConstant;
            if (!CacheManager.ICache.IsSet(cacheKey))
            {
                var userRemove = GetUsers.FirstOrDefault(item => item.UserName == user.UserName);
                GetUsers.Remove(userRemove);
                CacheManager.ICache.Remove(cacheKey);
            }
        }
    }
}