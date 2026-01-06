using DAL;
using PAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Utility;

namespace BAL
{
    public class IssuesBAL : IssuesPAL
    {
        public DataTable GetList(ref int intCurrentPage, int intRecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType)
        {
            DbParameter[] dbParam = new DbParameter[8];
            dbParam[0] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, 10);
            dbParam[1] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 4, 1);
            dbParam[2] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 4, intRecordPerPage);
            dbParam[3] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, strSortType);
            dbParam[4] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, "ID");
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "IssuesList", dbParam);
            intCurrentPage = Convert.ToInt32(dbParam[3].Value);
            intTotalRecord = 100;
            return table;
        }
        public void Operation(string strConatctID, string ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.VarChar, 20, strConatctID),
                new DbParameter("@OprType", DbParameter.DbType.VarChar, 20, ObjOperation)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "IssuesOperation", dbParam);
        }

        //public DataSet BusinessUsersDropdown()
        //{
        //    return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "BusinessUsersDropdown");
        //}

    }
}
