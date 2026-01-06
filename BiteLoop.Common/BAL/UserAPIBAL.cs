namespace BiteLoop.Common
{
    using DAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Security.AccessControl;
    using Utility;

    public class UserAPIBAL : UserAPIPAL
    {

        public long Save()
        {
            DbParameter[] dbParam = new DbParameter[16];

            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, ID);
            dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 500, Name);
            dbParam[2] = new DbParameter("@LastName", DbParameter.DbType.VarChar, 500, LastName);
            dbParam[3] = new DbParameter("@Mobile", DbParameter.DbType.VarChar, 50, Mobile);
            dbParam[4] = new DbParameter("@Email", DbParameter.DbType.VarChar, 500, Email);
            dbParam[5] = new DbParameter("@Password", DbParameter.DbType.VarChar, 500, Password);
            dbParam[6] = new DbParameter("@PostCode", DbParameter.DbType.VarChar, 20, PostCode);
            dbParam[7] = new DbParameter("@DeviceType", DbParameter.DbType.VarChar, 2, DeviceType);
            dbParam[8] = new DbParameter("@SocialMediaKey", DbParameter.DbType.VarChar, 8000, SocialMediaKey);
            dbParam[9] = new DbParameter("@OTP", DbParameter.DbType.VarChar, 50, OTP);
            dbParam[10] = new DbParameter("@FirebaseKey", DbParameter.DbType.VarChar, 200, FirebaseKey);
            //dbParam[10] = new DbParameter("@AppType", DbParameter.DbType.VarChar, 50, AppType);
            dbParam[11] = new DbParameter("@IsFacebookGoogleApple", DbParameter.DbType.Int, 2, IsFacebookGoogleApple);
            dbParam[12] = new DbParameter("@VersionNumber", DbParameter.DbType.VarChar, 50, VersionNumber);
            dbParam[13] = new DbParameter("@DeviceKey", DbParameter.DbType.VarChar, 200, DeviceKey);
            dbParam[14] = new DbParameter("@AuthToken", DbParameter.DbType.VarChar, 500, AuthToken);
            dbParam[15] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output);

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserSave", dbParam);
            return Convert.ToInt64(dbParam[15].Value);
        }

        public void CheckEmailMobileExist(string email, string mobile, out bool emailExists, out bool mobileExists)
        {
            DbParameter[] dbParam = new DbParameter[4];
            dbParam[0] = new DbParameter("@Email", DbParameter.DbType.VarChar, 500, email);
            dbParam[1] = new DbParameter("@Mobile", DbParameter.DbType.VarChar, 20, mobile);
            dbParam[2] = new DbParameter("@ExistsEmail", DbParameter.DbType.Bit, 1, ParameterDirection.Output);
            dbParam[3] = new DbParameter("@ExistsMobile", DbParameter.DbType.Bit, 1, ParameterDirection.Output);

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "CheckEmailMobileExistence", dbParam);

            emailExists = Convert.ToBoolean(dbParam[2].Value);
            mobileExists = Convert.ToBoolean(dbParam[3].Value);
        }



        //public long Save()
        //{
        //    DbParameter[] dbParam = new DbParameter[12];
        //    dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, ID);
        //    dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 500, Name);
        //    dbParam[2] = new DbParameter("@Email", DbParameter.DbType.VarChar, 500, Email);
        //    dbParam[3] = new DbParameter("@Password", DbParameter.DbType.VarChar, 500, Password);
        //    dbParam[4] = new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, Mobile);
        //    dbParam[5] = new DbParameter("@DeviceType", DbParameter.DbType.VarChar, 2, DeviceType);
        //    dbParam[6] = new DbParameter("@FacebookID", DbParameter.DbType.VarChar, 8000, FacebookID);
        //    dbParam[7] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output);
        //    dbParam[8] = new DbParameter("@LastName", DbParameter.DbType.VarChar, 500, LastName);
        //    dbParam[9] = new DbParameter("@State", DbParameter.DbType.Int, 5, State);
        //    dbParam[10] = new DbParameter("@OTP", DbParameter.DbType.VarChar, 50, OTP);
        //    dbParam[11] = new DbParameter("@PostCode", DbParameter.DbType.VarChar, 20, PostCode);

        //    DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserSave", dbParam);
        //    return Convert.ToInt64(dbParam[7].Value);


        public long UserProfileUpdate()
        {
            DbParameter[] dbParam = new DbParameter[10];

            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, ID);
            dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 500, Name ?? "");
            dbParam[2] = new DbParameter("@LastName", DbParameter.DbType.VarChar, 500, LastName ?? "");
            dbParam[3] = new DbParameter("@Email", DbParameter.DbType.VarChar, 500, Email ?? "");
            dbParam[4] = new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, Mobile ?? "");
            dbParam[5] = new DbParameter("@PostCode", DbParameter.DbType.VarChar, 20, PostCode ?? "");

            dbParam[6] = new DbParameter(
                "@Password",
                DbParameter.DbType.VarChar,
                200,
                string.IsNullOrEmpty(Password) ? (object)DBNull.Value : Password
            );

            dbParam[7] = new DbParameter(
                "@ProfilePhoto",
                DbParameter.DbType.VarChar,
                500,
                string.IsNullOrEmpty(ProfilePhoto) ? (object)DBNull.Value : ProfilePhoto
            );

            dbParam[8] = new DbParameter("@OTP", DbParameter.DbType.VarChar, 20, OTP ?? "");
            dbParam[9] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 20, ParameterDirection.Output);

            DbConnectionDAL.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "UserProfileUpdate",
                dbParam
            );

            return Convert.ToInt64(dbParam[9].Value);
        }



        //public long UserProfileUpdate()
        //{
        //    DbParameter[] dbParam = new DbParameter[9];
        //    dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, ID);
        //    dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 500, Name);
        //    dbParam[2] = new DbParameter("@Email", DbParameter.DbType.VarChar, 500, Email);
        //    //dbParam[3] = new DbParameter("@BirthDate", DbParameter.DbType.DateTime, 500, BirthDate);
        //    dbParam[3] = new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, Mobile);
        //    //dbParam[5] = new DbParameter("@Gender", DbParameter.DbType.VarChar, 500, Gender);
        //    //dbParam[6] = new DbParameter("@StreetAddress", DbParameter.DbType.VarChar, 500, StreetAddress);
        //    //dbParam[7] = new DbParameter("@Location", DbParameter.DbType.VarChar, 500, Location);
        //    dbParam[4] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 20, ParameterDirection.Output);
        //    //dbParam[9] = new DbParameter("@LastName", DbParameter.DbType.VarChar, 500, LastName);
        //    //dbParam[10] = new DbParameter("@State", DbParameter.DbType.Int, 5, State);
        //    dbParam[5] = new DbParameter("@PostCode", DbParameter.DbType.VarChar, 20, PostCode);
        //    //dbParam[6] = new DbParameter("@Password", DbParameter.DbType.VarChar, 200, Password);
        //    if (Password != null) // only send when normal user
        //    {
        //        dbParam[6] = new DbParameter("@Password", DbParameter.DbType.VarChar, 200, Password);
        //    }
        //    else
        //    {
        //        dbParam[6] = new DbParameter("@Password", DbParameter.DbType.VarChar, 200, DBNull.Value);
        //    }
        //    dbParam[7] = new DbParameter("@ProfilePhoto", DbParameter.DbType.VarChar, 500, ProfilePhoto);

        //    dbParam[8] = new DbParameter("@OTP", DbParameter.DbType.VarChar, 20, OTP);


        //    DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserProfileUpdate", dbParam);
        //    return Convert.ToInt64(dbParam[4].Value);
        //}

        public static int GetLoginType(long userId)
        {
            DbParameter[] param = new DbParameter[1];
            param[0] = new DbParameter("@UserID", DbParameter.DbType.Int, 20, userId);

            object result = DbConnectionDAL.GetScalarValue(
                CommandType.StoredProcedure,
                "GetLoginTypeByUserID",
                param
            );

            return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
        }

        public static string GetEmailByID(long userId)
        {
            DbParameter[] param = new DbParameter[1];
            param[0] = new DbParameter("@UserID", DbParameter.DbType.Int, 20, userId);

            object result = DbConnectionDAL.GetScalarValue(
                CommandType.StoredProcedure,
                "GetEmailByID",
                param
            );

            return result == null || result == DBNull.Value
                ? ""
                : Convert.ToString(result);
        }

        public static bool IsValidOTP(long userId, string otp)
        {
            DbParameter[] param = new DbParameter[2];
            param[0] = new DbParameter("@UserID", DbParameter.DbType.Int, 20, userId);
            param[1] = new DbParameter("@OTP", DbParameter.DbType.VarChar, 20, otp);

            object result = DbConnectionDAL.GetScalarValue(
                CommandType.StoredProcedure,
                "IsValidOTP",
                param
            );

            int val = (result == null || result == DBNull.Value)
                ? 0
                : Convert.ToInt32(result);

            return val > 0;
        }

        public static bool IsDuplicateEmailForEmailChange(string email, long userId)
        {
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = new DbParameter("@Email", DbParameter.DbType.VarChar, 500, email);
            dbParam[1] = new DbParameter("@UserID", DbParameter.DbType.Int, 20, userId);

            object result = DbConnectionDAL.GetScalarValue(
                CommandType.StoredProcedure,
                "CheckDuplicateEmailForEmailChange",
                dbParam
            );

            int val = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
            return val > 0;
        }

        public static void SaveOTPForEmailChange(long userId, string otp)
        {
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = new DbParameter("@UserID", DbParameter.DbType.Int, 20, userId);
            dbParam[1] = new DbParameter("@OTP", DbParameter.DbType.VarChar, 20, otp);

            DbConnectionDAL.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "SaveOTPEmailChange",
                dbParam
            );
        }       
        public long UserCardSetDefault(long UserID, long CardID)
        {
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = new DbParameter("@UserID", DbParameter.DbType.Int, 20, UserID);
            dbParam[1] = new DbParameter("@CardID", DbParameter.DbType.Int, 500, CardID);
            dbParam[2] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 20, ParameterDirection.Output);
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserCardSetDefault", dbParam);
            return Convert.ToInt64(dbParam[2].Value);
        }
        public int GenerateOTP(string Mobile, string Email)
        {
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = new DbParameter("@Mobile", DbParameter.DbType.VarChar, 50, Mobile);
            dbParam[1] = new DbParameter("@Email", DbParameter.DbType.VarChar, 500, Email);           
            dbParam[2] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output);
            
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "GenerateOTP", dbParam);
            return Convert.ToInt32(dbParam[2].Value);
        }
        public DataTable UserReferCode(long intID)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 50, intID);

            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserReferCode", dbParam);
            
        }
        public DataTable UserReferralListForCheckOut(long intID)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = new DbParameter("@UserID", DbParameter.DbType.Int, 50, intID);

            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserReferralList", dbParam);

        }
        public DataTable UserPromoCodeList(long intID)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = new DbParameter("@UserID", DbParameter.DbType.Int, 50, intID);

            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserPromocodeList", dbParam);

        }
        public DataTable UserPromoCodeListForCheckOut(long intID, long intBusinessID)
        {
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = new DbParameter("@UserID", DbParameter.DbType.Int, 50, intID);
            dbParam[1] = new DbParameter("@BusinessID", DbParameter.DbType.Int, 50, intBusinessID);

            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserPromocodeListForCheckOut", dbParam);

        }
        public long UserMobileUpdate(string OTP)
        {
            DbParameter[] dbParam = new DbParameter[4];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, ID);
            dbParam[1] = new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, Mobile);
            dbParam[2] = new DbParameter("@OTP", DbParameter.DbType.VarChar, 50, OTP);
            dbParam[3] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 20, ParameterDirection.Output);            

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserMobileUpdate", dbParam);
            return Convert.ToInt64(dbParam[3].Value);
        }
    }
}