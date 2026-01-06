namespace BAL
{
    using DAL;
    using Microsoft.ApplicationBlocks.Data;
    using PAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using Utility;



    public class    BusinessBAL : BusinessPAL
    {
        public DataTable GetList(ref int intCurrentPage, int intRecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType)
        {
            DbParameter[] dbParam = new DbParameter[8];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID);
            dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 100, base.Name);
            dbParam[2] = new DbParameter("@Email", DbParameter.DbType.VarChar, 100, base.EmailAddress);
            dbParam[3] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)intCurrentPage);
            dbParam[3].ParamDirection = ParameterDirection.InputOutput;
            dbParam[4] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, intRecordPerPage);
            dbParam[5] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((strSortColumn != string.Empty) && (strSortType != string.Empty))
            {
                dbParam[6] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, strSortType);
                dbParam[7] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, strSortColumn);
            }
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessList", dbParam);
            intCurrentPage = Convert.ToInt32(dbParam[3].Value);
            intTotalRecord = Convert.ToInt32(dbParam[5].Value);
            return table;
        }
        public void Operation(string strConatctID, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.VarChar, 0x7d0, strConatctID), 
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation))
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessOperation", dbParam);
        }

        #region SalesAdmin
        /// <summary>
        /// Business Details Save
        /// </summary>
        /// <returns></returns
        /// 

        public DataTable VerifyBusinessOTP()
        {
            DataTable dt = new DataTable();
            try
            {
                DbParameter[] dbParam = new DbParameter[]
                {
            new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 200, EmailAddress),
            new DbParameter("@OTP", DbParameter.DbType.VarChar, 10, OTP)
                };

                dt = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "SP_VerifyBusinessOTP", dbParam);
            }
            catch (Exception ex)
            {
                throw new Exception("Error verifying OTP: " + ex.Message);
            }
            return dt;
        }


        public void ChangeBusinessPassword()
        {
            try
            {
                DbParameter[] dbParam = new DbParameter[]
                {
            new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 200, EmailAddress),
            new DbParameter("@NewPassword", DbParameter.DbType.VarChar, 500, NewPassword)
                };

                DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SP_ChangeBusinessPassword", dbParam);
            }
            catch (Exception ex)
            {
                throw new Exception("Error changing password: " + ex.Message);
            }
        }


        public long Save(int Version)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 40, base.ID), 
                new DbParameter("@Name", DbParameter.DbType.VarChar, 200, Name),
                new DbParameter("@ABN", DbParameter.DbType.VarChar, 200, ABN),
                new DbParameter("@StreetAddress", DbParameter.DbType.VarChar, 500, StreetAddress),
                new DbParameter("@Location", DbParameter.DbType.VarChar, 500, Location),
                new DbParameter("@FullName", DbParameter.DbType.VarChar, 200, FullName),
                new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 200, EmailAddress),
                new DbParameter("@BusinessPhone", DbParameter.DbType.VarChar, 20, BusinessPhone),
                new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, Mobile),
                new DbParameter("@ProfilePhotoID", DbParameter.DbType.Int, 4, ProfilePhotoID),
                new DbParameter("@Description", DbParameter.DbType.VarChar, 8000, Description),
                new DbParameter("@BSBNo", DbParameter.DbType.VarChar, 200, BSBNo),
                new DbParameter("@AccountNumber", DbParameter.DbType.VarChar, 200, AccountNumber),
                new DbParameter("@BankName", DbParameter.DbType.VarChar, 200, BankName),
                new DbParameter("@AccountName", DbParameter.DbType.VarChar, 200, AccountName),
                new DbParameter("@Status", DbParameter.DbType.Int, 4, Status),
                new DbParameter("@BusinessType", DbParameter.DbType.VarChar, 500, BusinessType),
                new DbParameter("@FoodItems", DbParameter.DbType.VarChar, 500, FoodItems),
                
                
                new DbParameter("@MondaySchedule", DbParameter.DbType.VarChar, 8000, MondaySchedule),
                new DbParameter("@TuesdaySchedule", DbParameter.DbType.VarChar, 8000, TuesdaySchedule),
                new DbParameter("@WednesdaySchedule", DbParameter.DbType.VarChar, 8000, WednesdaySchedule),
                new DbParameter("@ThirsdaySchedule", DbParameter.DbType.VarChar, 8000, ThirsdaySchedule),
                new DbParameter("@FridaySchedule", DbParameter.DbType.VarChar, 8000, FridaySchedule),
                new DbParameter("@SaturdaySchedule", DbParameter.DbType.VarChar, 8000, SaturdaySchedule),
                new DbParameter("@SundayScheduleOn", DbParameter.DbType.VarChar, 8000, SundaySchedule),
                
                new DbParameter("@Monday2Schedule", DbParameter.DbType.VarChar, 8000, Monday2Schedule),
                new DbParameter("@Tuesday2Schedule", DbParameter.DbType.VarChar, 8000, Tuesday2Schedule),
                new DbParameter("@Wednesday2Schedule", DbParameter.DbType.VarChar, 8000, Wednesday2Schedule),
                new DbParameter("@Thirsday2Schedule", DbParameter.DbType.VarChar, 8000, Thirsday2Schedule),
                new DbParameter("@Friday2Schedule", DbParameter.DbType.VarChar, 8000, Friday2Schedule),
                new DbParameter("@Saturday2Schedule", DbParameter.DbType.VarChar, 8000, Saturday2Schedule),
                new DbParameter("@Sunday2ScheduleOn", DbParameter.DbType.VarChar, 8000, Sunday2Schedule),
                
                new DbParameter("@Monday3Schedule", DbParameter.DbType.VarChar, 8000, Monday3Schedule),
                new DbParameter("@Tuesday3Schedule", DbParameter.DbType.VarChar, 8000, Tuesday3Schedule),
                new DbParameter("@Wednesday3Schedule", DbParameter.DbType.VarChar, 8000, Wednesday3Schedule),
                new DbParameter("@Thirsday3Schedule", DbParameter.DbType.VarChar, 8000, Thirsday3Schedule),
                new DbParameter("@Friday3Schedule", DbParameter.DbType.VarChar, 8000, Friday3Schedule),
                new DbParameter("@Saturday3Schedule", DbParameter.DbType.VarChar, 8000, Saturday3Schedule),
                new DbParameter("@Sunday3ScheduleOn", DbParameter.DbType.VarChar, 8000, Sunday3Schedule),
                
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, Password),      
                new DbParameter("@Latitude", DbParameter.DbType.VarChar, 500, Latitude),                
                new DbParameter("@Longitude", DbParameter.DbType.VarChar, 500, Longitude),                
                new DbParameter("@PostCode", DbParameter.DbType.VarChar, 20, PostCode),
                new DbParameter("@RestaurantTypes", DbParameter.DbType.VarChar, 8000, RestaurantTypes),
                new DbParameter("@Version", DbParameter.DbType.VarChar, 8000, Version),
                
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessModify", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }
       
        public DataTable BusinessBankUpdate()
        {
            DbParameter[] dbParam = new DbParameter[12];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, this.ID);
            dbParam[1] = new DbParameter("@ProfilePhotoID", DbParameter.DbType.Int, 10, this.ProfilePhotoID);
            dbParam[2] = new DbParameter("@Description", DbParameter.DbType.VarChar, int.MaxValue, this.Description);
            dbParam[3] = new DbParameter("@AboutUs", DbParameter.DbType.VarChar, 8000, this.AboutUs);   // ✅ Fixed line
            dbParam[4] = new DbParameter("@BSBNo", DbParameter.DbType.VarChar, 100, this.BSBNo);
            dbParam[5] = new DbParameter("@AccountNumber", DbParameter.DbType.VarChar, 100, this.AccountNumber);
            dbParam[6] = new DbParameter("@BankName", DbParameter.DbType.VarChar, 100, this.BankName);
            dbParam[7] = new DbParameter("@AccountName", DbParameter.DbType.VarChar, 100, this.AccountName);
            dbParam[8] = new DbParameter("@BusinessType", DbParameter.DbType.VarChar, 1000, this.BusinessType);
            dbParam[9] = new DbParameter("@FoodItems", DbParameter.DbType.VarChar, 1000, this.FoodItems);
            dbParam[10] = new DbParameter("@DietryIDs", DbParameter.DbType.VarChar, 1000, this.DietryIDs);
            dbParam[11] = new DbParameter("@AuthToken", DbParameter.DbType.VarChar, 500, this.AuthToken);


            DataTable dt = new DataTable();
            dt = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessBankUpdate", dbParam);
            return dt;
        }
        
        public DataTable GetBusinessByOTP()
        {
            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@PasswordOTP", DbParameter.DbType.VarChar, 50, PasswordOTP)
            };

            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "SP_GetBusinessByOTP", dbParam);
        }


        public int UpdateBusinessPasswordAfterOTP(int BusinessID, string EncryptedPassword)
        {
            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 0, BusinessID),
                new DbParameter("@Password", DbParameter.DbType.NVarChar, 200, EncryptedPassword),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 0, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SP_UpdateBusinessPasswordAfterOTP", dbParam);
            return Convert.ToInt32(dbParam[2].Value);
        }

        public void ClearBusinessOTP(int BusinessID)
        {
            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 0, BusinessID)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SP_ClearBusinessOTP", dbParam);
        }

        public long UpdateBusinessDetails()
        {
                DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 40, ID),
                new DbParameter("@Name", DbParameter.DbType.VarChar, 200, Name),
                new DbParameter("@ABN", DbParameter.DbType.VarChar, 200, ABN),
                new DbParameter("@StreetAddress", DbParameter.DbType.VarChar, 500, StreetAddress),
                new DbParameter("@Location", DbParameter.DbType.VarChar, 500, Location),
                new DbParameter("@FullName", DbParameter.DbType.VarChar, 200, FullName),
                new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 200, EmailAddress),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 300, Password),
                new DbParameter("@FirstName", DbParameter.DbType.VarChar, 100, FirstName),
                new DbParameter("@LastName", DbParameter.DbType.VarChar, 100, LastName),
                new DbParameter("@Suburb", DbParameter.DbType.VarChar, 100, Suburb),
                new DbParameter("@State", DbParameter.DbType.VarChar, 100, State),
                new DbParameter("@BusinessPhone", DbParameter.DbType.VarChar, 20, BusinessPhone),
                new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, Mobile),
                new DbParameter("@Latitude", DbParameter.DbType.VarChar, 100, Latitude),
                new DbParameter("@Longitude", DbParameter.DbType.VarChar, 100, Longitude),
                new DbParameter("@PostCode", DbParameter.DbType.VarChar, 20, PostCode),
                new DbParameter("@GSTregistered", DbParameter.DbType.Int, 4, GSTregistered),
                new DbParameter("@ReceiveMarketingMails", DbParameter.DbType.Int, 4, ReceiveMarketingMails),
                new DbParameter("@Note", DbParameter.DbType.VarChar, 2000, Note == null ? "" : Note),
                new DbParameter("@StoreName", DbParameter.DbType.VarChar, 200, StoreName),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
        };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateBusinessDetails", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }

        public static int DeleteBusiness(long businessId, string secretKey, string authToken)
        {
            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, businessId),
                new DbParameter("@AuthToken", DbParameter.DbType.VarChar, 500, authToken),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessDelete", dbParam);

            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }



        public DataTable GetBusinessByID(long ID)
        {
            DbParameter[] dbParam = new DbParameter[] {
        new DbParameter("@ID", DbParameter.DbType.Int, 40, ID)
        };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GetBusinessByID", dbParam);
        }

        public class CurrentDayScheduleBAL
        {
            public long ID { get; set; }
            public long BusinessID { get; set; }
            public string CurrentDate { get; set; }
            public string PackSize { get; set; }
            public int NumberOfPack { get; set; }
            public string Pickup_from { get; set; }
            public int Repeted { get; set; }
            public string Pickup_To { get; set; }
            public decimal OriginalPrice1 { get; set; }
            public decimal DiscountedPrice1 { get; set; }
            public int NumberOfPack1 { get; set; }

            public decimal OriginalPrice2 { get; set; }
            public decimal DiscountedPrice2 { get; set; }
            public int NumberOfPack2 { get; set; }

            public decimal OriginalPrice3 { get; set; }
            public decimal DiscountedPrice3 { get; set; }
            public int NumberOfPack3 { get; set; }

            public string[] SelectedDays { get; set; }

            public int InsertCurrentDaySchedule(string selectedDaysJson)
            {
                DbParameter[] dbParam = new DbParameter[]
                {
                    new DbParameter("@BusinessID", DbParameter.DbType.Int, 0, BusinessID),
                    new DbParameter("@CurrentDate", DbParameter.DbType.VarChar, 20, CurrentDate),
                    //new DbParameter("@PackSize", DbParameter.DbType.VarChar, 100, PackSize),
                    //new DbParameter("@NumberOfPack", DbParameter.DbType.Int, 0, NumberOfPack),
                    new DbParameter("@Pickup_from", DbParameter.DbType.VarChar, 50, Pickup_from),

                    new DbParameter("@Repeted", DbParameter.DbType.Bit, 0, Repeted),
                    new DbParameter("@Pickup_To", DbParameter.DbType.VarChar, 50, Pickup_To),

                    new DbParameter("@SelectedDays", DbParameter.DbType.VarChar, 500, selectedDaysJson),
                            new DbParameter("@OriginalPrice1", DbParameter.DbType.Decimal, 0, OriginalPrice1),
                            new DbParameter("@DiscountedPrice1", DbParameter.DbType.Decimal, 0, DiscountedPrice1),
                            new DbParameter("@NumberOfPack1", DbParameter.DbType.Int, 0, NumberOfPack1),
                            
                            new DbParameter("@OriginalPrice2", DbParameter.DbType.Decimal, 0, OriginalPrice2),
                            new DbParameter("@DiscountedPrice2", DbParameter.DbType.Decimal, 0, DiscountedPrice2),
                            new DbParameter("@NumberOfPack2", DbParameter.DbType.Int, 0, NumberOfPack2),
                            
                            new DbParameter("@OriginalPrice3", DbParameter.DbType.Decimal, 0, OriginalPrice3),
                            new DbParameter("@DiscountedPrice3", DbParameter.DbType.Decimal, 0, DiscountedPrice3),
                            new DbParameter("@NumberOfPack3", DbParameter.DbType.Int, 0, NumberOfPack3),

                    new DbParameter("@ReturnVal", DbParameter.DbType.Int, 0, ParameterDirection.Output)
                };

                DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SP_InsertCurrentDaySchedule", dbParam);
                return Convert.ToInt32(dbParam[15].Value);
            }

            public int InsertWeeklySchedule(long businessId, List<string> selectedDays, string orderDate)
            {
                DbParameter[] dbParam = new DbParameter[]
                {
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 0, businessId),
                new DbParameter("@Monday", DbParameter.DbType.Bit, 0, selectedDays.Contains("MON") ? 1 : 0),
                new DbParameter("@Tuesday", DbParameter.DbType.Bit, 0, selectedDays.Contains("TUE") ? 1 : 0),
                new DbParameter("@Wednesday", DbParameter.DbType.Bit, 0, selectedDays.Contains("WED") ? 1 : 0),
                new DbParameter("@Thursday", DbParameter.DbType.Bit, 0, selectedDays.Contains("THU") ? 1 : 0),
                new DbParameter("@Friday", DbParameter.DbType.Bit, 0, selectedDays.Contains("FRI") ? 1 : 0),
                new DbParameter("@Saturday", DbParameter.DbType.Bit, 0, selectedDays.Contains("SAT") ? 1 : 0),
                new DbParameter("@Sunday", DbParameter.DbType.Bit, 0, selectedDays.Contains("SUN") ? 1 : 0),
                new DbParameter("@OrderDate", DbParameter.DbType.VarChar, 20, orderDate),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 0, ParameterDirection.Output)
                };

                DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SP_InsertWeeklySchedule", dbParam);
                return Convert.ToInt32(dbParam[9].Value);
            }

            public void DeleteWeeklySchedule(long businessID)
            {
                DbParameter[] dbParam = new DbParameter[]
                {
                    new DbParameter("@BusinessID", DbParameter.DbType.Int, 0, businessID)
                };

                DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SP_DeleteWeeklySchedule", dbParam);
            }
        }

        public class WeeklyScheduleBAL
        {
            public long BusinessID { get; set; }
            public string DayName { get; set; }

            //public string PackSize { get; set; }
            //public int? NumberOfPack { get; set; }
            public string Pickup_from { get; set; }
            public string Pickup_To { get; set; }
            public decimal WOriginalPrice1 { get; set; }
            public decimal WDiscountedPrice1 { get; set; }
            public int WNumberOfPack1 { get; set; }
            public decimal WOriginalPrice2 { get; set; }
            public decimal WDiscountedPrice2 { get; set; }
            public int WNumberOfPack2 { get; set; }
            public decimal WOriginalPrice3 { get; set; }
            public decimal WDiscountedPrice3 { get; set; }
            public int WNumberOfPack3 { get; set; }

            /// <summary>
            /// Updates an existing WeeklySchedule record based on BusinessID and DayName.
            /// Returns: 
            ///  > 0  = Updated successfully
            ///  = 0  = Record not found
            ///  < 0  = Error / failure
            /// </summary>
            public DataTable UpdateWeeklySchedule()
            {
                try
                {
                    DbParameter[] dbParam = new DbParameter[]
                    {
                        new DbParameter("@BusinessID", DbParameter.DbType.Int, 0, BusinessID),
                        new DbParameter("@DayName", DbParameter.DbType.VarChar, 10, DayName),
                        //new DbParameter("@PackSize", DbParameter.DbType.VarChar, 100, (object)PackSize ?? DBNull.Value),
                        //new DbParameter("@NumberOfPack", DbParameter.DbType.Int, 0, (object)NumberOfPack ?? DBNull.Value),
                        new DbParameter("@Pickup_from", DbParameter.DbType.VarChar, 50, (object)Pickup_from ?? DBNull.Value),
                        new DbParameter("@Pickup_To", DbParameter.DbType.VarChar, 50, (object)Pickup_To ?? DBNull.Value),
                        new DbParameter("@WOriginalPrice1", DbParameter.DbType.Decimal, 0, WOriginalPrice1),
                        new DbParameter("@WDiscountedPrice1", DbParameter.DbType.Decimal, 0, WDiscountedPrice1),
                        new DbParameter("@WNumberOfPack1", DbParameter.DbType.Int, 0, WNumberOfPack1),
                        
                        new DbParameter("@WOriginalPrice2", DbParameter.DbType.Decimal, 0, WOriginalPrice2),
                        new DbParameter("@WDiscountedPrice2", DbParameter.DbType.Decimal, 0, WDiscountedPrice2),
                        new DbParameter("@WNumberOfPack2", DbParameter.DbType.Int, 0, WNumberOfPack2),
                        
                        new DbParameter("@WOriginalPrice3", DbParameter.DbType.Decimal, 0, WOriginalPrice3),
                        new DbParameter("@WDiscountedPrice3", DbParameter.DbType.Decimal, 0, WDiscountedPrice3),
                        new DbParameter("@WNumberOfPack3", DbParameter.DbType.Int, 0, WNumberOfPack3),
                        new DbParameter("@ReturnVal", DbParameter.DbType.Int, 0, ParameterDirection.Output)
                    };

                    DataSet ds = DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "SP_UpdateWeeklySchedule", dbParam);

                    if (Convert.ToInt32(dbParam[13].Value) == 1 && ds != null && ds.Tables.Count > 0)
                    {
                        return ds.Tables[0]; 
                    }
                    else
                    {
                        return null; 
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///  Business Status Change
        /// </summary>
        /// <returns></returns>
        public long BusinessStatusChange()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 40, base.ID), 
                new DbParameter("@Status", DbParameter.DbType.Int, 2, Status),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessStatusChange", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }

        #endregion


        #region Business

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public long BusinessSave()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 40, base.ID), 
                new DbParameter("@Name", DbParameter.DbType.VarChar, 200, Name),
                new DbParameter("@ABN", DbParameter.DbType.VarChar, 200, ABN),
                new DbParameter("@StreetAddress", DbParameter.DbType.VarChar, 500, StreetAddress),
                new DbParameter("@Location", DbParameter.DbType.VarChar, 500, Location),
                new DbParameter("@FullName", DbParameter.DbType.VarChar, 200, FullName),
                new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 200, EmailAddress),
                new DbParameter("@BusinessPhone", DbParameter.DbType.VarChar, 20, BusinessPhone),
                new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, Mobile),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, Password),
                new DbParameter("@Latitude", DbParameter.DbType.VarChar, 500, Latitude),                
                new DbParameter("@Longitude", DbParameter.DbType.VarChar, 500, Longitude),                
                new DbParameter("@PostCode", DbParameter.DbType.VarChar, 20, PostCode),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output),

            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessSave", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public long BusinessAccountInformationUpdate()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 40, base.ID), 
                new DbParameter("@Name", DbParameter.DbType.VarChar, 200, Name),
                new DbParameter("@ABN", DbParameter.DbType.VarChar, 200, ABN),
                new DbParameter("@StreetAddress", DbParameter.DbType.VarChar, 500, StreetAddress),
                new DbParameter("@Location", DbParameter.DbType.VarChar, 500, Location),
                new DbParameter("@FullName", DbParameter.DbType.VarChar, 200, FullName),
                new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 200, EmailAddress),
                new DbParameter("@BusinessPhone", DbParameter.DbType.VarChar, 20, BusinessPhone),
                new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, Mobile),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessAccountInformationUpdate", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable BusinessLoginCheck(string strDeviceKey, string strDeviceType)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@Email", DbParameter.DbType.VarChar, 200, EmailAddress),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 200, Password),
                new DbParameter("@DeviceKey", DbParameter.DbType.VarChar, 4000, strDeviceKey),
                new DbParameter("@DeviceType", DbParameter.DbType.VarChar, 4000, strDeviceType),
                new DbParameter("@DeviceToken", DbParameter.DbType.VarChar, 500, DeviceToken)
                };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessLoginCheck", dbParam);
        }

        public string GetAuthToken(long businessID)
        {
            DbParameter[] dbParam = new DbParameter[]
            {
        new DbParameter("@BusinessID", DbParameter.DbType.Int, 8, businessID)
            };

            DataTable dt = DbConnectionDAL.GetDataTable(
                CommandType.Text,
                "SELECT AuthToken FROM BusinessAuthToken WHERE BusinessID = @BusinessID",
                dbParam
            );

            if (dt.Rows.Count > 0)
                return Convert.ToString(dt.Rows[0]["AuthToken"]);

            return "";
        }

        #endregion

        public long BusinessCurrentDayScheduleUpdate(int Version,int PickUpTime)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, ID), 
                new DbParameter("@NoOfItems", DbParameter.DbType.Int, 40, NoOfItems),
                new DbParameter("@OriginalPrice", DbParameter.DbType.Decimal, 200, OriginalPrice),
                new DbParameter("@DiscountID", DbParameter.DbType.Decimal, 500, DiscountID),
                new DbParameter("@PickupFromTime", DbParameter.DbType.DateTime, 500, PickupFromTime),
                new DbParameter("@PickupToTime", DbParameter.DbType.DateTime, 200, PickupToTime),
                new DbParameter("@Version", DbParameter.DbType.Int, 2, Version),
                new DbParameter("@PickUpTime", DbParameter.DbType.Int, 2, PickUpTime),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessCurrentDayScheduleUpdate", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }
        public void BusinessCurrentDayScheduleUpdateCronJob()
        {
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessCurrentDayScheduleUpdateCronJob");

        }

        public DataTable BusinessProductList(string CategoryIDs, string Search1, string Search2, string Search3, int Postcode, ref int CurrentPage, int RecordPerPage, out int TotalRecord, string Latitude, string Longitude)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@CategoryIDs", DbParameter.DbType.VarChar, 2000, CategoryIDs),
                new DbParameter("@Search1", DbParameter.DbType.VarChar, 2000, Search1),
                new DbParameter("@Search2", DbParameter.DbType.VarChar, 2000, Search2),
                new DbParameter("@Search3", DbParameter.DbType.VarChar, 2000, Search3),
                new DbParameter("@Postcode", DbParameter.DbType.VarChar, 2000, Postcode),
                new DbParameter("@CurrentPage", DbParameter.DbType.Int, 20, (int) CurrentPage),
                new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 20, RecordPerPage),
                new DbParameter("@TotalRecord", DbParameter.DbType.Int, 20, ParameterDirection.Output),
                new DbParameter("@Lat1", DbParameter.DbType.VarChar, 500, Latitude),
                new DbParameter("@Lon1", DbParameter.DbType.VarChar, 500, Longitude),
            
            };
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessProductList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[5].Value);
            TotalRecord = Convert.ToInt32(dbParam[7].Value);
            return table;
        }


        public DataTable BusinessDetailsByID(long UserID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 20, ID),
                new DbParameter("@UserID", DbParameter.DbType.Int, 20, UserID)                           
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessDetailsByID", dbParam);
        }
        public long UserFavouriteBusinessAdd(long UserID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@UserID", DbParameter.DbType.Int, 40, UserID), 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, ID),                 
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserFavouriteBusinessAdd", dbParam);
            return 1;
        }

        public DataSet BusinessCurrentDayOrders(string AuthToken)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 20, ID)  ,
                new DbParameter("@AuthToken", DbParameter.DbType.VarChar, 2000, AuthToken) 
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "BusinessCurrentDayOrders", dbParam);
        }

        public long UpdateBusinessProfilePhoto()
        {
            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@ID", DbParameter.DbType.Int, 40, ID),
                new DbParameter("@ProfilePhotoID", DbParameter.DbType.Int, 40, ProfilePhotoID),
                new DbParameter("@ProfilePhoto", DbParameter.DbType.VarChar, 500, ProfilePhoto),
                new DbParameter("@StoreImages", DbParameter.DbType.NVarChar, -1, StoreImages),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateBusinessProfilePhoto", dbParam);
            return Convert.ToInt64(dbParam[4].Value);
        }

        //public long UpdateBusinessProfilePhoto()
        //{
        //    DbParameter[] dbParam = new DbParameter[] { 
        //        new DbParameter("@ID", DbParameter.DbType.Int, 40, ID), 
        //        new DbParameter("@ProfilePhotoID", DbParameter.DbType.Int, 40, ProfilePhotoID), 
        //        new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
        //    };
        //    DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateBusinessProfilePhoto", dbParam);
        //    return 1;
        //}

        public long UpdateBusinessProfilePhotoPath()
        {
            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, this.ID),
                new DbParameter("@ProfilePhoto", DbParameter.DbType.VarChar, 500, this.ProfilePhoto)
            };

            object result = DbConnectionDAL.GetScalarValue(CommandType.StoredProcedure, "BusinessUpdateProfilePhotoPath", dbParam);
            return (result != null) ? Convert.ToInt64(result) : 0;
        }


        public long UpdateBusinessWhatToExpact(string FoodItems)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 40, ID), 
                new DbParameter("@FoodItems", DbParameter.DbType.VarChar, 1000, FoodItems), 
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateBusinessWhatToExpact", dbParam);
            return 1;
        }

        public long UpdateBusinessBankDetails()
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 40, ID),
                new DbParameter("@BSBNo", DbParameter.DbType.VarChar, 200, BSBNo),
                new DbParameter("@AccountNumber", DbParameter.DbType.VarChar, 200, AccountNumber),
                new DbParameter("@BankName", DbParameter.DbType.VarChar, 200, BankName),
                new DbParameter("@AccountName", DbParameter.DbType.VarChar, 200, AccountName),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateBusinessBankDetails", dbParam);
            return 1;
        }
        public long UpdateBusinessTypes(string BusinessTypes, byte RegisterGst, byte CategoryTaxItemOrNot)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 40, ID), 
                new DbParameter("@BusinessTypes", DbParameter.DbType.VarChar, 1000, BusinessTypes),
                new DbParameter("@RegisterGst", DbParameter.DbType.Int, 4, RegisterGst),
                new DbParameter("@CategoryTaxItemOrNot", DbParameter.DbType.Int, 4, CategoryTaxItemOrNot),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateBusinessTypes", dbParam);
            return 1;
        }

        public static long UpdateDietaryTypes(long BusinessID, string DietaryTypes)
        {
            DbParameter[] dbParam = new DbParameter[] {
        new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID),
        new DbParameter("@DietaryTypes", DbParameter.DbType.VarChar, 1000, DietaryTypes),
        new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
    };

            DbConnectionDAL.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "UpdateBusinessDietTypes",
                dbParam
            );

            return Convert.ToInt64(dbParam[2].Value);
        }



        public long UpdateBusinessDescription()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 40, ID), 
                new DbParameter("@Description", DbParameter.DbType.VarChar, 8000, Description),
                new DbParameter("@AboutUs", DbParameter.DbType.VarChar, 8000, AboutUs),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateBusinessDescription", dbParam);
            return 1;
        }

        public long BusinessScheduleModify(int PickupTime)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 40, base.ID),                 
                new DbParameter("@MondaySchedule", DbParameter.DbType.VarChar, 8000, MondaySchedule),
                new DbParameter("@TuesdaySchedule", DbParameter.DbType.VarChar, 8000, TuesdaySchedule),
                new DbParameter("@WednesdaySchedule", DbParameter.DbType.VarChar, 8000, WednesdaySchedule),
                new DbParameter("@ThirsdaySchedule", DbParameter.DbType.VarChar, 8000, ThirsdaySchedule),
                new DbParameter("@FridaySchedule", DbParameter.DbType.VarChar, 8000, FridaySchedule),
                new DbParameter("@SaturdaySchedule", DbParameter.DbType.VarChar, 8000, SaturdaySchedule),
                new DbParameter("@SundaySchedule", DbParameter.DbType.VarChar, 8000, SundaySchedule),               
                new DbParameter("@PickupTime", DbParameter.DbType.VarChar, 8000, PickupTime)                
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessScheduleModify", dbParam);
            return 1;
        }

        public int Changepassword(string OldPassword)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 10, ID), 
                new DbParameter("@Password", DbParameter.DbType.VarChar, 50, OldPassword),
                new DbParameter("@NewPassword", DbParameter.DbType.VarChar, 50, Password), 
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 10, ParameterDirection.Output) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessChangePassword", dbParam);
            return Convert.ToInt32(dbParam[3].Value);
        }

        public DataSet BusinessDropdownRegistration()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@BusinessID ", DbParameter.DbType.Int, 200, ID)                
                };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "BusinessDropdownRegistration", dbParam);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable BusinessForgotPassword(string strPassword)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@EmailID", DbParameter.DbType.VarChar, 200, EmailAddress),                
                new DbParameter("@Password", DbParameter.DbType.VarChar, 200, strPassword)                
                };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessForgotPassword", dbParam);
        }

        //new
        public DataTable BusinessForgotPasswordOTP(string OTP)
        {
            DbParameter[] dbParam = new DbParameter[]
            {
        new DbParameter("@EmailID", DbParameter.DbType.VarChar, 200, EmailAddress),
        new DbParameter("@OTP", DbParameter.DbType.VarChar, 10, OTP)
            };

            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessForgotPasswordOTP", dbParam);
        }


        public long SaveAdmin()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 40, base.ID), 
                new DbParameter("@Name", DbParameter.DbType.VarChar, 200, Name),
                new DbParameter("@ABN", DbParameter.DbType.VarChar, 200, ABN),
                new DbParameter("@StreetAddress", DbParameter.DbType.VarChar, 500, StreetAddress),
                new DbParameter("@Location", DbParameter.DbType.VarChar, 500, Location),
                new DbParameter("@FullName", DbParameter.DbType.VarChar, 200, FullName),
                new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 200, EmailAddress),
                new DbParameter("@BusinessPhone", DbParameter.DbType.VarChar, 20, BusinessPhone),
                new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, Mobile),                
                new DbParameter("@Description", DbParameter.DbType.VarChar, 8000, Description),
                new DbParameter("@BSBNo", DbParameter.DbType.VarChar, 200, BSBNo),
                new DbParameter("@AccountNumber", DbParameter.DbType.VarChar, 200, AccountNumber),
                new DbParameter("@BankName", DbParameter.DbType.VarChar, 200, BankName),
                new DbParameter("@AccountName", DbParameter.DbType.VarChar, 200, AccountName),                
                new DbParameter("@BusinessType", DbParameter.DbType.VarChar, 500, BusinessType),
                new DbParameter("@FoodItems", DbParameter.DbType.VarChar, 500, FoodItems),
                new DbParameter("@MondaySchedule", DbParameter.DbType.VarChar, 8000, MondaySchedule),
                new DbParameter("@TuesdaySchedule", DbParameter.DbType.VarChar, 8000, TuesdaySchedule),
                new DbParameter("@WednesdaySchedule", DbParameter.DbType.VarChar, 8000, WednesdaySchedule),
                new DbParameter("@ThirsdaySchedule", DbParameter.DbType.VarChar, 8000, ThirsdaySchedule),
                new DbParameter("@FridaySchedule", DbParameter.DbType.VarChar, 8000, FridaySchedule),
                new DbParameter("@SaturdaySchedule", DbParameter.DbType.VarChar, 8000, SaturdaySchedule),
                new DbParameter("@SundayScheduleOn", DbParameter.DbType.VarChar, 8000, SundaySchedule),                
                new DbParameter("@Monday2Schedule", DbParameter.DbType.VarChar, 8000, Monday2Schedule),
                new DbParameter("@Tuesday2Schedule", DbParameter.DbType.VarChar, 8000, Tuesday2Schedule),
                new DbParameter("@Wednesday2Schedule", DbParameter.DbType.VarChar, 8000, Wednesday2Schedule),
                new DbParameter("@Thirsday2Schedule", DbParameter.DbType.VarChar, 8000, Thirsday2Schedule),
                new DbParameter("@Friday2Schedule", DbParameter.DbType.VarChar, 8000, Friday2Schedule),
                new DbParameter("@Saturday2Schedule", DbParameter.DbType.VarChar, 8000, Saturday2Schedule),
                new DbParameter("@Sunday2ScheduleOn", DbParameter.DbType.VarChar, 8000, Sunday2Schedule),                
                new DbParameter("@Monday3Schedule", DbParameter.DbType.VarChar, 8000, Monday3Schedule),
                new DbParameter("@Tuesday3Schedule", DbParameter.DbType.VarChar, 8000, Tuesday3Schedule),
                new DbParameter("@Wednesday3Schedule", DbParameter.DbType.VarChar, 8000, Wednesday3Schedule),
                new DbParameter("@Thirsday3Schedule", DbParameter.DbType.VarChar, 8000, Thirsday3Schedule),
                new DbParameter("@Friday3Schedule", DbParameter.DbType.VarChar, 8000, Friday3Schedule),
                new DbParameter("@Saturday3Schedule", DbParameter.DbType.VarChar, 8000, Saturday3Schedule),
                new DbParameter("@Sunday3ScheduleOn", DbParameter.DbType.VarChar, 8000, Sunday3Schedule),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, Password),      
                new DbParameter("@Latitude", DbParameter.DbType.VarChar, 500, Latitude),                
                new DbParameter("@Longitude", DbParameter.DbType.VarChar, 500, Longitude),                
                new DbParameter("@PostCode", DbParameter.DbType.VarChar, 20, PostCode),
                new DbParameter("@RestaurantTypes", DbParameter.DbType.VarChar, 80000, RestaurantTypes),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)                
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessModifyAdmin", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }
        public void BusinessLogout(string strDeviceKey)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 200, ID),
                new DbParameter("@DeviceKey", DbParameter.DbType.VarChar, 2000, strDeviceKey)
                };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessLogout", dbParam);
        }


        public DataSet GetOrderNotificationList()
        {
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GetOrderNotificationList");                        
        }
        public void UpdateOrderNotications(string strOrderID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@OrderID", DbParameter.DbType.VarChar, 8000, strOrderID)                
                };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateOrderNotications", dbParam);
        }
        public void UpdateNoticationsForNitificationModule(string strOrderID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@OrderID", DbParameter.DbType.VarChar, 8000, strOrderID)                
                };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateNoticationsForNitificationModule", dbParam);
        }
        public DataTable BusinessDetailsByIDForContactEnquiry()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 20, ID)                                 
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessDetailsByIDForContactEnquiry", dbParam);
        }
        public DataSet GetOrderNotificationListByNotificationID(long NotificationID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@NotificationID", DbParameter.DbType.Int, 4000, NotificationID)                
                };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GetOrderNotificationListByNotificationID", dbParam);
        }
        public void BusinessRestaurantUpdate()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 20, ID), 
                new DbParameter("@RestaurantTypes", DbParameter.DbType.VarChar, 8000, RestaurantTypes),                
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessRestaurantUpdate", dbParam);            
        }
        public DataTable BusinessPickupTimesByBusinessID()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 20, ID)                                 
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessPickupTImesByBusinessID", dbParam);
        }
    }
}


