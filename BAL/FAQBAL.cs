namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class FAQBAL : FAQPAL
    {
        public int FAQModify()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID), new DbParameter("@Question", DbParameter.DbType.VarChar, 200, base.Question), new DbParameter("@Answer", DbParameter.DbType.VarChar, 0x1f40, base.Answer), new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "FAQModify", dbParam);
            return Convert.ToInt32(dbParam[3].Value);
        }

        public DataTable GetCurrentFAQListFront(ref int intCurrentPage, int intRecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType)
        {
            DbParameter[] dbParam = new DbParameter[8];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID);
            dbParam[1] = new DbParameter("@Question", DbParameter.DbType.VarChar, 100, base.Question);
            dbParam[2] = new DbParameter("@Answer", DbParameter.DbType.VarChar, 100, base.Answer);
            dbParam[3] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int) intCurrentPage);
            dbParam[4].ParamDirection = ParameterDirection.InputOutput;
            dbParam[5] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, intRecordPerPage);
            dbParam[5] = new DbParameter("@TotalRecords", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((strSortColumn != string.Empty) && (strSortType != string.Empty))
            {
                dbParam[6] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, strSortType);
                dbParam[7] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, strSortColumn);
            }
            dbParam[8] = new DbParameter("@Status", DbParameter.DbType.TinyInt, 10, base.Status);
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "FAQListFront", dbParam);
            intCurrentPage = Convert.ToInt32(dbParam[3].Value);
            intTotalRecord = Convert.ToInt32(dbParam[5].Value);
            return table;
        }

        public DataTable GetFAQList(ref int intCurrentPage, int intRecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType)
        {
            DbParameter[] dbParam = new DbParameter[9];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID);
            dbParam[1] = new DbParameter("@Question", DbParameter.DbType.VarChar, 100, base.Question);
            dbParam[2] = new DbParameter("@Answer", DbParameter.DbType.VarChar, 100, base.Answer);
            dbParam[3] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int) intCurrentPage);
            dbParam[3].ParamDirection = ParameterDirection.InputOutput;
            dbParam[4] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, intRecordPerPage);
            dbParam[5] = new DbParameter("@TotalRecords", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((strSortColumn != string.Empty) && (strSortType != string.Empty))
            {
                dbParam[6] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, strSortType);
                dbParam[7] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, strSortColumn);
            }
            dbParam[8] = new DbParameter("@Status", DbParameter.DbType.TinyInt, 10, base.Status);
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "FAQList", dbParam);
            intCurrentPage = Convert.ToInt32(dbParam[3].Value);
            intTotalRecord = Convert.ToInt32(dbParam[5].Value);
            return table;
        }

        public DataTable GetFAQListByID()
        {
            DataTable table = new DataTable();
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 10, base.ID) };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "FAQList", dbParam);
        }

        public DataTable GetFAQsListFront()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "FAQListAtFront");
        }

        public void Operation(string strConatctID, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.VarChar, 200, strConatctID), new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "FAQOperation", dbParam);
        }

        public void UpdateFAQSequence(string ID, string sequenceNo)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.VarChar, 0x3e8, ID), new DbParameter("@SequenceNo", DbParameter.DbType.VarChar, 0x3e8, sequenceNo) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateFAQSequence", dbParam);
        }
    }
}

