namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class CartBAL : CartPAL
    {
        public long CartAdd(out int RemainingQty, long PickUpTimeID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@UserID", DbParameter.DbType.Int, 40, UserID), 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID), 
                new DbParameter("@Qty", DbParameter.DbType.Int, 500, Qty),
                new DbParameter("@Donation", DbParameter.DbType.Decimal, 200, Donation),
                new DbParameter("@RemainingQuantity", DbParameter.DbType.Int, 200, ParameterDirection.Output),                
                new DbParameter("@PickUpTimeID", DbParameter.DbType.Int, 20, PickUpTimeID),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "CartAdd", dbParam);
            RemainingQty = Convert.ToInt32(dbParam[dbParam.Length - 3].Value);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }

        public DataTable CartSummary()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@UserID", DbParameter.DbType.Int, 20, UserID),
                 new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CartSummary", dbParam);
        }
        public long PlaceOrder(int intVersion, decimal RewardsPoints)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@UserID", DbParameter.DbType.Int, 40, UserID), 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID), 

                new DbParameter("@TransactionId", DbParameter.DbType.VarChar, 8000, TransactionId), 
                new DbParameter("@Currency", DbParameter.DbType.VarChar, 8000, Currency), 
                new DbParameter("@CustomerId", DbParameter.DbType.VarChar, 8000, CustomerId), 
                new DbParameter("@CardType", DbParameter.DbType.VarChar, 8000, CardType), 
                new DbParameter("@CardLastDigits", DbParameter.DbType.VarChar, 8000, CardLastDigits), 
                new DbParameter("@TransactionResponse", DbParameter.DbType.VarChar, 8000, TransactionResponse), 
                new DbParameter("@Version", DbParameter.DbType.Int, 2, intVersion), 
                new DbParameter("@RewardsPoints", DbParameter.DbType.Decimal, 10, RewardsPoints), 
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "PlaceOrder", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }
        public DataTable OrderSummary(long OrderID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@OrderID", DbParameter.DbType.Int, 20, OrderID),                 
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "OrderSummary", dbParam);
        }
        public DataTable UserOrders()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@UserID", DbParameter.DbType.Int, 20, UserID)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserOrders", dbParam);
        }
        public long ConfirmCollected(long OrderID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@OrderID", DbParameter.DbType.Int, 40, OrderID)                
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "ConfirmCollected", dbParam);
            return 1;
        }

        public DataTable UserOrdersReport(ref int intCurrentPage, int RecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType, string Email, DateTime StartDate, DateTime EndDate)
        {
            DbParameter[] dbParam = new DbParameter[11];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 40, ID);
            dbParam[1] = new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID);
            dbParam[2] = new DbParameter("@UserID", DbParameter.DbType.Int, 40, UserID);
            dbParam[3] = new DbParameter("@Email", DbParameter.DbType.VarChar, 500, Email);
            dbParam[4] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 4, (int)intCurrentPage, ParameterDirection.InputOutput);
            dbParam[5] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 4, RecordPerPage);
            dbParam[6] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((strSortColumn != string.Empty) && (strSortType != string.Empty))
            {
                dbParam[7] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 10, strSortType);
                dbParam[8] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 50, strSortColumn);
            }
            dbParam[9] = new DbParameter("@StartDate", DbParameter.DbType.DateTime, 40, StartDate);
            dbParam[10] = new DbParameter("@EndDate", DbParameter.DbType.DateTime, 40, EndDate);
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserOrdersReport", dbParam);
            intCurrentPage = Convert.ToInt32(dbParam[4].Value);
            intTotalRecord = Convert.ToInt32(dbParam[6].Value);
            return table;
        }

        public DataTable BusinessTimelyReport(DateTime StartDate, DateTime EndDate)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 20, BusinessID),
                new DbParameter("@StartDate", DbParameter.DbType.DateTime, 200, StartDate),
                new DbParameter("@EndDate", DbParameter.DbType.DateTime, 200, EndDate)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessTimelyReport", dbParam);
        }
        public long UserCreditCardAdd(long intUserID, string CustomerID, string strCardType, string strLastDigits)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@UserID", DbParameter.DbType.Int, 40, intUserID) ,               
                new DbParameter("@CustomerID", DbParameter.DbType.VarChar, 4000, CustomerID),
                new DbParameter("@LastDigits", DbParameter.DbType.VarChar, 40, strLastDigits),
                new DbParameter("@CardType", DbParameter.DbType.VarChar, 40, strCardType), 
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserCreditCardAdd", dbParam);            
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }
        public DataTable OrderUsersDeviceKey(long OrderID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@OrderID", DbParameter.DbType.Int, 20, OrderID)                
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "OrderUsersDeviceKey", dbParam);
        }
    }
}


