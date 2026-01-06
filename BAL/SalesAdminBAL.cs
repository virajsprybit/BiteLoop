namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class SalesAdminBAL : SalesAdminPAL
    {
        
        public DataTable GetByID()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID)
                };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "SalesAdminGetList", dbParam);
        }

        public DataTable GetList(ref int CurrentPage, int RecordPerPage, out int TotalRecord, string SortColumn, string SortType)
        {
            DbParameter[] dbParam = new DbParameter[9];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 10, base.ID);
            dbParam[1] = new DbParameter("@FirstName", DbParameter.DbType.VarChar, 50, base.FirstName);
            dbParam[2] = new DbParameter("@LastName", DbParameter.DbType.VarChar, 50, "");            
            dbParam[3] = new DbParameter("@EmailID", DbParameter.DbType.VarChar, 100, base.EmailID);
            dbParam[4] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int) CurrentPage);
            dbParam[4].ParamDirection = ParameterDirection.InputOutput;
            dbParam[5] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[6] = new DbParameter("@TotalAdmin", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((SortColumn != string.Empty) && (SortType != string.Empty))
            {
                dbParam[7] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[8] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }            
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "SalesAdminGetList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[4].Value);
            TotalRecord = Convert.ToInt32(dbParam[6].Value);
            return table;
        }
      
        public void Operation(string Id, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.VarChar, 200, Id), 
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SalesAdminOperation", dbParam);
        }
     
        public int Save()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID), 
                new DbParameter("@FirstName", DbParameter.DbType.VarChar, 100, base.FirstName),                 
                new DbParameter("@EmailID", DbParameter.DbType.VarChar, 500, base.EmailID), 
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, base.Password),                
                new DbParameter("@Phone", DbParameter.DbType.VarChar, 20, base.Phone),                
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)                 
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SalesAdminSave", dbParam);
            return Convert.ToInt32(dbParam[5].Value);
        }

        public DataTable SalesAdminLoginCheck()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@Email", DbParameter.DbType.VarChar, 200, EmailID),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 200, Password)
                };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "SalesAdminLoginCheck", dbParam);
        }
        public DataTable BusinessListAll()
        {                      
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessistAll");
        }
        public DataSet BusinessDetailsByID()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 200, ID)                
                };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "BusinessistDetailsByID", dbParam);
        }
        public DataTable BusinessMonthlyReport(DateTime FromDate, DateTime ToDate)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 200, ID),                
                new DbParameter("@FromDate", DbParameter.DbType.DateTime, 200, FromDate),
                new DbParameter("@ToDate", DbParameter.DbType.DateTime, 200, ToDate)                
                };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessMonthlyReport", dbParam);
        }
    }
}

