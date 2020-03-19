using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace api.Services
{
    public class DefaultPrinterService : IDefaultPrinterService
    {

        public GetDefaultPrinterView GetInfo(string code)
        {
            using (var ctx = new ConXContext())
            {
                //whmobileprnt_default prnt_def = ctx.mobileprnt_def
                //    .Where(z=>z.MC_CODE == code).SingleOrDefault();

                string sql = "select series_no from whmobileprnt_default where mc_code = :p_mc_code";

                string prnt_def = ctx.Database.SqlQuery<string>(sql, new OracleParameter("p_mc_code", code)).SingleOrDefault();

                if (prnt_def == null)
                {
                    return new GetDefaultPrinterView
                    {
                        serial_no = "",
                        grp_type = "",
                        prnt_point_name = "",
                        //default_no = "",
                        filepath_data = "",
                        filepath_btw = "",
                        filepath_txt = ""
                    };
                }

                else
                {
                    // whmobileprnt_ctl model = ctx.mobileprnt_ctl
                    //.Where(z => z.SERIES_NO == prnt_def.SERIES_NO).SingleOrDefault();
                    string sqlp = "select series_no serial_no , grp_type , prnt_point_name , filepath_data , filepath_btw , filepath_txt  from whmobileprnt_ctl where series_no = :p_series_no";

                    PrinterView model = ctx.Database.SqlQuery<PrinterView>(sqlp, new OracleParameter("p_series_no", prnt_def)).SingleOrDefault();


                    return new GetDefaultPrinterView
                    {
                        serial_no = model.serial_no,
                        grp_type = model.grp_type,
                        prnt_point_name = model.prnt_point_name,
                        filepath_data = model.filepath_data,
                        filepath_btw = model.filepath_btw,
                        filepath_txt = model.filepath_txt
                    };
                }
            }
        }

        public void SetDefault(DefaultPrinterView model)
        {
            using (var ctx = new ConXContext())
            {
                using (TransactionScope scope = new TransactionScope())
                {

                   // whmobileprnt_default prnt_def = ctx.mobileprnt_def
                   //.Where(z => z.MC_CODE == model.mc_code).SingleOrDefault();

                    string sqlp = "select series_no  from whmobileprnt_default where mc_code = :p_mc_code";

                    string printer = ctx.Database.SqlQuery<string>(sqlp, new OracleParameter("p_mc_code", model.mc_code)).SingleOrDefault();

                    if (printer == null)
                    {
                        //whmobileprnt_default newObj = new whmobileprnt_default()
                        //{
                        //    MC_CODE = model.mc_code,
                        //    SERIES_NO = model.serial_no,
                        //    UPD_BY = model.user_id,
                        //    UPD_DATE = DateTime.Now

                        //};

                        //ctx.mobileprnt_def.Add(newObj);
                        //ctx.SaveChanges();
                        //scope.Complete();

                        string strConn = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
                        var dataConn = new OracleConnectionStringBuilder(strConn);
                        OracleConnection conn = new OracleConnection(dataConn.ToString());

                        conn.Open();

                        OracleCommand oraCommand = conn.CreateCommand();
                        OracleParameter[] param = new OracleParameter[]
                        {
                            new OracleParameter("p_mc_code",model.mc_code),
                            new OracleParameter("p_series_no", model.serial_no),
                            new OracleParameter("p_upd_by", model.user_id),
                            new OracleParameter("p_upd_date", DateTime.Now),

                        };
                        oraCommand.BindByName = true;
                        oraCommand.Parameters.AddRange(param);
                        oraCommand.CommandText = "insert into whmobileprnt_default (mc_code , series_no , upd_by , upd_date) values (:p_mc_code , :p_series_no , :p_upd_by , :p_upd_date)";


                        oraCommand.ExecuteNonQuery();

                        conn.Close();


                        scope.Complete();

                    }
                    else
                    {
                        //whmobileprnt_default updateObj = ctx.mobileprnt_def
                        //    .Where(z => z.MC_CODE == model.mc_code).SingleOrDefault();

                        //updateObj.MC_CODE = model.mc_code;
                        //updateObj.SERIES_NO = model.serial_no;
                        //updateObj.UPD_BY = model.user_id;
                        //updateObj.UPD_DATE = DateTime.Now;

                        //ctx.Configuration.AutoDetectChangesEnabled = true;
                        //ctx.SaveChanges();
                        //scope.Complete();
                        string strConn = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;
                        var dataConn = new OracleConnectionStringBuilder(strConn);
                        OracleConnection conn = new OracleConnection(dataConn.ToString());

                        conn.Open();

                        OracleCommand oraCommand = conn.CreateCommand();
                        OracleParameter[] param = new OracleParameter[]
                        {
                            new OracleParameter("p_mc_code",model.mc_code),
                            new OracleParameter("p_series_no", model.serial_no),
                            new OracleParameter("p_upd_by", model.user_id),
                            new OracleParameter("p_upd_date", DateTime.Now),

                        };
                        oraCommand.BindByName = true;
                        oraCommand.Parameters.AddRange(param);
                        oraCommand.CommandText = "update whmobileprnt_default set series_no = :p_series_no , upd_by =:p_upd_by , upd_date = :p_upd_date  where mc_code = :p_mc_code";


                        oraCommand.ExecuteNonQuery();

                        conn.Close();


                        scope.Complete();
                    }

                   
                }
            }
        }

        
    }
}