using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Product.Models;
using System.Data;
using System.Configuration;

namespace Product.DataManager
{
    public class HomeDataManager:BaseClass
    {
        public static List<HomeModel> GetHomeList(string BrandID, string RegionID)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
            List<HomeModel> listItems = new List<HomeModel>();
            HomeModel requestDetail;
            OracleCommand cmd = new OracleCommand(PKG_ORDER_RESULTS, con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                using (con)
                {
                    using (cmd)
                    {
                        con.Open();
                        cmd.Parameters.Add("BrandID", OracleDbType.Varchar2).Value = BrandID;
                        cmd.Parameters.Add("RegionID", OracleDbType.Varchar2).Value = RegionID;
                        cmd.Parameters.Add("Error_no", OracleDbType.Int64).Value = ParameterDirection.Output;
                        cmd.Parameters.Add("Error_desc", OracleDbType.Varchar2,4000).Value = ParameterDirection.Output; 
                        cmd.Parameters.Add("RegionID", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        using(OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            DataTable dt = new DataTable();
                            dt.Load(dr);
                            foreach(DataRow row in dt.Rows)
                            {
                                requestDetail = new HomeModel();
                                requestDetail.Order_id = row["Order_id"] != DBNull.Value ? Convert.ToString(row["Order_id"]) : string.Empty;
                                requestDetail.Order_cost= row["Order_cost"] != DBNull.Value ? Convert.ToString(row["Order_cost"]) : string.Empty;                             
                                requestDetail.Product= row["Product"] != DBNull.Value ? Convert.ToString(row["Product"]) : string.Empty;
                                requestDetail.Qty= row["Qty"] != DBNull.Value ? Convert.ToString(row["Qty"]) : string.Empty;
                                requestDetail.Brand_ID= row["Brand_ID"] != DBNull.Value ? Convert.ToString(row["Brand_ID"]) : string.Empty;
                                requestDetail.Region_ID= row["Region_ID"] != DBNull.Value ? Convert.ToString(row["Region_ID"]) : string.Empty;

                                listItems.Add(requestDetail);

                            }
                            return listItems;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                string error = e.Message;
                throw;
            }

        }
    }
}