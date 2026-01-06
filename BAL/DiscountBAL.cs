namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class DiscountBAL : DiscountPAL
    {      
       
        /// <summary>
        /// FoodItems list Paging
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="RecordPerPage"></param>
        /// <param name="TotalRecord"></param>
        /// <param name="SortColumn"></param>
        /// <param name="SortType"></param>
        /// <returns></returns>
        public DataTable GetList(ref int CurrentPage, int RecordPerPage, out int TotalRecord, string SortColumn, string SortType)
        {
            DbParameter[] dbParam = new DbParameter[6];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID);            
            dbParam[1] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int) CurrentPage);
            dbParam[1].ParamDirection = ParameterDirection.InputOutput;
            dbParam[2] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[3] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((SortColumn != string.Empty) && (SortType != string.Empty))
            {
                dbParam[4] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[5] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "DiscountList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[1].Value);
            TotalRecord = Convert.ToInt32(dbParam[3].Value);
            return table;
        }

      
    }
}

