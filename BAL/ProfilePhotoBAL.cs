namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class ProfilePhotoBAL : ProfilePhotoPAL
    {
        /// <summary>
        /// ProfilePhoto Operations Active/Deactive/Delete
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ObjOperation"></param>
        public void ProfilePhotoOperation(string ID, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@ID", DbParameter.DbType.VarChar, 500, ID),
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation))
            };

            DbConnectionDAL.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "ProfilePhotoOperation",
                dbParam
            );
        }

        /// <summary>
        /// Get ProfilePhoto Details By ID
        /// </summary>
        /// <returns></returns>
        public DataTable GetProfilePhotoByID()
        {
            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID)
            };

            return DbConnectionDAL.GetDataTable(
                CommandType.StoredProcedure,
                "ProfilePhotoList",
                dbParam
            );
        }

        /// <summary>
        /// ProfilePhoto list Paging
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="RecordPerPage"></param>
        /// <param name="TotalRecord"></param>
        /// <param name="SortColumn"></param>
        /// <param name="SortType"></param>
        /// <returns></returns>
        public DataTable GetList(
            ref int CurrentPage,
            int RecordPerPage,
            out int TotalRecord,
            string SortColumn,
            string SortType)
        {
            DbParameter[] dbParam = new DbParameter[7];

            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID);
            dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 200, base.Name);

            dbParam[2] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)CurrentPage);
            dbParam[2].ParamDirection = ParameterDirection.InputOutput;

            dbParam[3] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[4] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);

            if (!string.IsNullOrEmpty(SortColumn) && !string.IsNullOrEmpty(SortType))
            {
                dbParam[5] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[6] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }

            DataTable table = DbConnectionDAL.GetDataTable(
                CommandType.StoredProcedure,
                "ProfilePhotoList",
                dbParam
            );

            CurrentPage = Convert.ToInt32(dbParam[2].Value);
            TotalRecord = Convert.ToInt32(dbParam[4].Value);

            return table;
        }

        /// <summary>
        /// ProfilePhoto Details Save
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@ID", DbParameter.DbType.Int, 4, base.ID),
                new DbParameter("@Name", DbParameter.DbType.VarChar, 200, base.Name),
                new DbParameter("@ImageName", DbParameter.DbType.VarChar, 100, base.ImageName),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(
                CommandType.StoredProcedure,
                "ProfilePhotoModify",
                dbParam
            );

            return Convert.ToInt16(dbParam[3].Value);
        }

        public DataTable ProfilePhotoListByUserID(long UserID)
        {
            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, UserID)
            };

            return DbConnectionDAL.GetDataTable(
                CommandType.StoredProcedure,
                "ProfilePhotoListWithUser",
                dbParam
            );
        }
    }
}
