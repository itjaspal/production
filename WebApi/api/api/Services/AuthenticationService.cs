using api.DataAccess;
using api.Interfaces;
using api.Models;
using api.ModelViews;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace api.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DropdownlistService));

        public AuthenticationService()
        {

        }

        public AuthenticationData login(string username, string password)
        {
            using (var ctx = new ConXContext())
            {
                su_user user = ctx.user
                    .Include("departments")
                    .Include("user_mac")
                    .Where(z => z.USER_ID == username)
                    .SingleOrDefault();

                //su_user user = ctx.user.SqlQuery("Select a.USER_ID , a.USER_NAME , a.USER_PASSWORD , a.DEPT_CODE , a.ACTIVE , b.DEPT_NAMET , c.MC_CODE , c.STATUS from su_user a , department b , pd_mapp_user_mac c  where a.dept_code=b.dept_code and a.user_id=c.user_id and a.user_id = :param1", new OracleParameter("param1", username)).SingleOrDefault();

                //department dept = ctx.departments.SqlQuery("select DEPT_CODE , DEPT_NAMET from department where dept_code = :param1", new OracleParameter("param1", user.DEPT_CODE)).SingleOrDefault();

                //pd_mapp_user_mac user_mac = ctx.user_mac.SqlQuery("select USER_ID , MC_CODE , STATUS from pd_mapp_user_mac where user_id = :param1", new OracleParameter("param1", user.USER_ID)).SingleOrDefault();

                whmobileprnt_ctl whmobileprnt = ctx.mobileprnt_ctl
                   .Where(z => z.DEFAULT_NO == user.user_mac.MC_CODE).SingleOrDefault();

                auth_function auth = ctx.auth
                   .Where(z => z.USER_ID == user.USER_ID && z.FUNCTION_ID == "PDOPTM_WEB").SingleOrDefault();

                string def_printer = null;
                string wc_code = null;

                if (whmobileprnt == null)
                {
                    def_printer = "";
                }
                else
                {
                    def_printer = whmobileprnt.SERIES_NO;
                }

                if (auth == null)
                {
                    wc_code = "";
                }
                else
                {
                    wc_code = auth.DEPT_CODE;
                }



                if (user == null)
                {
                    throw new Exception("รหัสผู้ใช้หรือรหัสผ่านไม่ถูกต้อง / ไมได้กำนหด Machine");
                }
                else if (auth == null)
                {
                    throw new Exception("ยังไมได้กำนหด หน่วยงาน");
                }
                else
                {
                    if (!user.USER_PASSWORD.Equals(password))
                    {
                        throw new Exception("รหัสผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
                    }

                    if (!user.ACTIVE.Equals("Y"))
                    {
                        throw new Exception("สถานะผู้ใช้งานนี้ถูกยกเลิก");
                    }



                    if (!user.user_mac.STATUS.Equals("A"))
                    {
                        throw new Exception("ไม่มีการกำหนด Machine");
                    }
                }

                
                AuthenticationData data = new AuthenticationData()
                {
                    username = user.USER_ID,
                    name = user.USER_NAME,
                    dept_code = user.DEPT_CODE,
                    department = user.departments,
                    user_mac = user.user_mac,
                    def_printer = def_printer,
                    def_wc_code = wc_code,
                    statusId = user.ACTIVE,
                    menuGroups = new List<ModelViews.menuFunctionGroupView>(),
                };



               
                    data.menuGroups = getUserRole((string)user.USER_ID);
                


                return data;
            }

        }

        public List<menuFunctionGroupView> getUserRole(string userId)
        {
            using (var ctx = new ConXContext())
            {
                //List<su_user_role> user_role = ctx.user_role.SqlQuery("Select USER_ID , ROLE_ID ,ACTIVE  from su_user_role where user_id = :param1", new OracleParameter("param1", userId)).ToList();
               List<su_menu> menu = ctx.job.SqlQuery("select  LEVEL , MENU_ID, MENU_NAME , MENU_TYPE, LINK_NAME , MAIN_MENU from su_menu where EXISTS   (select MENU_ID  from su_role_menu  WHERE MENU_ID= SU_MENU.MENU_ID AND EXISTS (select role_id from su_user_role  WHERE ROLE_ID= SU_ROLE_MENU.ROLE_ID  and user_id = :param1)) CONNECT BY PRIOR MENU_ID = MAIN_MENU START WITH  menu_id ='MOB0000000' ORDER BY MENU_ID", new OracleParameter("param1", userId)).ToList();
                
               List<menuFunctionView> functionViews = new List<menuFunctionView>();


                foreach (var x in menu)
                {
                        
                            menuFunctionView view = new menuFunctionView()
                            {
                                menuFunctionGroupId = x.MAIN_MENU,
                                menuFunctionId = x.MENU_ID,
                                menuFunctionName = x.MENU_NAME,
                                iconName = "store",
                                menuURL = x.LINK_NAME,
                            };


                            functionViews.Add(view);    
                  
                }



                List<menuFunctionGroupView> groupView = new List<menuFunctionGroupView>();

                foreach (var x in menu)
                {
                    menuFunctionGroupView view = new menuFunctionGroupView()
                    {
                        menuFunctionGroupId = x.MENU_ID,
                        menuFunctionGroupName = x.MENU_NAME,
                        menuFunctionList = functionViews
                                .Where(o => o.menuFunctionGroupId == x.MENU_ID)
                                .ToList()

                    };

                    if (x.MENU_TYPE == "M" && x.MENU_ID != "MOB0000000")
                    {
                        groupView.Add(view);
                    }
                }

                return groupView;


            }
        }


    }
}