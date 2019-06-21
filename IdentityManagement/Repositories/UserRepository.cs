﻿using IdentityManagement.Data;
using IdentityManagement.Entities;
using System.Collections.Generic;

namespace IdentityManagement.Repositories
{
    public static class UserRepository
    {
        public static int CreateNewUser(ApplicationUser objUser)
        {
            List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = "USER_NAME", ParameterValue = objUser.UserName });
            parameters.Add(new ParameterInfo() { ParameterName = "FIRST_NAME", ParameterValue = objUser.FirstName });
            parameters.Add(new ParameterInfo() { ParameterName = "LAST_NAME", ParameterValue = objUser.LastName });
            parameters.Add(new ParameterInfo() { ParameterName = "MIDDLE_NAME", ParameterValue = objUser.MiddleName });
            parameters.Add(new ParameterInfo() { ParameterName = "EMAIL", ParameterValue = objUser.Email });
            parameters.Add(new ParameterInfo() { ParameterName = "PASSWORD", ParameterValue = objUser.Password });
            parameters.Add(new ParameterInfo() { ParameterName = "USER_STATUS", ParameterValue = objUser.UserStatus });
            int success = SqlHelper.ExecuteQuery("Identity_CreateNewUser", parameters);
            return success;
        }

        public static int DeleteUser(ApplicationUser objUser)
        {
            List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = "Id", ParameterValue = objUser.Id });
            int success = SqlHelper.ExecuteQuery("DeleteUser", parameters);
            return success;
        }

        public static ApplicationUser GetUser(string id)
        {
            List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = "USER_ID", ParameterValue = id });
            ApplicationUser oUser = SqlHelper.GetRecord<ApplicationUser>("Identity_GetUserProfile", parameters);
            return oUser;
        }

        public static IList<ApplicationUser> GetUsers()
        {
            IList<ApplicationUser> oUserList = SqlHelper.GetRecords<ApplicationUser>("Identity_GetUsers", null);
            return oUserList;
        }

        public static ApplicationUser GetUserByUsername(string userName)
        {
            List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = "USER_NAME", ParameterValue = userName });
            ApplicationUser oUser = SqlHelper.GetRecord<ApplicationUser>("Identity_GetUserProfileByUserName", parameters);
            return oUser;
        }

        public static int UpdateUser(ApplicationUser objUser)
        {
			List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = "USER_ID", ParameterValue = objUser.UserId });
            parameters.Add(new ParameterInfo() { ParameterName = "USER_NAME", ParameterValue = objUser.UserName });
            parameters.Add(new ParameterInfo() { ParameterName = "FIRST_NAME", ParameterValue = objUser.FirstName });
            parameters.Add(new ParameterInfo() { ParameterName = "LAST_NAME", ParameterValue = objUser.LastName });
            parameters.Add(new ParameterInfo() { ParameterName = "MIDDLE_NAME", ParameterValue = objUser.MiddleName });
            parameters.Add(new ParameterInfo() { ParameterName = "EMAIL", ParameterValue = objUser.Email });
			parameters.Add(new ParameterInfo() { ParameterName = "PASSWORD", ParameterValue = objUser.Password });
			parameters.Add(new ParameterInfo() { ParameterName = "USER_STATUS", ParameterValue = objUser.UserStatus });
			int success = SqlHelper.ExecuteQuery("Identity_UpdateUserProfile", parameters);
            return success;
        }
    }
}
