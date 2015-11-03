using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MySql.Data.MySqlClient;
using System.DirectoryServices;
using System.Xml;
using System.IO;
using System.Net;
using System.ServiceModel.Channels;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Data;
using DataContractAssembly;

namespace GeneralWcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class GeneralService : IGeneralService,IDisposable
    {
        #region local variable
        private OperationContext operationContext;
        private bool _bDebug = false;
        private FuncTagInfo oTagInfo;
        private MySqlConnection oDbCon = null;
        private MySqlCommand oDbCmd = null;
        private MySqlDataReader oDbReader = null;
        CallBackData oData = null;
        //string _sIPV4 = string.Empty;
        string _sPc = string.Empty;

        private string sConnectionString =
                        "server=127.0.0.1;" +//192.168.0.91
                       "uid=mesadmin;" +
                       "pwd=1qAZ2wSX;" +
                       "database=js_mes;";
        #endregion

        #region constractor
        public GeneralService()
        {
            oData = new CallBackData();
            oData.bIsOK = true;

            #region open mysql connection
            try
            {
                oDbCon = new MySqlConnection(sConnectionString);
                oDbCon.Open();
                oDbCmd = oDbCon.CreateCommand();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        oData.bIsOK = false;
                        oData.Result = "Cannot connect to server.  Contact administrator";
                        //return oData;
                        break;
                    case 1045:
                        oData.bIsOK = false;
                        oData.Result = "Invalid username/password, please try again";
                        //return oData;
                        break;
                }
            }
            #endregion
            
        }
        public GeneralService(bool bDebug)
        {
            _bDebug = bDebug;
            oData = new CallBackData();
            oData.bIsOK = true;
        }

        public void Dispose()
        {
            //if (OperationContext.Current.Channel!=null)
            //{
            //    if (OperationContext.Current.Channel.State==CommunicationState.Faulted)
            //    {
                
            //    }
            //}
        }


        #endregion


        public CallBackData Request(FuncTagInfo _Request)
        {
            if (!oData.bIsOK)
            {
                return oData;
            }

            oTagInfo = _Request;

            try
            {
                this.GetType().GetMethod(oTagInfo.FuncName).Invoke(this, null);
                
                return oData;
            }
            finally
            {
                oDbCmd.Dispose();
                oDbCmd = null;
                oDbCon.Close();
                oDbCon = null;
            }

        }

        #region Client Request Functions
        
        /// <summary>
        /// 机台设定
        /// </summary>
        public void MachineMaintain()
        {
            int i = 0;
            string machine_id = oTagInfo.InputParams[i++];
            string machine_name = oTagInfo.InputParams[i++];
            string manufacturer = oTagInfo.InputParams[i++];
            string worksite_id = oTagInfo.InputParams[i++];
            string remark = oTagInfo.InputParams[i++];
            string workshop = oTagInfo.InputParams[i++];

            try
            {
                string soperator = oTagInfo.UserName;

                string sql =
                    "insert into df_eqp_info(machine_id,workshop,machine_name,manufacturer,actived,worksite_id,remark,operator,op_time)" +
                    " values('" + machine_id + "','" + workshop + "','" + machine_name + "','" + manufacturer +
                    "','T','" + worksite_id + "','" + remark + "','" + soperator + "',now())";
                oDbCmd.CommandText = sql;
                oDbCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                oData.bIsOK = false;
                oData.Result = e.Message;
            }
        }

        /// <summary>
        /// 创建工单
        /// </summary>
        public void CreateWO()
        {
            string workshop = oTagInfo.InputParams[0];
            string workorder_type = oTagInfo.InputParams[1];
            string module_type = oTagInfo.InputParams[2];
            string pid_type = oTagInfo.InputParams[3];
            string sale_order = oTagInfo.InputParams[4];
            string customer = oTagInfo.InputParams[5];
            string workorder_qty = oTagInfo.InputParams[6];
            string workflow_id = oTagInfo.InputParams[7];
            string workorder_seq = oTagInfo.InputParams[8];
            string module_color=oTagInfo.InputParams[9];
            string cristal_type = oTagInfo.InputParams[10];
            string cell_size=oTagInfo.InputParams[11];
            string cell_consumption=oTagInfo.InputParams[12];
            string cell_type=oTagInfo.InputParams[13];
            string product_no1 = oTagInfo.InputParams[14];
            string product_no2=oTagInfo.InputParams[15];


            try
            {
                #region 检查工单次数
                string workorder_prefix = workorder_type + "-" + DateTime.Now.ToString("yyMMdd");
                int iWoSeq = Convert.ToInt32(workorder_seq);

                string workorder_flow_prefix = "WO_" + DateTime.Now.ToString("yyMMdd");
                string sql = "select lastcount from rt_prefix_lastcount where prefix='" + workorder_flow_prefix + "'";
                oDbCmd.CommandText = sql;
                oDbReader = oDbCmd.ExecuteReader();

                if (!oDbReader.HasRows)
                {
                    if (1 != iWoSeq)
                    {
                        oData.bIsOK = false;
                        oData.Result = "工单次数应从1开始,您的输入有误!";
                        return;
                    }
                }
                else
                {
                    oDbReader.Read();
                    int workorder_last_seq = Convert.ToInt32(oDbReader[0]);

                    if (workorder_last_seq != iWoSeq - 1)
                    {
                        oData.bIsOK = false;
                        oData.Result = "上一个工单版本为\"" + workorder_last_seq+"\",您的输入有误!";
                        return;
                    }
                }
                oDbReader.Close();

                #endregion

                string workorder_id = workorder_prefix + iWoSeq.ToString("000") ;
                string currentUser = oTagInfo.UserName;

                sql =
                    "INSERT INTO `js_mes`.`df_wo_info` (`workorder_id`,`workshop`,`workorder_seq`, `flow_id`, `qty`, `workorder_type`," +
                    "`module_type`, `pid_type`, `module_color`, `cristal_type`, `cell_size`, `cell_consumption`, `cell_type`," +
                    "`product_no1`, `product_no2`, `sale_order`, `customer`, `Create_Date`, `LastUpdate_Date`, `OP_ID`," +
                    "`UOP_ID`, `sys_workorder`) VALUES ('" + workorder_id + "', '" + workshop + "','" + workorder_seq + "', '" + workflow_id +
                    "', '" + workorder_qty + "', '" + module_type + "', '" + module_type + "', '" + pid_type + "', '" + module_color +
                    "', '" + cristal_type + "', '" + cell_size + "', '" + cell_consumption + "', '" + cell_type + "', '" + product_no1 +
                    "', '" + product_no2 + "', '" + sale_order + "', '" + customer + "', now(), null, '" + currentUser + 
                    "',null,null)";

                oDbCmd.CommandText = sql;
                oDbCmd.ExecuteNonQuery();

                #region 更新工单次数
                if (workorder_seq=="1")
                {
                    sql =
                        "insert into rt_prefix_lastcount(prefix,lastcount) values('" + workorder_flow_prefix + "',1)";
                }
                else
                {
                    sql =
                        "update rt_prefix_lastcount set lastcount='" + workorder_seq + "' where prefix='" + workorder_flow_prefix + "'";
                }
                oDbCmd.CommandText = sql;
                oDbCmd.ExecuteNonQuery();

                
                #endregion
            }
            catch (Exception e)
            {
                oData.bIsOK = false;
                oData.Result = e.Message;
            }
        }


        /// <summary>
        /// 电池片分批
        /// </summary>
        public void CreateCellBatchNo()
        {
            int i = 0;
            string workshop = oTagInfo.InputParams[i++];                   //车间编号
            string cell_erp_lot = oTagInfo.InputParams[i++];             //电池erp批号
            string cell_color = oTagInfo.InputParams[i++];                //片源颜色
            string cell_eff_low = oTagInfo.InputParams[i++];                   //片源效率低    
            string cell_eff_up = oTagInfo.InputParams[i++];                 //片源效率高
            string cell_power = oTagInfo.InputParams[i++];                   //单片功率
            string cell_grade = oTagInfo.InputParams[i++];              //片源等级
            string cell_batch_count = oTagInfo.InputParams[i++];                //分批数量
            string cell_comp = oTagInfo.InputParams[i++];                //电池厂商
            //string workorder_seq = oTagInfo.InputParams[8];
            //string module_color = oTagInfo.InputParams[9];
            //string cristal_type = oTagInfo.InputParams[10];
            //string cell_size = oTagInfo.InputParams[11];
            //string cell_consumption = oTagInfo.InputParams[12];
            //string cell_type = oTagInfo.InputParams[13];
            //string product_no1 = oTagInfo.InputParams[14];
            //string product_no2 = oTagInfo.InputParams[15];
            string cell_eff=cell_eff_low+"%-"+cell_eff_up+"%";

            try
            {
                #region 获取分批流水号
                //string cell_batch_prefix = cell_erp_lot + workshop + DateTime.Now.ToString("yyMMdd");
                string cell_flow_prefix = "Cell_" + workshop + DateTime.Now.ToString("yyMMdd");

                string sql = "select lastcount from rt_prefix_lastcount where prefix='" + cell_flow_prefix + "'";
                oDbCmd.CommandText = sql;
                oDbReader = oDbCmd.ExecuteReader();

                int lastcount = 0;
                if (!oDbReader.HasRows)
                {
                    lastcount = 1;
                }
                else
                {
                    oDbReader.Read();
                    lastcount = Convert.ToInt32(oDbReader[0])+1;
                    
                }
                oDbReader.Close();
                #endregion

                #region 更新流水码
                if (lastcount == 1)
                {
                    sql =
                        "insert into rt_prefix_lastcount(prefix,lastcount) values('" + cell_flow_prefix + "',1)";
                }
                else
                {
                    sql =
                        "update rt_prefix_lastcount set lastcount='" + lastcount.ToString() + "' where prefix='" + cell_flow_prefix + "'";
                }
                oDbCmd.CommandText = sql;
                oDbCmd.ExecuteNonQuery();


                #endregion

                string cell_batch_prefix = workshop + DateTime.Now.ToString("yyMMdd");
                string cell_batch_no = cell_batch_prefix + lastcount.ToString("000");
                string currentUser = oTagInfo.UserName;

                sql =
                    "INSERT INTO `js_mes`.`rt_cell_distribute` (`cell_batch_no`, `cell_erp_lot`, `batch_count`,`cell_count_remain`," +
                    "`workshop_id`, `cell_color`, `cell_eff_low`, `cell_eff_up`, `cell_eff`, `cell_power`, `cell_grade`," +
                    "`create_date`, `op`, `cell_comp`) VALUES ('" + cell_batch_no + "', '" + cell_erp_lot + "', '" + cell_batch_count + "', '" + cell_batch_count +
                    "', '" + workshop + "', '" + cell_color + "', '" + cell_eff_low + "', '" + cell_eff_up +
                    "', '" + cell_eff + "', '" + cell_power + "', '" + cell_grade + "', now(), '" + currentUser + "','" + cell_comp + "')";

                oDbCmd.CommandText = sql;
                oDbCmd.ExecuteNonQuery();

                oData.Result = cell_batch_no;
                
            }
            catch (Exception e)
            {
                oData.bIsOK = false;
                oData.Result = e.Message;
            }
        }

        /// <summary>
        /// 插入叠层信息
        /// </summary>
        public void InsertModuleLayupInfo()
        {
            int i=0;
            string module = oTagInfo.InputParams[i++];
            string currentSite=oTagInfo.InputParams[i++];
            string eqp = oTagInfo.InputParams[i++];
            string glassComp = oTagInfo.InputParams[i++];
            string tptComp = oTagInfo.InputParams[i++];
            string evaComp = oTagInfo.InputParams[i++];
            string glassDesc = oTagInfo.InputParams[i++];
            string tptDesc = oTagInfo.InputParams[i++];
            string evaDesc = oTagInfo.InputParams[i++];
            string user = oTagInfo.InputParams[i++];
            string workshop = oTagInfo.InputParams[i++];
            string isNight = oTagInfo.InputParams[i++]; 

            //先判定组件是否可以过站
            string sCurrentSiteName = string.Empty;
            string moduleSite = getCurrentSite(module, ref sCurrentSiteName);
            if (moduleSite!=currentSite)
            {
                oData.bIsOK = false;
                oData.Result = "组件当前站点是：\n" + sCurrentSiteName;
                return;
            }
            //string sql=

            
            string sql = string.Empty;
            //try
            //{
            //    //插入组件叠层信息
            sql=
                "insert into rt_worksite_layup(module_id,eqp_id,glass_comp,tpt_comp,eva_comp,glass_desc," +
                " tpt_desc,eva_desc,create_time,op,workshop) values('" + module + "','" + eqp + "','" + glassComp
                + "','" + tptComp + "','" + evaComp + "','" + glassDesc + "','" + tptDesc + "','" + evaDesc +
                "',now(),'" + user + "','" + workshop + "')";

                //oDbCmd.CommandText = sql;
                //oDbCmd.ExecuteNonQuery();

                
            //}
            //catch (Exception e)
            //{
            //    oData.bIsOK = false;
            //    oData.Result = e.Message;
            //}

            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }

            if (isNight.ToUpper()=="NIGHT")
            {
                sql =
                  "   select count(*) layup_count from rt_worksite_layup                     " +
                  "   where eqp_id='" + eqp + "'                                               " +
                  "   and create_time>=CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 19:20:00')     " +
                  "   and create_time<CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 23:59:59')      ";

            }
            else if (isNight.ToUpper() == "DAY")
            {
                sql =
                 "   select count(*) layup_count from rt_worksite_layup                     " +
                 "   where eqp_id='" + eqp + "'                                               " +
                 "   and create_time>=CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 07:20:00')     " +
                 "   and create_time<CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 19:20:00')      ";

            }
            else
            {
                sql =
                "   select count(*) layup_count from rt_worksite_layup                     " +
                "   where eqp_id='" + eqp + "'                                               " +
                "   and create_time>=DATE_SUB(CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 19:20:00'), INTERVAL 1 DAY)     " +
                "   and create_time<CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 07:20:00')      ";
            }

            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                oData.Result = Convert.ToString(dr[0]);
            }
            
            //更新站点信息
            updateWorksite(module);

            
        }


        /// <summary>
        /// 插入清洗信息
        /// </summary>
        public void InsertModuleCleanInfo()
        {
            int i = 0;
            string module = oTagInfo.InputParams[i++];
            string currentSite = oTagInfo.InputParams[i++];
            string eqp = oTagInfo.InputParams[i++];
            //string glassComp = oTagInfo.InputParams[i++];
            //string tptComp = oTagInfo.InputParams[i++];
            //string evaComp = oTagInfo.InputParams[i++];
            //string glassDesc = oTagInfo.InputParams[i++];
            //string tptDesc = oTagInfo.InputParams[i++];
            //string evaDesc = oTagInfo.InputParams[i++];
            string user = oTagInfo.InputParams[i++];
            string workshop = oTagInfo.InputParams[i++];
            string isNight = oTagInfo.InputParams[i++];

            //先判定组件是否可以过站
            string sCurrentSiteName = string.Empty;
            string moduleSite = getCurrentSite(module, ref sCurrentSiteName);
            if (moduleSite != currentSite)
            {
                oData.bIsOK = false;
                oData.Result = "组件当前站点是：\n" + sCurrentSiteName;
                return;
            }
            //string sql=


            string sql = string.Empty;
            //try
            //{
            //    //插入组件清洗信息
            sql =
                "insert into rt_worksite_clean(module_id,eqp_id,create_time,op,workshop) values('" + module + "','" + eqp +
                "',now(),'" + user + "','" + workshop + "')";

            //oDbCmd.CommandText = sql;
            //oDbCmd.ExecuteNonQuery();


            //}
            //catch (Exception e)
            //{
            //    oData.bIsOK = false;
            //    oData.Result = e.Message;
            //}

            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }

            if (isNight.ToUpper() == "NIGHT")
            {
                sql =
                  "   select count(*) layup_count from rt_worksite_clean                     " +
                  "   where eqp_id='" + eqp + "'                                               " +
                  "   and create_time>=CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 19:20:00')     " +
                  "   and create_time<CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 23:59:59')      ";

            }
            else if (isNight.ToUpper() == "DAY")
            {
                sql =
                 "   select count(*) layup_count from rt_worksite_clean                     " +
                 "   where eqp_id='" + eqp + "'                                               " +
                 "   and create_time>=CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 07:20:00')     " +
                 "   and create_time<CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 19:20:00')      ";

            }
            else
            {
                sql =
                "   select count(*) layup_count from rt_worksite_clean                     " +
                "   where eqp_id='" + eqp + "'                                               " +
                "   and create_time>=DATE_SUB(CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 19:20:00'), INTERVAL 1 DAY)     " +
                "   and create_time<CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 07:20:00')      ";
            }

            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                oData.Result = Convert.ToString(dr[0]);
            }

            //更新站点信息
            updateWorksite(module);
        }


        /// <summary>
        /// 插入装框信息
        /// </summary>
        public void InsertMoudleFrameInfo()
        {
            int i = 0;
            string module = oTagInfo.InputParams[i++];
            string currentSite = oTagInfo.InputParams[i++];
            string eqp = oTagInfo.InputParams[i++];
            string jobxComp = oTagInfo.InputParams[i++];
            string frameComp = oTagInfo.InputParams[i++];
            string glueComp = oTagInfo.InputParams[i++];
            string jobxDesc = oTagInfo.InputParams[i++];
            string frameDesc = oTagInfo.InputParams[i++];
            string glueDesc = oTagInfo.InputParams[i++];
            string user = oTagInfo.InputParams[i++];
            string workshop = oTagInfo.InputParams[i++];
            string isNight = oTagInfo.InputParams[i++];
            string fram_glue_comp = oTagInfo.InputParams[i++];
            string fram_glue_desc = oTagInfo.InputParams[i++]; 

            //先判定组件是否可以过站
            string sCurrentSiteName = string.Empty;
            string moduleSite = getCurrentSite(module, ref sCurrentSiteName);
            if (moduleSite != currentSite)
            {
                oData.bIsOK = false;
                oData.Result = "组件当前站点是：\n" + sCurrentSiteName;
                return;
            }

            string sql = string.Empty;
            //try
            //{
                //插入组件叠层信息
                sql =
                    "insert into rt_worksite_frame(module_id,eqp_id,jobx_comp,frame_comp,glue_comp,jbox_desc," +
                    " frame_desc,glue_desc,create_time,op,workshop,frame_glue_comp,frame_glue_desc) values('" + module + "','" + eqp + "','" + jobxComp
                    + "','" + frameComp + "','" + glueComp + "','" + jobxDesc + "','" + frameDesc + "','" + glueDesc +
                    "',now(),'" + user + "','" + workshop + "','" + fram_glue_comp + "','" + fram_glue_desc + "')";

                //oDbCmd.CommandText = sql;
                //oDbCmd.ExecuteNonQuery();

                
                

            //}
            //catch (Exception e)
            //{
            //    oData.bIsOK = false;
            //    oData.Result = e.Message;
            //}

            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }

            if (isNight.ToUpper() == "NIGHT")
            {
                sql =
                  "   select count(*) layup_count from rt_worksite_frame                     " +
                  "   where eqp_id='" + eqp + "'                                               " +
                  "   and create_time>=CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 19:20:00')     " +
                  "   and create_time<CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 23:59:59')      ";

            }
            else if (isNight.ToUpper() == "DAY")
            {
                sql =
                 "   select count(*) layup_count from rt_worksite_frame                     " +
                 "   where eqp_id='" + eqp + "'                                               " +
                 "   and create_time>=CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 07:20:00')     " +
                 "   and create_time<CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 19:20:00')      ";

            }
            else
            {
                sql =
                "   select count(*) layup_count from rt_worksite_frame                     " +
                "   where eqp_id='" + eqp + "'                                               " +
                "   and create_time>=DATE_SUB(CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 19:20:00'), INTERVAL 1 DAY)     " +
                "   and create_time<CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 07:20:00')      ";
            }

            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                oData.Result = Convert.ToString(dr[0]);
            }


            //更新站点信息
            updateWorksite(module);
        }


        /// <summary>
        /// 获取功率模板
        /// </summary>
        public void GetPowerTemplate()
        {
            int i = 0;
            string sPowerTemplate = oTagInfo.InputParams[i++];
            string sql = string.Empty;

            sql =
             "   SELECT power_grade,upperpower,       " +
             "   lowerpower,upperimp,lowerimp                   " +
             "   FROM js_mes.df_module_power                    " +
             "   where q1_id='" + sPowerTemplate + "'           ";

            XmlDocument oReplyResult = new XmlDocument();
            XmlDeclaration oDocDec = oReplyResult.CreateXmlDeclaration("1.0", "UTF-8", null);
            oReplyResult.AppendChild(oDocDec);
            XmlNode oPList = oReplyResult.CreateElement("Powers");
            oReplyResult.AppendChild(oPList);


            oDbCmd.CommandText = sql;
            MySqlDataReader oPReader = oDbCmd.ExecuteReader();
            XmlNode MenuItem = null;

            if (!oPReader.HasRows)
            {
                oData.Result = "没有此功率信息！";
                return;
            }
            while (oPReader.Read())
            {
                MenuItem = oReplyResult.CreateElement("PowerItem");

                XmlAttribute oPower_grade = oReplyResult.CreateAttribute("POWER_GRADE");
                oPower_grade.Value = Convert.ToString(oPReader["power_grade"]);
                MenuItem.Attributes.Append(oPower_grade);

                //XmlAttribute oImp_grade = oReplyResult.CreateAttribute("IMP_GRADE");
                //oImp_grade.Value = Convert.ToString(oPReader["imp_grade"]);
                //MenuItem.Attributes.Append(oImp_grade);

                XmlAttribute oUpperpower = oReplyResult.CreateAttribute("UPPERPOWER");
                oUpperpower.Value = Convert.ToString(oPReader["upperpower"]);
                MenuItem.Attributes.Append(oUpperpower);

                XmlAttribute oLowerpower = oReplyResult.CreateAttribute("LOWERPOWER");
                oLowerpower.Value = Convert.ToString(oPReader["lowerpower"]);
                MenuItem.Attributes.Append(oLowerpower);

                XmlAttribute oUpperimp = oReplyResult.CreateAttribute("UPPERIMP");
                oUpperimp.Value = Convert.ToString(oPReader["upperimp"]);
                MenuItem.Attributes.Append(oUpperimp);

                XmlAttribute oLowerimp = oReplyResult.CreateAttribute("LOWERIMP");
                oLowerimp.Value = Convert.ToString(oPReader["lowerimp"]);
                MenuItem.Attributes.Append(oLowerimp);

                oPList.AppendChild(MenuItem);

            }

            StringWriter oSW = new StringWriter();
            XmlTextWriter oXTW = new XmlTextWriter(oSW);
            oReplyResult.WriteTo(oXTW);

            oData.Result = oSW.ToString();


        }

        /// <summary>
        /// 设置功率参数
        /// </summary>
        public void SetPowerGrade()
        {
            int i = 0;
            string workorder = oTagInfo.InputParams[i++];           //工单
            string packcount = oTagInfo.InputParams[i++];           //包装数量
            string saleorder = oTagInfo.InputParams[i++];           //订单

            int iPowerInfoStartIdx = 3;
            int sequenceStart = 0;

            string sql = string.Empty;
            sql = "delete from df_module_power where q1_id='" + workorder + "';";
            for (int j = iPowerInfoStartIdx; j < oTagInfo.InputParams.Count; j++)
            {
                string[] PowerInfo = oTagInfo.InputParams[j].Split('|');
                //==>判定有无电流分档
                if (PowerInfo.Count() < 4)
                {//不分电流档
                    string powerGrade = PowerInfo[0];
                    string upPower = PowerInfo[1];
                    string lowPower = PowerInfo[2];
                    sql = sql +
                       " insert into df_module_power(Q1_id,grade_type,nameplate_type,cell_size,power_grade,imp_grade,upperpower,  " +
                       " lowerpower,upperimp,lowerimp,sequence) values('" + workorder + "','NORMAL','TCZ_UL_ENG_NORMAL','156','" +
                       powerGrade + "','I0','" + upPower + "','" + lowPower + "','100','0','" + sequenceStart.ToString() + "');";

                    sequenceStart++;
                }
                else
                {
                    bool isFstRow = true;
                    string previousLowImp = string.Empty;
                    string previousUpImp = string.Empty;
                    string currentLowImp = string.Empty;
                    string currentUpImp = string.Empty;

                    string powerGrade = PowerInfo[0];
                    string lowPower = PowerInfo[1];
                    string upPower = PowerInfo[2];

                    for (int k = 3; k < PowerInfo.Count(); k++)
                    {
                        if (isFstRow)
                        {
                            isFstRow = false;
                            currentLowImp = "0";
                        }
                        else
                        {
                            currentLowImp = previousUpImp;
                        }

                        currentUpImp = PowerInfo[k];
                        previousUpImp = currentUpImp;
                        sql=sql+
                            " insert into df_module_power(Q1_id,grade_type,nameplate_type,cell_size,power_grade,imp_grade,upperpower,  " +
                       " lowerpower,upperimp,lowerimp,sequence) values('" + workorder + "','NORMAL','TCZ_UL_ENG_NORMAL','156','" +
                       powerGrade + "','I" + (k - 2).ToString() + "','" + upPower + "','" + lowPower + "','" + currentUpImp +
                       "','" + currentLowImp + "','" + sequenceStart.ToString() + "');";

                        sequenceStart++;
                    }

                }
            }
            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        
        /// <summary>
        /// 获取车间的工单好号
        /// </summary>
        public void GetWorkOrder()
        {
            int i = 0;
            string workshop=oTagInfo.InputParams[i++];

            string sql =
             "   SELECT workorder_id FROM js_mes.df_wo_info " +
             "   where workshop='"+workshop+"'                        " +
             "   and state='open'                           ";

            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    oData.bIsOK = false;
                    oData.Result = "无法找到合适的工单！";
                    return;
                }

                while (dr.Read())
                {
                    oData.Result = oData.Result + (string)dr[0] + "|";
                }
            }
        
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public void GetModuleBingingInfo()
        {
            int i = 0;
            string module_id=oTagInfo.InputParams[i++];

            string sql=string.Empty;

            #region 检查组件号的站点
            sql =
              "  select current_flow_id                         " +
              "  from lotbasis                                  " +
              "  where module_id='" + module_id + "'             ";

            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    oData.bIsOK = false;
                    oData.Result = "无法找到组件信息！";
                    return;
                }
                
                dr.Read();
                string current_flow_id=Convert.ToString(dr[0]);
                if (current_flow_id!="2")
                {
                    oData.bIsOK = false;
                    oData.Result = "组件不在焊接站点，不能解绑！";
                    return;
                }
            }
            #endregion                                          


            sql =
             "    SELECT a.module_id,b.cell_batch_no,                   " +
             "    c.machine_name,d.cell_consumption                     " +
             "    FROM lotbasis a                                       " +
             "    left join rt_worksite_weld b                          " +
             "    on a.module_id=b.module_id                            " +
             "    and b.usedflag='1'                                    " +
             "    left join df_eqp_info c                               " +
             "    on b.eqp_id=c.machine_id                              " +
             "    left join df_wo_info d                                " +
             "    on a.workorder_id=d.workorder_id                      " +
             "    where a.module_id='" + module_id + "'                  ";

            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    oData.bIsOK = false;
                    oData.Result = "无法找到组件信息！";
                    return;
                }

                int iCount = 0;
                //RowData oRawData = null;
                //oData.RowDatas = new List<RowData>();
                while (dr.Read())
                {
                    iCount++;
                    //oRawData = new RowData();
                    //setReportData40(ref dr, ref oRawData);
                    //oData.RowDatas.Add(oRawData);
                    oData.Result = Convert.ToString(dr[0]) + "|" + Convert.ToString(dr[1]) + "|" + Convert.ToString(dr[2]) + "|" + Convert.ToString(dr[3]);
                }

                if (iCount>1)
                {
                    oData.bIsOK = false;
                    oData.Result = "无法找到合适的批次信息！";
                    return;
                }
            }
        }

        /// <summary>
        /// 解除组件号和电池分批号之间的绑定关系
        /// </summary>
        public void ReleaseModuleBingingInfo()
        {
            string op=oTagInfo.InputParams[0];                  
            string cell_batch = oTagInfo.InputParams[1];
            string unBindingCellCount = oTagInfo.InputParams[2];        


            string sql=string.Empty;
            for (int i = 3; i < oTagInfo.InputParams.Count; i++)
			{
			    string module_id=oTagInfo.InputParams[i];

                sql =sql+
               " update rt_worksite_weld set usedflag='0',unbindingop='"+op+"',unBindingTime=now() " +
               " where module_id='"+module_id+"'            " +
               " and usedflag='1';                           ";
                sql = sql +
                 "   update lotbasis set current_flow_id=current_flow_id-1,     " +
                 "   next_flow_id=next_flow_id-1                                " +
                 "   where module_id='" + module_id + "';                        ";
			}
            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }

            #region 更新电池批号的数量
            sql =
               " update rt_cell_distribute                          " +
               " set cell_count_remain=cell_count_remain+" + unBindingCellCount +
               " where cell_batch_no='" + cell_batch + "'           ";

            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }

            #endregion
        }

        private string getCurrentSite(string module,ref string sCurrentSiteName)
        {
            string sql =

            "    SELECT a.module_id,c.flow_id_name,c.remark      " +
            "    FROM lotbasis a                        " +
            "    left join df_wo_info b                 " +
            "    on a.workorder_id=b.workorder_id       " +
            "    left join df_workflow c                " +
            "    on b.flow_id=c.flow_id                 " +
            "    and a.next_flow_id=c.flow_id_idx       " +
            "    where a.module_id='" + module + "'   ";

            MySqlDataReader dr = null;
            try
            {
                oDbCmd.CommandText = sql;
                dr = oDbCmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    return "过站信息检查失败";
                }
                else
                {
                    dr.Read();
                    if (!Convert.IsDBNull(dr[1]))
                    {
                        sCurrentSiteName = Convert.ToString(dr[2]);
                        return (string)dr[1];
                    }
                    else
                    {
                        return "过站信息检查失败";
                    }
                }
            }
            catch (Exception)
            {
                return "此组件有异常，请联系mes工程师检查";
            }
            finally
            {
                dr.Close();
            }
            
        }

        /// <summary>
        /// 更新站点信息
        /// </summary>
        /// <param name="sModuleID"></param>
        private void updateWorksite(string sModuleID)
        {
            try
            {
                oDbCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oDbCmd.CommandText = "updateModuleFlowID";

                oDbCmd.Parameters.AddWithValue("?module", sModuleID);
                oDbCmd.Parameters["?module"].Direction = ParameterDirection.Input;

                oDbCmd.Parameters.AddWithValue("?updateType", "OK");
                oDbCmd.Parameters["?updateType"].Direction = ParameterDirection.Input;

                oDbCmd.Parameters.AddWithValue("?isRework", false);
                oDbCmd.Parameters["?isRework"].Direction = ParameterDirection.Input;

                //oDbCmd.Parameters.Add(new MySqlParameter("?period_time", MySqlDbType.Double));
                //oDbCmd.Parameters["?period_time"].Direction = ParameterDirection.Output;
                oDbCmd.ExecuteNonQuery();
                oDbCmd.CommandType = System.Data.CommandType.Text;
            }
            catch (Exception e)
            {
                oData.bIsOK = false;
                oData.Result = e.Message;
            }
            

        }



        #endregion

        #region general functions
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public void getCurrentUser()
        {
            string sql = "select user_name from rt_user_ipmapping where user_pc='" + oTagInfo.ComputerName + "'";
            oDbCmd.CommandText = sql;
            oDbReader = oDbCmd.ExecuteReader();

            oDbReader.Read();
            string soperator = Convert.ToString(oDbReader[0]);
            oDbReader.Close();

            oData.Result = soperator;
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        public void AuthenticateUser()
        {
            string UserID=oTagInfo.InputParams[0];
            string Password = oTagInfo.InputParams[1];

            string _result = "";

            if (UserID == "")
            {
                UserID = " ";
            }
            if (Password == null)
            {
                Password = " ";
            }

            if (UserID.IndexOf("@") > -1)
            {
                UserID = UserID.Substring(0, UserID.IndexOf("@"));
            }
            if (UserID.IndexOf("\\") > -1)
            {
                UserID = UserID.Substring(UserID.IndexOf("\\") + 1);
            }

            try
            {
                string sSql =
                    "   SELECT   NICKNAME,IsDomainUser,PWD " +
                    "   FROM   permission_usergroup  " +
                    "   WHERE  STATE = 'T'                 " +
                    "       AND USERNAME = '" + UserID + "';    ";

                oDbCmd.CommandText = sSql;
                MySqlDataReader oPReader = oDbCmd.ExecuteReader();

                if (!oPReader.HasRows)
                {
                    _result = UserID + "@";
                }
                while (oPReader.Read())
                {
                    if (((string)oPReader["IsDomainUser"]) == "T")
                    {
                        DirectoryEntry entry = new DirectoryEntry("LDAP://trinasolar.com",
                            UserID, Password);
                        object nativeObject = entry.NativeObject;
                        _result = UserID + "@Ok";
                    }
                    else
                    {
                        if ( Convert.ToString(oPReader["PWD"]) == Password)
                        {
                            string nickName = (string)oPReader["NICKNAME"];
                            _result = UserID + "|"+nickName+"@Ok";
                        }
                        else
                        {
                            _result = UserID + "@";
                        }
                    }
                }

                oData.Result = _result;
            }
            catch (DirectoryServicesCOMException _ex)
            {
                _result = UserID + "@" + _ex.ToString();
                oData.bIsOK = false;
                oData.Result = _result;
            }
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="_sUserID"></param>
        public void GetMenuItem()
        {
            string _sUserID = oTagInfo.InputParams[0];
            try
            {
                XmlDocument oReplyResult = new XmlDocument();
                XmlDeclaration oDocDec = oReplyResult.CreateXmlDeclaration("1.0", "UTF-8", null);
                oReplyResult.AppendChild(oDocDec);
                XmlNode oPList = oReplyResult.CreateElement("Menus");
                oReplyResult.AppendChild(oPList);
            
                string sSql =
                 "   SELECT DISTINCT                                " +
                 "       a.LOCALACCESSNAME,                         " +
                 "       A.LOCALDISPLAYNAME,                        " +
                 "       A.FUNCTIONNAME,a.assembly                  " +
                 "   FROM                                           " +
                 "       DF_CONFIG_FUNCTIONS a,                     " +
                 "       permission_usergroup b,                    " +
                 "       permission_groupfunction c,                " +
                 "       df_user_usergroup d                        " +
                 "   WHERE                                          " +
                 "       A.FUNCTIONNAME = C.FUNCTIONNAME            " +
                 "           AND d.GROUPNAME = C.GROUPNAME          " +
                 "           and b.username=d.username              " +
                 "           AND b.state = 'T'                      " +
                 "           AND b.USERNAME = '" + _sUserID + "'    " +
                 "           and a.isreport='N'                     " +
                 "   ORDER BY LOCALACCESSNAME , LOCALDISPLAYNAME    ";

                oDbCmd.CommandText = sSql;
                MySqlDataReader oPReader = oDbCmd.ExecuteReader();
                XmlNode MenuItem = null;

                while (oPReader.Read())
                {
                    MenuItem = oReplyResult.CreateElement("MenuItem");

                    XmlAttribute oAccessName = oReplyResult.CreateAttribute("AccessName");
                    oAccessName.Value = (string)oPReader["LOCALACCESSNAME"];
                    MenuItem.Attributes.Append(oAccessName);

                    XmlAttribute oDisplayName = oReplyResult.CreateAttribute("DisplayName");
                    oDisplayName.Value = (string)oPReader["LOCALDISPLAYNAME"];
                    MenuItem.Attributes.Append(oDisplayName);

                    XmlAttribute oFuncName = oReplyResult.CreateAttribute("FunctionName");
                    oFuncName.Value = (string)oPReader["FUNCTIONNAME"];
                    MenuItem.Attributes.Append(oFuncName);

                    XmlAttribute oAssembly = oReplyResult.CreateAttribute("Assembly");
                    oAssembly.Value = (string)oPReader["assembly"];
                    MenuItem.Attributes.Append(oAssembly);

                    oPList.AppendChild(MenuItem);

                }

                StringWriter oSW = new StringWriter();
                XmlTextWriter oXTW = new XmlTextWriter(oSW);
                oReplyResult.WriteTo(oXTW);

                oData.Result = oSW.ToString();
            }
            catch (Exception ex)
            {
                oData.bIsOK = false;
                oData.Result = ex.Message;
            }
        }

        /// <summary>
        /// 记录主机当前登录用户
        /// </summary>
        public void setCurrentUserInfo()
        {
            try
            {

                string sql = "delete from rt_user_ipmapping where user_pc='" + oTagInfo.ComputerName + "'";

                oDbCmd.CommandText = sql;
                oDbCmd.ExecuteNonQuery();

                sql = "insert into rt_user_ipmapping(user_pc,user_name) values('" + oTagInfo.ComputerName
                        + "','" + oTagInfo.UserName + "')";
                oDbCmd.CommandText = sql;
                oDbCmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                oData.bIsOK = false;
                oData.Result = e.Message;
            }
                
        }

        /// <summary>
        /// 获取comobox的下拉选项
        /// </summary>
        public void GetComoboxItems()
        {
            string sql = "";
            for (int i = 0; i < oTagInfo.InputParams.Count(); i++)
            {
                sql = sql + oTagInfo.InputParams[i] + "','";
            }
            sql = sql.Substring(0,sql.Length-3);

            try
            {
                XmlDocument oReplyResult = new XmlDocument();
                XmlDeclaration oDocDec = oReplyResult.CreateXmlDeclaration("1.0", "UTF-8", null);
                oReplyResult.AppendChild(oDocDec);
                XmlNode oPList = oReplyResult.CreateElement("ComoboxList");
                oReplyResult.AppendChild(oPList);

                sql =
                 "   select linkdescription,pridisplayname,prisourcename        " +
                 "   from df_config_condition_linkage                           " +
                 "   where linkdescription in ('" + sql + "')                   " +
                 "   order by linkdescription, prisequence * 1                  ";

                oDbCmd.CommandText = sql;
                MySqlDataReader oPReader = oDbCmd.ExecuteReader();
                XmlNode MenuItem = null;

                while (oPReader.Read())
                {
                    MenuItem = oReplyResult.CreateElement("Comoboxes");

                    XmlAttribute oAccessName = oReplyResult.CreateAttribute("ComoboxName");
                    oAccessName.Value = (string)oPReader["linkdescription"];
                    MenuItem.Attributes.Append(oAccessName);

                    XmlAttribute oDisplayName = oReplyResult.CreateAttribute("displayName");
                    oDisplayName.Value = (string)oPReader["pridisplayname"];
                    MenuItem.Attributes.Append(oDisplayName);

                    XmlAttribute oSourceName = oReplyResult.CreateAttribute("sourceName");
                    oSourceName.Value = (string)oPReader["prisourcename"];
                    MenuItem.Attributes.Append(oSourceName);

                    oPList.AppendChild(MenuItem);

                }

                StringWriter oSW = new StringWriter();
                XmlTextWriter oXTW = new XmlTextWriter(oSW);
                oReplyResult.WriteTo(oXTW);

                oData.Result = oSW.ToString();
            }
            catch (Exception ex)
            {
                oData.bIsOK = false;
                oData.Result = "无法初始化下拉框选项，请联系管理员！" + ex.Message;
            }
        }

        #endregion


        #region common function
        public static void setReportData40(ref MySqlDataReader oDataReader, ref RowData oRawData)
        {

            //oRawData.RowNum = i.ToString();
            oRawData.rowData = new List<string>();
            //Type oType = oRawData.GetType();

            for (int iIndex = 1; iIndex <= oDataReader.FieldCount; iIndex++)
            {
                if (!Convert.IsDBNull(oDataReader[iIndex - 1]))
                {
                    oRawData.rowData.Add(Convert.ToString(oDataReader[iIndex - 1]));
                    //oType.GetProperty("Collumn" + iIndex.ToString()).SetValue(oRawData, Convert.ToString(oDataReader[iIndex - 1]), null);
                }
                else
                {
                    oRawData.rowData.Add("");
                    //oType.GetProperty("Collumn" + iIndex.ToString()).SetValue(oRawData, "", null);
                }

                //if (!Convert.IsDBNull(oDataReader[iIndex - 1]))
                //{
                //    oType.GetProperty("Collumn" + iIndex.ToString()).SetValue(oRawData, Convert.ToString(oDataReader[iIndex - 1]), null);
                //}
                //else
                //{
                //    oType.GetProperty("Collumn" + iIndex.ToString()).SetValue(oRawData, "", null);
                //}
            }

        }
        //private static void setReportData80(MySqlDataReader oDataReader, Raw80Data oRawData, int i)
        //{
        //    //oRawData.RowNum = i.ToString();

        //    oRawData.RowNum = i.ToString();
        //    Type oType = oRawData.GetType();

        //    for (int iIndex = 1; iIndex <= oDataReader.FieldCount; iIndex++)
        //    {
        //        if (!Convert.IsDBNull(oDataReader[iIndex - 1]))
        //        {
        //            oType.GetProperty("Collumn" + iIndex.ToString()).SetValue(oRawData, Convert.ToString(oDataReader[iIndex - 1]), null);
        //        }
        //        else
        //        {
        //            oType.GetProperty("Collumn" + iIndex.ToString()).SetValue(oRawData, "", null);
        //        }
        //    }

        //}

        #endregion

    }
}
