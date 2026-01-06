namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class NotificationsBAL : NotificationsPAL
    {
        /// <summary>
        /// Get Contact Us Details by ID
        /// </summary>
        /// <returns></returns>
        public DataSet NotificationsUsersVendors()
        {
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "NotificationsUsersVendors");
        }
        public DataTable GetByID()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID)
                };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "NotificationByID", dbParam);
        }
        public DataTable GetList(ref int intCurrentPage, int intRecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType)
        {
            DbParameter[] dbParam = new DbParameter[6];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID);
            dbParam[1] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)intCurrentPage);
            dbParam[1].ParamDirection = ParameterDirection.InputOutput;
            dbParam[2] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, intRecordPerPage);
            dbParam[3] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((strSortColumn != string.Empty) && (strSortType != string.Empty))
            {
                dbParam[4] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 45, strSortType);
                dbParam[5] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 45, strSortColumn);
            }
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "NotificationsList", dbParam);
            intCurrentPage = Convert.ToInt32(dbParam[1].Value);
            intTotalRecord = Convert.ToInt32(dbParam[3].Value);
            return table;
        }
        public void Operation(string strConatctID, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.VarChar, 0x7d0, strConatctID), 
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation))
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "NotificationsOperation", dbParam);
        }

        public long Save()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 40, base.ID), 
                new DbParameter("@Message", DbParameter.DbType.VarChar, 1000, Message), 
                new DbParameter("@Vendors", DbParameter.DbType.VarChar, 8000, Vendors),        
                new DbParameter("@Users", DbParameter.DbType.VarChar, 8000, Users),        
                new DbParameter("@SalesAdmin", DbParameter.DbType.VarChar, 8000, SalesAdmin),        
                new DbParameter("@IsSchedule", DbParameter.DbType.Int, 4, IsSchedule),
                new DbParameter("@ScheduleTime", DbParameter.DbType.DateTime, 8000, ScheduleTime),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 40, ParameterDirection.Output) 
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "NotificationModify", dbParam);
            return Convert.ToInt64(dbParam[7].Value);
        }
    }
}

