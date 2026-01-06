
using DAL;
using System;
using System.Data;

namespace BiteLoop.Common
{
    public class PromotionalCodeBAL : PromotionalCodePAL
    {        
        public DataTable GetByID()
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 2000, base.ID)
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "PromotionalCodeDetailsByID", dbParam);
        }
        public DataTable GetList(ref int CurrentPage, int RecordPerPage, out int TotalRecord, string SortColumn, string SortType)
        {
            DbParameter[] dbParam = new DbParameter[7];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 10, base.ID);
            dbParam[1] = new DbParameter("@CouponCode", DbParameter.DbType.VarChar, 50, base.CouponCode);
            dbParam[2] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)CurrentPage);
            dbParam[2].ParamDirection = ParameterDirection.InputOutput;
            dbParam[3] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[4] = new DbParameter("@TotalRecords", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((SortColumn != string.Empty) && (SortType != string.Empty))
            {
                dbParam[5] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[6] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "PromotionalCodeList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[2].Value);
            TotalRecord = Convert.ToInt32(dbParam[4].Value);
            return table;
        }
        public void Operation(string Id, Utility.Common.DataBaseOperation ObjOperation, long UpdatedUserID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.VarChar, 2000, Id),
                new DbParameter("@UpdatedUserID", DbParameter.DbType.Int, 2000, UpdatedUserID),
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "PromotionalCodeOperation", dbParam);
        }
        public int Save(string Business, string Users)
        {
            DbParameter[] dbParam = new DbParameter[12];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID);
            dbParam[1] = new DbParameter("@CouponCode", DbParameter.DbType.VarChar, 50, base.CouponCode);
            dbParam[2] = new DbParameter("@DiscountType", DbParameter.DbType.Int, 10, base.DiscountType);
            dbParam[3] = new DbParameter("@DiscountAmount", DbParameter.DbType.VarChar, 50, base.DiscountAmount);
            dbParam[4] = new DbParameter("@CouponStartTime", DbParameter.DbType.DateTime, 50, base.CouponStartTime);
            dbParam[5] = new DbParameter("@CouponEndTime", DbParameter.DbType.DateTime, 50, base.CouponEndTime);
            dbParam[6] = new DbParameter("@State", DbParameter.DbType.Int, 10, base.State);
            dbParam[7] = new DbParameter("@SingleUse", DbParameter.DbType.Int, 10, base.SingleUse);
            dbParam[8] = new DbParameter("@Business", DbParameter.DbType.VarChar, 1000000, Business);
            dbParam[9] = new DbParameter("@Users", DbParameter.DbType.VarChar, 10000000, Users);
            dbParam[10] = new DbParameter("@MinOrderAmount", DbParameter.DbType.VarChar, 100000, MinOrderAmount);
            dbParam[11] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "PromotionalCodeSave", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value); 
        }
        public int UserPromoCodeSave(string Code, long UserID)
        {
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = new DbParameter("@UserID", DbParameter.DbType.Int, 50, UserID);
            dbParam[1] = new DbParameter("@Code", DbParameter.DbType.VarChar, 50, Code);
            dbParam[2] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserPromoCodeSave", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }
        public int UserRefferalCodeSave(string Code, long UserID)
        {
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = new DbParameter("@UserID", DbParameter.DbType.Int, 50, UserID);
            dbParam[1] = new DbParameter("@Code", DbParameter.DbType.VarChar, 50, Code);
            dbParam[2] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserRefferalCodeSave", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }
    }
}
