namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class DonationsBAL : DonationsPAL
    {
        /// <summary>
        /// Donations Operations Active/Deactive/Delete
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ObjOperation"></param>
        public void DonationsOperation(string ID, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.VarChar, 500, ID), 
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)) 
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "DonationsOperation", dbParam);
        }
      
        /// <summary>
        /// Get Donations Details By ID
        /// </summary>
        /// <returns></returns>
        public DataTable GetDonationsByID()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "DonationsList", dbParam);
        }
       
        /// <summary>
        /// Donations list Paging
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
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "DonationsList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[1].Value);
            TotalRecord = Convert.ToInt32(dbParam[3].Value);
            return table;
        }

        /// <summary>
        /// Donations Details Save
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 4, base.ID), 
                new DbParameter("@Donation", DbParameter.DbType.Decimal, 200, Donation),                
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "DonationsModify", dbParam);
            return Convert.ToInt16(dbParam[2].Value);
        }

        public void UpdateDonationsSequence(string ID, string sequenceNo)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.VarChar, 0x3e8, ID), new DbParameter("@SequenceNo", DbParameter.DbType.VarChar, 0x3e8, sequenceNo) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateDonationsSequence", dbParam);
        }
    }
}

