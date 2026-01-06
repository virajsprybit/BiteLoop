using DAL;
using System;
using System.Data;
using System.Runtime.InteropServices;
using Utility;

namespace BiteLoop.Common
{
    public class CommonBAL
    {
        /// <summary>
        /// State List
        /// </summary>
        /// <returns></returns>
        public DataTable StateList()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "StateList");
        }

        public long BusinessHolidaySave(long BusinessID, DateTime Date, string Title)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID), 
                new DbParameter("@Date", DbParameter.DbType.DateTime, 200, Date),
                new DbParameter("@Title", DbParameter.DbType.VarChar, 1000, Title),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessHolidaySave", dbParam);
            return Convert.ToInt64(dbParam[3].Value);
        }
        public DataTable BusinessHolidayList(int Year, long BusinessID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID),                 
                new DbParameter("@Year", DbParameter.DbType.Int, 4, Year)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessHolidayList", dbParam);
        }

        //public long BusinessHolidaySave_Webservice(long BusinessID, DateTime Date, string Title)
        //{
        //    DbParameter[] dbParam = new DbParameter[] { 
        //        new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID), 
        //        new DbParameter("@Date", DbParameter.DbType.DateTime, 200, Date),
        //        new DbParameter("@Title", DbParameter.DbType.VarChar, 1000, Title),
        //        new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
        //    };
        //    DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessHolidaySave_Webservice", dbParam);
        //    return Convert.ToInt64(dbParam[3].Value);
        //}
        public long BusinessHolidayRemove_Webservice(long BusinessID, DateTime Date)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID), 
                new DbParameter("@Date", DbParameter.DbType.DateTime, 200, Date),                
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessHolidayRemove_Webservice", dbParam);
            return Convert.ToInt64(dbParam[2].Value);
        }

        public long UserForgotResetPassword(string Email, string TempPassword, string NewPassword)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@Email", DbParameter.DbType.VarChar, 400, Email), 
                new DbParameter("@TempPassword", DbParameter.DbType.VarChar, 400, TempPassword), 
                new DbParameter("@NewPassword", DbParameter.DbType.VarChar, 200, NewPassword),                
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserForgotResetPassword", dbParam);
            return Convert.ToInt64(dbParam[3].Value);
        }

        public DataTable StateHolidayList(int Year, int StateID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@StateID", DbParameter.DbType.Int, 40, StateID),                 
                new DbParameter("@Year", DbParameter.DbType.Int, 4, Year)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "StateHolidayList", dbParam);
        }

        public long StateHolidaySave(int StateID, DateTime Date, string Title)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@StateID", DbParameter.DbType.Int, 40, StateID), 
                new DbParameter("@Date", DbParameter.DbType.DateTime, 200, Date),
                new DbParameter("@Title", DbParameter.DbType.VarChar, 1000, Title),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "StateHolidaySave", dbParam);
            return Convert.ToInt64(dbParam[3].Value);
        }

        public DataTable StateHolidayList_Webservice(int StateID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@StateID", DbParameter.DbType.Int, 40, StateID)                
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "StateHolidayList_Webservice", dbParam);
        }

        public void BusinessHolidaySave_Webservice(long BusinessID, int StateHolidayID, int OnOff)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID), 
                new DbParameter("@StateHolidayID", DbParameter.DbType.Int, 40, StateHolidayID),
                new DbParameter("@OnOff", DbParameter.DbType.Int, 4, OnOff),                
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessHolidaySave_Webservice", dbParam);            
        }
        public void BusinessCustomHolidaySave_Webservice(long ID, long BusinessID, DateTime HolidayFromDate, DateTime HolidayToDate, string Title)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 40, ID), 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID), 
                new DbParameter("@HolidayFromDate", DbParameter.DbType.DateTime, 40, HolidayFromDate),
                new DbParameter("@HolidayToDate", DbParameter.DbType.DateTime, 40, HolidayToDate),
                new DbParameter("@Title", DbParameter.DbType.VarChar, 4000, Title),                
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessCustomHolidaySave_Webservice", dbParam);
        }

        public DataTable BusinessCustomHolidayList_Webservice(long BusinessID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID),
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessCustomHolidayList_Webservice", dbParam);
        }
        public DataTable BusinessPublicHolidayList_Webservice(long BusinessID, int StateID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID),
                   new DbParameter("@StateID", DbParameter.DbType.Int, 40, StateID)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessPublicHolidayList_Webservice", dbParam);
        }
        public void BusinessCustomHolidayDelete_Webservice(long ID, long BusinessID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 40, ID), 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID)                
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessCustomHolidayDelete_Webservice", dbParam);
        }

        public DataSet DashboardReportStateWise(int Year, string StateName)
        {
            DbParameter[] dbParam = new DbParameter[] 
            { 
                new DbParameter("@Year", DbParameter.DbType.Int, 400, Year),
                new DbParameter("@StateName", DbParameter.DbType.VarChar, 400, StateName)
             };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "DashboardReport", dbParam);

        }
        public DataTable AdminDashboard(string StateName)
        {
            DbParameter[] dbParam = new DbParameter[] 
            {               
                new DbParameter("@StateName", DbParameter.DbType.VarChar, 400, StateName)
             };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminDashBoard", dbParam);

        }
        public DataTable SuburbListByStateCode(string StateName)
        {
            DbParameter[] dbParam = new DbParameter[] 
            {               
                new DbParameter("@StateName", DbParameter.DbType.VarChar, 400, StateName)
             };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "SuburbListByStateCode", dbParam);

        }
        public DataTable AdminDashBoardStateReport()
        {            
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminDashBoardStateReport");
        }
        public DataTable ProfilePhotoListALL()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "ProfilePhotoListALL");
        }
        public DataTable GetBusinessOrUsers(int IsBusiness)
        {
            DbParameter[] dbParam = new DbParameter[] 
            {               
                new DbParameter("@IsBusiness", DbParameter.DbType.Int, 4, IsBusiness)
             };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GetBusinessOrUsers", dbParam);

        }
    }
}
