using DAL;
using System;
using System.Data;
using System.Runtime.InteropServices;
using Utility;

namespace BiteLoop.Common
{
    public class BusinessCommonBAL : BusinessCommonPAL
    {
        public long BusinessRegistrationStep1()
        {
            DbParameter[] dbParam = new DbParameter[] {                                 
                new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 200, EmailAddress),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, Password),                
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output),

            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessRegistrationStep1", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }

        public long BusinessRegistrationStep2(string FirstName, string LastName, string Mobile)
        {
            DbParameter[] dbParam = new DbParameter[] {                                 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 200, ID),
                new DbParameter("@FirstName", DbParameter.DbType.VarChar, 500, FirstName),                
                new DbParameter("@LastName", DbParameter.DbType.VarChar, 500, LastName),      
                new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, Mobile),     
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 20, ParameterDirection.Output),

            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessRegistrationStep2", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }
        public long BusinessRegistrationStep3(string BusinessName, string Location, string Phone, string StoreManagerName, int MultipleStore, string State)
        {
            DbParameter[] dbParam = new DbParameter[] {                                 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 200, ID),
                new DbParameter("@BusinessName", DbParameter.DbType.VarChar, 500, BusinessName),                
                new DbParameter("@Location", DbParameter.DbType.VarChar, 500, Location),      
                new DbParameter("@Phone", DbParameter.DbType.VarChar, 500, Phone),     
                new DbParameter("@StoreManagerName", DbParameter.DbType.VarChar, 500, StoreManagerName),     
                new DbParameter("@MultipleStore", DbParameter.DbType.Int, 5, MultipleStore),     
                new DbParameter("@State", DbParameter.DbType.VarChar, 100, State),     
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 20, ParameterDirection.Output),

            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessRegistrationStep3", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }
        public long BusinessRegistrationStep4(string BusinessTypes, int BYOContainers)
        {
            DbParameter[] dbParam = new DbParameter[] {                                 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 200, ID),
                new DbParameter("@BusinessTypes", DbParameter.DbType.VarChar, 8000, BusinessTypes),                              
                new DbParameter("@BYOContainers", DbParameter.DbType.Int, 5, BYOContainers),     
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 20, ParameterDirection.Output),

            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessRegistrationStep4", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }
        public long BusinessRegistrationStep5(string FoodTypes)
        {
            DbParameter[] dbParam = new DbParameter[] {                                 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 200, ID),
                new DbParameter("@FoodTypes", DbParameter.DbType.VarChar, 8000, FoodTypes),                                              
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 20, ParameterDirection.Output),

            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessRegistrationStep5", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }

        public long BusinessRegistrationStep6(string MondayFromTime, string MondayToTime, string TuesdayFromTime, string TuesdayToTime,
                        string WednesdayFromTime, string WednesdayToTime, string ThirsdayFromTime, string ThirsdayToTime,
                        string FridayFromTime, string FridayToTime, string SaturdayFromTime, string SaturdayToTime, string SundayFromTime,
                        string SundayToTime, string NoOfMeals, string OriginalPrice, string DiscountedPrice)
        {
            DbParameter[] dbParam = new DbParameter[] {                                 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 200, ID),
                new DbParameter("@MondayFromTime", DbParameter.DbType.VarChar, 100, MondayFromTime),                                              
                new DbParameter("@MondayToTime", DbParameter.DbType.VarChar, 100, MondayToTime),    
                new DbParameter("@TuesdayFromTime", DbParameter.DbType.VarChar, 100, TuesdayFromTime),    
                new DbParameter("@TuesdayToTime", DbParameter.DbType.VarChar, 100, TuesdayToTime),    
                new DbParameter("@WednesdayFromTime", DbParameter.DbType.VarChar, 100, WednesdayFromTime),    
                new DbParameter("@WednesdayToTime", DbParameter.DbType.VarChar, 100, WednesdayToTime),    
                new DbParameter("@ThirsdayFromTime", DbParameter.DbType.VarChar, 100, ThirsdayFromTime),    
                new DbParameter("@ThirsdayToTime", DbParameter.DbType.VarChar, 100, ThirsdayToTime),    
                new DbParameter("@FridayFromTime", DbParameter.DbType.VarChar, 100, FridayFromTime),    
                new DbParameter("@FridayToTime", DbParameter.DbType.VarChar, 100, FridayToTime),    
                new DbParameter("@SaturdayFromTime", DbParameter.DbType.VarChar, 100, SaturdayFromTime),    
                new DbParameter("@SaturdayToTime", DbParameter.DbType.VarChar, 100, SaturdayToTime),    
                new DbParameter("@SundayFromTime", DbParameter.DbType.VarChar, 100, SundayFromTime),    
                new DbParameter("@SundayToTime", DbParameter.DbType.VarChar, 100, SundayToTime),    
                new DbParameter("@NoOfMeals", DbParameter.DbType.VarChar, 100, NoOfMeals),    
                new DbParameter("@OriginalPrice", DbParameter.DbType.VarChar, 100, OriginalPrice),    
                new DbParameter("@DiscountedPrice", DbParameter.DbType.VarChar, 100, DiscountedPrice),  
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 20, ParameterDirection.Output),

            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessRegistrationStep6", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }

        //public long BusinessHolidaySave(long BusinessID, DateTime Date, string Title)
        //{
        //    DbParameter[] dbParam = new DbParameter[] { 
        //        new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID), 
        //        new DbParameter("@Date", DbParameter.DbType.DateTime, 200, Date),
        //        new DbParameter("@Title", DbParameter.DbType.VarChar, 1000, Title),
        //        new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
        //    };
        //    DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessHolidaySave", dbParam);
        //    return Convert.ToInt64(dbParam[3].Value);
        //}

        public DataTable BusinessDetailsForRegistration(long BusinessID, string DeviceKey, string DeviceType)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID),                 
                new DbParameter("@DeviceKey", DbParameter.DbType.VarChar, 400, DeviceKey),
                new DbParameter("@DeviceType", DbParameter.DbType.VarChar, 400, DeviceType)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessDetailsForRegistration", dbParam);
        }
        public DataTable BusinessDetailsByIDForStepRegistration(long BusinessID, int Step)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID),                 
                new DbParameter("@Step", DbParameter.DbType.Int, 400, Step)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessDetailsByIDForStepRegistration", dbParam);
        }
    }
}
