namespace BAL
{
    using DAL;
    using Microsoft.ApplicationBlocks.Data;
    using PAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using Utility;

    public class UsersBAL : UsersPAL
    {
        public DataTable GetList(ref int intCurrentPage, int intRecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType)
        {
            DbParameter[] dbParam = new DbParameter[8];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID);
            dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 100, base.Name);
            dbParam[2] = new DbParameter("@Email", DbParameter.DbType.VarChar, 100, base.Email);
            dbParam[3] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)intCurrentPage);
            dbParam[3].ParamDirection = ParameterDirection.InputOutput;
            dbParam[4] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, intRecordPerPage);
            dbParam[5] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((strSortColumn != string.Empty) && (strSortType != string.Empty))
            {
                dbParam[6] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, strSortType);
                dbParam[7] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, strSortColumn);
            }
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UsersList", dbParam);
            intCurrentPage = Convert.ToInt32(dbParam[3].Value);
            intTotalRecord = Convert.ToInt32(dbParam[5].Value);
            return table;
        }
        public void Operation(string strConatctID, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.VarChar, 0x7d0, strConatctID), 
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation))
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UsersOperation", dbParam);
        }
        public long Save()
        {
            DbParameter[] dbParam = new DbParameter[8];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, ID);
            dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 500, Name);            
            dbParam[2] = new DbParameter("@Email", DbParameter.DbType.VarChar, 500, Email);
            dbParam[3] = new DbParameter("@Password", DbParameter.DbType.VarChar, 500, Password);
            dbParam[4] = new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, Mobile);
            dbParam[5] = new DbParameter("@DeviceType", DbParameter.DbType.VarChar, 2, DeviceType);
            dbParam[6] = new DbParameter("@FacebookID", DbParameter.DbType.VarChar, 8000, FacebookID);
            dbParam[7] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output);            

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserSave", dbParam);
            return Convert.ToInt64(dbParam[7].Value);
        }

        public DataTable User_LoginCheck(out int intResult, string strDeviceKey, string strDeviceType)
        {
            DbParameter[] dbParam = new DbParameter[9];
            dbParam[0] = new DbParameter("@UserName", DbParameter.DbType.VarChar, 500, Email);
            dbParam[1] = new DbParameter("@Password", DbParameter.DbType.VarChar, 500, Password);
            dbParam[2] = new DbParameter("@DeviceKey", DbParameter.DbType.VarChar, 4000, strDeviceKey);
            dbParam[3] = new DbParameter("@DeviceType", DbParameter.DbType.VarChar, 4000, strDeviceType);
            dbParam[4] = new DbParameter("@VersionNumber", DbParameter.DbType.VarChar, 50, VersionNumber);
            dbParam[5] = new DbParameter("@FirebaseKey", DbParameter.DbType.VarChar, 1000, FirebaseKey);
            dbParam[6] = new DbParameter("@IsFacebookGoogleApple", DbParameter.DbType.Int, 4, IsFacebookGoogleApple);
            dbParam[7] = new DbParameter("@SocialMediaKey", DbParameter.DbType.VarChar, 1000, SocialMediaKey);
            dbParam[8] = new DbParameter("@IsError", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            
            DataTable dtblUserData = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "User_LoginCheck", dbParam);
            intResult = Convert.ToInt32(dbParam[8].Value);
            return dtblUserData;
        }
        public DataTable UserDetailsByID()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 200, ID)                
                };            
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserDetailsByID", dbParam);
        }
        public DataTable UserFavouriteBusinessList()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@UserID", DbParameter.DbType.Int, 200, ID)                
                };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserFavouriteBusinessList", dbParam);
        }
        public long UserProfileUpdate()
        {
            DbParameter[] dbParam = new DbParameter[9];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, ID);
            dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 500, Name);
            dbParam[2] = new DbParameter("@Email", DbParameter.DbType.VarChar, 500, Email);
            dbParam[3] = new DbParameter("@BirthDate", DbParameter.DbType.DateTime, 500, BirthDate);
            dbParam[4] = new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, Mobile);
            dbParam[5] = new DbParameter("@Gender", DbParameter.DbType.VarChar, 500, Gender);
            dbParam[6] = new DbParameter("@StreetAddress", DbParameter.DbType.VarChar, 500, StreetAddress);
            dbParam[7] = new DbParameter("@Location", DbParameter.DbType.VarChar, 500, Location);
            dbParam[8] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output);

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserProfileUpdate", dbParam);
            return Convert.ToInt64(dbParam[8].Value);
        }

        public int Changepassword(string OldPassword)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 10, ID), 
                new DbParameter("@Password", DbParameter.DbType.VarChar, 50, OldPassword),
                new DbParameter("@NewPassword", DbParameter.DbType.VarChar, 50, Password), 
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 10, ParameterDirection.Output) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserChangePassword", dbParam);
            return Convert.ToInt32(dbParam[3].Value);
        }
        public DataSet BusinessUsersDropdown()
        {
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "BusinessUsersDropdown");
        }
        public DataTable UserForgotPassword()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@EmailID", DbParameter.DbType.VarChar, 200, Email)                
                };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserForgotPassword", dbParam);
        }
        public void UserLogout()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 200, ID)                
                };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserLogout", dbParam);
        }

        public void UpdateUserForDelete()
        {
            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@ID", DbParameter.DbType.Int, 100, ID),
                new DbParameter("@Email", DbParameter.DbType.VarChar, 200, this.Email),
                new DbParameter("@Mobile", DbParameter.DbType.VarChar, 50, this.Mobile),
                new DbParameter("@SocialMediaKey", DbParameter.DbType.VarChar, 200, this.SocialMediaKey)
            };

            DbConnectionDAL.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "SP_DeleteUser",
                dbParam
            );
        }

        public DataTable FavouriteList(ref int intCurrentPage, int intRecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType, string Keyword)
        {
            DbParameter[] dbParam = new DbParameter[7];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID);            
            dbParam[1] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)intCurrentPage);
            dbParam[1].ParamDirection = ParameterDirection.InputOutput;
            dbParam[2] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, intRecordPerPage);
            dbParam[3] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((strSortColumn != string.Empty) && (strSortType != string.Empty))
            {
                dbParam[4] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, strSortType);
                dbParam[5] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 45, strSortColumn);
            }
            dbParam[6] = new DbParameter("@Keyword", DbParameter.DbType.VarChar, 4000, Keyword);
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "FavouriteList", dbParam);
            intCurrentPage = Convert.ToInt32(dbParam[1].Value);
            intTotalRecord = Convert.ToInt32(dbParam[3].Value);
            return table;
        }
        public DataTable UserCardList()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@UserID", DbParameter.DbType.Int, 200, ID)                
                };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserCardList", dbParam);
        }
        public DataTable UserImpactAndRewards()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 200, ID)                
                };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserImpactAndRewards", dbParam);
        }
        public void UpdateMobile(string strMobile)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 10, ID), 
                new DbParameter("@Mobile", DbParameter.DbType.VarChar, 50, strMobile)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserUpdateMobile", dbParam);
        }
    }
}

