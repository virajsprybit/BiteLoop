namespace BiteLoop.Common
{
    using DAL;    
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class ContactUsSubjectBAL : ContactUsSubjectPAL
    {
        /// <summary>
        /// ContactUs Subject Operations Active/Deactive/Delete
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ObjOperation"></param>
        public void ContactUsSubjectOperation(string ID, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.VarChar, 500, ID), 
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)) 
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "ContactUsSubjectOperation", dbParam);
        }
      
        /// <summary>
        /// Get ContactUs Subject Details By ID
        /// </summary>
        /// <returns></returns>
        public DataTable GetContactUsSubjectByID()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "ContactUsSubjectList", dbParam);
        }
       
        /// <summary>
        /// ContactUs Subject list Paging
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
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "ContactUsSubjectList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[2].Value);
            TotalRecord = Convert.ToInt32(dbParam[4].Value);
            return table;
        }

        /// <summary>
        /// ContactUs Subject Details Save
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 4, base.ID), 
                new DbParameter("@Name", DbParameter.DbType.VarChar, 200, base.Name),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "ContactUsSubjectModify", dbParam);
            return Convert.ToInt16(dbParam[2].Value);
        }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public DataTable ContactUsSubjectListALL()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "ContactUsSubjectListAll");
        }
       
    }
}

