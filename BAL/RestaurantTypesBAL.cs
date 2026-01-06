namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class RestaurantTypesBAL : RestaurantTypesPAL
    {
        /// <summary>
        /// RestaurantTypes Operations Active/Deactive/Delete
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ObjOperation"></param>
        public void RestaurantTypesOperation(string ID, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.VarChar, 500, ID), 
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)) 
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "RestaurantTypesOperation", dbParam);
        }
      
        /// <summary>
        /// Get RestaurantTypes Details By ID
        /// </summary>
        /// <returns></returns>
        public DataTable GetRestaurantTypesByID()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "RestaurantTypesList", dbParam);
        }
       
        /// <summary>
        /// RestaurantTypes list Paging
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="RecordPerPage"></param>
        /// <param name="TotalRecord"></param>
        /// <param name="SortColumn"></param>
        /// <param name="SortType"></param>
        /// <returns></returns>
        public DataTable GetList(ref int CurrentPage, int RecordPerPage, out int TotalRecord, string SortColumn, string SortType)
        {
            DbParameter[] dbParam = new DbParameter[7];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID);
            dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 200, base.Name);
            dbParam[2] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int) CurrentPage);
            dbParam[2].ParamDirection = ParameterDirection.InputOutput;
            dbParam[3] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[4] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((SortColumn != string.Empty) && (SortType != string.Empty))
            {
                dbParam[5] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[6] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "RestaurantTypesList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[2].Value);
            TotalRecord = Convert.ToInt32(dbParam[4].Value);
            return table;
        }

        /// <summary>
        /// RestaurantTypes Details Save
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 4, base.ID), 
                new DbParameter("@Name", DbParameter.DbType.VarChar, 200, base.Name),
                new DbParameter("@IconUrl", DbParameter.DbType.VarChar, 500, base.IconUrl),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "RestaurantTypesModify", dbParam);
            return Convert.ToInt16(dbParam[2].Value);
        }
        public DataTable GetRestaurantTypes(int Status)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@Status", DbParameter.DbType.Int, 4, Status) };            
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "RestaurantTypesListALL", dbParam);
        }

      
    }
}

