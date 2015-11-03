using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MySql.Data.MySqlClient;
using System.ServiceModel.Channels;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Data;
using DataContractAssembly;

namespace WIPWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WipService : IWipService
    {
        #region local variable
        //private OperationContext operationContext;
        private bool _bDebug = false;
        private FuncTagInfo oTagInfo;
        private MySqlConnection oDbCon = null;
        private MySqlCommand oDbCmd = null;
        private MySqlDataReader oDbReader = null;
        CallBackData oData = null;
        //string _sIPV4 = string.Empty;
        string _sPc = string.Empty;

        private string sConnectionString =
                        "server=127.0.0.1;" +//192.168.0.91 127.0.0.1
                       "uid=mesadmin;" +
                       "pwd=1qAZ2wSX;" +
                       "database=js_mes;";

        #endregion
        
        #region constractor
        public WipService()
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
        public WipService(bool bDebug)
        {
            _bDebug = bDebug;
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
        /// 获取电池分批信息
        /// </summary>
        public void GetCellBatchInfo()
        {
            string sCellBatchInfo = oTagInfo.InputParams[0];        //电池片批号

            try
            {
                string sSql =
                   " select cell_batch_no,batch_count,cell_count_remain,cell_comp   " +
                    " from rt_cell_distribute where cell_batch_no='" + sCellBatchInfo + "' ";


                oDbCmd.CommandText = sSql;
                MySqlDataReader oReader = oDbCmd.ExecuteReader();

                if (!oReader.HasRows)
                {
                    oData.bIsOK = false;
                    oData.Result = "系统无法找到此批次的电池片！";
                    return;
                }

                RowData oRawData = null;
                oData.RowDatas = new List<RowData>();
                while (oReader.Read())
                {
                    oRawData = new RowData();
                    setReportData40(ref oReader, ref oRawData);
                    oData.RowDatas.Add(oRawData);
                }
            }
            catch (Exception e)
            {
                oData.bIsOK = false;
                oData.Result = e.Message;
            }
        }

        /// <summary>
        /// 电池片信息绑定
        /// </summary>
        public void CellBinding()
        {
            int i = 0;
            string sCellBatchInfo = oTagInfo.InputParams[i++];        //电池片批号
            string sModuleID = oTagInfo.InputParams[i++];        //组件号
            string sHulianComp = oTagInfo.InputParams[i++];        //互联条
            string sHuiliuComp = oTagInfo.InputParams[i++];        //汇流条
            string sEqp = oTagInfo.InputParams[i++];        //设备号
            string suser = oTagInfo.InputParams[i++];        //操作人员
            string workshop = oTagInfo.InputParams[i++];        //车间
            string isNight = oTagInfo.InputParams[i++];        //

            try
            {
                //检查组件号是否存在
                string sSql =
                  "  SELECT a.module_id,b.cell_batch_no             " +
                  "  FROM lotbasis a                                " +
                  "  left join js_mes.rt_worksite_weld b            " +
                  "  on a.module_id=b.module_id                     " +
                  "  and b.usedflag='1'                             " +
                  "  where a.module_id='" + sModuleID + "'           ";


                using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sSql, conn);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (!dr.HasRows)
                    {
                        oData.bIsOK = false;
                        oData.Result = "系统中无法找到此组件！";
                        return;
                    }

                    dr.Read();
                    if (!Convert.IsDBNull(dr[1]))
                    {
                        oData.bIsOK = false;
                        oData.Result = "不能重复绑定！";
                        return;
                    }
                }


                #region 插入绑定信息
                sSql =
                   " insert into rt_worksite_weld(module_id,cell_batch_no,hulian_comp,huiliu_comp,eqp_id,create_time,op,workshop,usedflag) " +
                   " values('" + sModuleID + "','" + sCellBatchInfo + "','" + sHulianComp + "','" + sHuiliuComp +
                   "','" + sEqp + "',now(),'" + suser + "','" + workshop + "','1')";

                using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sSql, conn);
                    cmd.ExecuteNonQuery();
                }
                #endregion

                #region 更新批次数量
                sSql =
                   " update rt_cell_distribute set cell_count_remain=cell_count_remain-60   " +
                   " where cell_batch_no='" + sCellBatchInfo + "' ";
                using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sSql, conn);
                    cmd.ExecuteNonQuery();
                }
                #endregion

                #region 更新站点
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
                #endregion

                #region 返回批次信息
                sSql =
                   " select cell_batch_no,batch_count,cell_count_remain,cell_comp   " +
                    " from rt_cell_distribute where cell_batch_no='" + sCellBatchInfo + "' ";
                oDbCmd.CommandText = sSql;
                MySqlDataReader oReader = oDbCmd.ExecuteReader();

                RowData oRawData = null;
                oData.RowDatas = new List<RowData>();
                while (oReader.Read())
                {
                    oRawData = new RowData();
                    setReportData40(ref oReader, ref oRawData);
                    oData.RowDatas.Add(oRawData);
                }
                #endregion

                #region 返回过站记录数
                if (isNight.ToUpper() == "NIGHT")
                {
                    sSql =
                      "   select count(*) layup_count from rt_worksite_weld                     " +
                      "   where eqp_id='" + sEqp + "' and usedflag='1'                          " +
                      "   and create_time>=CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 19:20:00')     " +
                      "   and create_time<CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 23:59:59')      ";

                }
                else if (isNight.ToUpper() == "DAY")
                {
                    sSql =
                     "   select count(*) layup_count from rt_worksite_weld                     " +
                     "   where eqp_id='" + sEqp + "'  and usedflag='1'                         " +
                     "   and create_time>=CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 07:20:00')     " +
                     "   and create_time<CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 19:20:00')      ";

                }
                else
                {
                    sSql =
                    "   select count(*) layup_count from rt_worksite_weld                     " +
                    "   where eqp_id='" + sEqp + "'  and usedflag='1'                         " +
                    "   and create_time>=DATE_SUB(CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 19:20:00'), INTERVAL 1 DAY)     " +
                    "   and create_time<CONCAT(DATE_FORMAT(now(),'%Y-%m-%d'),' 07:20:00')      ";
                }

                using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sSql, conn);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    oData.Result = Convert.ToString(dr[0]);
                }
                #endregion

            }
            catch (Exception e)
            {
                oData.bIsOK = false;
                if (e.Message.Contains("Duplicate entry"))
                {
                    oData.Result = "不能重复绑定！";
                }
                else
                {
                    oData.Result = e.Message;
                }
            }
        }

        /// <summary>
        /// 获取工单在制信息
        /// </summary>
        public void GetWoWipInfo()
        {
            string workshop=oTagInfo.InputParams[0];
            string sql =
             "   select a.workorder_id,a.qty,        " +
             "   count(b.module_id) wipcount,a.module_type,     " +
             "   a.pid_type,workorder_seq,a.workshop,c.lastcount                       " +
             "   from df_wo_info a                              " +
             "   left join lotbasis b                           " +
             "   on b.workorder_id=a.workorder_id               " +
             "   and b.workshop_id =a.workshop                  " +
             "   left join rt_prefix_lastcount c                                        " +
             "   on c.prefix=concat(a.workorder_id,DATE_FORMAT(now(),'%y%m%d')) " +
             "   where a.state='open'                           " +
             "   and a.workshop='" + workshop + "'              " +
             "   group by a.workorder_id,a.qty,a.workshop       ";
            
            oDbCmd.CommandText = sql;
            MySqlDataReader oReader = oDbCmd.ExecuteReader();

            if (!oReader.HasRows)
            {
                oData.bIsOK = false;
                oData.Result = "没有找到工单信息";
                return;
            }

            RowData oRawData = null;
            oData.RowDatas = new List<RowData>();
            while (oReader.Read())
            {
                oRawData = new RowData();
                setReportData40(ref oReader, ref oRawData);
                oData.RowDatas.Add(oRawData);
            }

        }

        /// <summary>
        /// 生码
        /// </summary>
        public void CreateLabel()
        { 
            string workorder=oTagInfo.InputParams[0];       //工单
            string workshop=oTagInfo.InputParams[1];       //车间
            string startModuleIdx = oTagInfo.InputParams[2];//起始序列号
            string modulePrefix = oTagInfo.InputParams[3]; //组件前缀
            string endModuleIdx=oTagInfo.InputParams[4];     //截至序列号      
            string user = oTagInfo.InputParams[5];
            string createDate = oTagInfo.InputParams[6];        //生码日期

            string sql = string.Empty;

            try
            {
                for (int i = int.Parse(startModuleIdx); i < int.Parse(endModuleIdx); i++)
                {
                    sql = sql + "INSERT INTO `js_mes`.`lotbasis` (`Module_ID`, `WorkOrder_ID`, `Status`, `Factory_ID`, " +
                        "`Workshop_ID`, `current_flow_id`, `next_flow_id`,  `worksite`, `cell_batch_no`, `Appearance_grade`, `EL_Grade`, `final_grade`," +
                        "`Create_Date`, `OP_ID`) VALUES ('"+modulePrefix+i.ToString("0000")+"', '"+workorder+"', 'A', NULL, '"+
                        workshop + "', 1,2, null, NULL, 'A', 'A',NULL, now(), '" + user + "');";
                }


                oDbCmd.CommandText = sql;
                oDbCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                oData.bIsOK = false;
                oData.Result = e.Message;
            }

            #region 更新流水号
            if (int.Parse(startModuleIdx)==1)
            {
                sql =
                 "   insert into rt_prefix_lastcount(prefix,lastcount) values(      " +
                 "   '" + workorder + createDate + "','" + (int.Parse(endModuleIdx)-1).ToString() + "')       ";
            }
            else
            {
                sql =
                 "   update rt_prefix_lastcount set lastcount='" + (int.Parse(endModuleIdx) - 1).ToString() + 
                 "' where prefix='" + workorder + createDate + "'";
            }
            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }

            #endregion

        }

        /// <summary>
        /// 获取机台信息
        /// </summary>
        public void GetEQPInfo()
        {
            int i = 0;
            string eqpType = oTagInfo.InputParams[i++];
            string workshop = oTagInfo.InputParams[i++];

            string sql =
              "  select machine_name machineName,         " +
              "  machine_id machineID,worksite_id       " +
              "  from df_eqp_info                       " +
              "  where actived='T'                      " +
              "  and machine_type='" + eqpType + "'     " +
              "  and workshop='" + workshop + "'        ";

            oDbCmd.CommandText = sql;
            MySqlDataReader oReader = oDbCmd.ExecuteReader();

            if (!oReader.HasRows)
            {
                oData.bIsOK = false;
                oData.Result = "没有找到机台信息";
                return;
            }

            RowData oRawData = null;
            oData.RowDatas = new List<RowData>();
            while (oReader.Read())
            {
                oRawData = new RowData();
                setReportData40(ref oReader, ref oRawData);
                oData.RowDatas.Add(oRawData);
            }
        }

        /// <summary>
        /// 包装--获取组件信息
        /// </summary>
        public void GetModuleInfo()
        {
            int i=0;
            string module_id=oTagInfo.InputParams[i++];
            
            #region 检查 1.是否包装 2.是否活动状态 3.当前站点是否是M75(包装)
            string sql =
             "  select a.module_id,c.flow_id_name,a.status,d.state          " +
             "   from lotbasis a                                            " +
             "   left join df_wo_info b                                     " +
             "   on a.workorder_id=b.workorder_id                           " +
             "   left join df_workflow c                                    " +
             "   on c.flow_id=b.flow_id                                     " +
             "   and c.flow_id_idx=a.next_flow_id                           " +
             "   left join rt_mid_packing d                                 " +
             "   on d.module_id=a.module_id                                 " +
             "   and d.state='Packed'                                       " +
             "   where a.module_id='" + module_id + "'                       ";

            try
            {
                oDbCmd.CommandText = sql;
                oDbReader = oDbCmd.ExecuteReader();

                if (!oDbReader.HasRows)
                {
                    oData.bIsOK = false;
                    oData.Result = "系统找不到这块组件！";
                    return;
                }
                while (oDbReader.Read())
                {
                    string currentSite = string.Empty;
                    string status = string.Empty;
                    string packState = string.Empty;

                    if (!Convert.IsDBNull(oDbReader["flow_id_name"]))
                    {
                        currentSite = (string)oDbReader["flow_id_name"];
                    }
                    if (!Convert.IsDBNull(oDbReader["status"]))
                    {
                        status=(string)oDbReader["status"];
                    }
                    if (!Convert.IsDBNull(oDbReader["state"]))
                    {
                        packState = (string)oDbReader["state"];
                    }

                    if (packState.ToUpper()=="PACKED")
                    {
                        oData.bIsOK = false;
                        oData.Result = "组件已经包装";
                        return;
                    }

                    if (status==string.Empty||status.ToUpper()=="HOLD")
                    {
                        oData.bIsOK = false;
                        oData.Result = "组件是hold状态，不能进行包装！";
                        return;
                    }

                    if (currentSite == string.Empty || currentSite != "M75")
                    {
                        oData.bIsOK = false;
                        oData.Result = "组件当前站点是" + currentSite;
                        return;
                    }
                }
                oDbReader.Close();
            }
            catch (Exception e)
            {
                oData.bIsOK = false;
                oData.Result = e.Message;
                return;
            }
            
            #endregion

            #region 获取托盘信息
            sql =
             "   select a.module_id,b.workorder_id,b.sale_order,            " +
             "   (case when a.el_grade = 'A' then (                         " +
             "       case when a.appearance_grade = 'A' then 'A'            " +
             "           when a.appearance_grade = 'B' then 'B'             " +
             "           when a.appearance_grade = 'C' then 'C'             " +
             "           else ''                                            " +
             "       end)                                                   " +
             "   when a.el_grade = 'B' then (                               " +
             "       case when a.appearance_grade = 'A' then 'B'            " +
             "           when a.appearance_grade = 'B' then 'B'             " +
             "           when a.appearance_grade = 'C' then 'C'             " +
             "           else ''                                            " +
             "       end)                                                   " +
             "   when a.el_grade = 'C' then (                               " +
             "       case when a.appearance_grade = 'A' then 'C'            " +
             "           when a.appearance_grade = 'B' then 'C'             " +
             "           when a.appearance_grade = 'C' then 'C'             " +
             "           else ''                                            " +
             "       end)                                                   " +
             "   else ''                                                    " +
             "   end                                                        " +
             "   )                                                          " +
             "   final_grade,e.power_grade,                                 " +
             "   e.imp_grade,b.cell_consumption,                            " +
             "   b.module_color,b.pack_count,c.jbox_desc,                   " +
             "   c.frame_desc,d.pmax,d.VPM,d.IPM,d.ff,d.voc,                " +
             "   d.isc,d.test_datetime,d.surftemp,                           " +
             "   b.module_type,b.cristal_type,b.workshop,b.cell_type,       " +
             "   b.product_no1,b.product_no2                                " +
             "   from lotbasis a                                            " +
             "   left join df_wo_info b                                     " +
             "   on b.workorder_id=a.workorder_id                           " +
             "   left join rt_worksite_frame c                              " +
             "   on c.module_id=a.module_id                                 " +
             "   left join rt_mid_flash d                                   " +
             "   on d.module_id=a.module_id                                 " +
             "   left join df_module_power e                                " +
             "   on b.workorder_id=e.q1_id                                  " +
             "   and e.upperpower > d.pmax                                  " +
             "   and e.lowerpower <= d.pmax                                 " +
             "   AND e.upperimp > d.ipm                                     " +
             "   AND e.lowerimp <= d.ipm                                    " +
             "   where a.module_id='" + module_id + "'                       ";

            oDbCmd.CommandText = sql;
            oDbReader = oDbCmd.ExecuteReader();

            RowData oRawData = null;
            oData.RowDatas = new List<RowData>();
            while (oDbReader.Read())
            {
                oRawData = new RowData();
                setReportData40(ref oDbReader, ref oRawData);
                oData.RowDatas.Add(oRawData);
            }
            #endregion
        }

        /// <summary>
        /// 包装--生成托盘信息
        /// </summary>
        public void GetPalletInfo()
        {
            int i = 0;

            string pallet = oTagInfo.InputParams[i++];
            string sPattern = oTagInfo.InputParams[i++];
            string pack_count = oTagInfo.InputParams[i++];
            string module_id = oTagInfo.InputParams[i++];
            string final_grade = oTagInfo.InputParams[i++];
            string power_grade = oTagInfo.InputParams[i++];
            string imp_grade = oTagInfo.InputParams[i++];
            string pmax = oTagInfo.InputParams[i++];
            string vpm = oTagInfo.InputParams[i++];
            string ipm = oTagInfo.InputParams[i++];
            string ff = oTagInfo.InputParams[i++];
            string voc = oTagInfo.InputParams[i++];
            string isc = oTagInfo.InputParams[i++];
            string test_time = oTagInfo.InputParams[i++];
            string surf_temp = oTagInfo.InputParams[i++];
            string modult_type = oTagInfo.InputParams[i++];
            string cristal_type = oTagInfo.InputParams[i++];
            string wrokshop = oTagInfo.InputParams[i++];
            string module_color=oTagInfo.InputParams[i++];
            string cell_consumption=oTagInfo.InputParams[i++];
            string workorder=oTagInfo.InputParams[i++];
            string user = oTagInfo.InputParams[i++];
            string cell_type= oTagInfo.InputParams[i++];
            string jbox_desc= oTagInfo.InputParams[i++];
            string frame_desc=oTagInfo.InputParams[i++];
            string product_no1 = oTagInfo.InputParams[i++];
            string product_no2 = oTagInfo.InputParams[i++]; 


            //==>判断组件是否是等待状态（之前已经包装过）
            bool bIsWait = false;
            string strCartonNo = "";
            string sql = "select packing.Carton_No from rt_mid_packing packing where packing.module_id='" +
                module_id + "' and (state = 'Wait') ";

            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    bIsWait = true;
                    strCartonNo = Convert.ToString(dr[0]);
                }
                
            }
                
            //====>如果是未包装过的组件
            if (!bIsWait)
            {
                //==>如果托盘号为空，则创建托盘号
                if (pallet==string.Empty)
                {
                    #region MyRegion
                    //托盘号规则：组件类型(1码)+组件颜色(1码)+晶体类型(3码)+电池片数(2码)+
                    //              组件功率(3码)+电流档位(1码)+部门编码(1码)+包装时间(6码)+流水码(3码)

                    #region 检查输入项目是否合法
                    if (modult_type.Length != 1 || module_color.Length!=1||
                        cristal_type.Length != 3 || cell_consumption.Length!=2||
                        power_grade.Length != 3 || imp_grade.Length!=2||
                        wrokshop.Length!=3)
                    {
                        oData.bIsOK = false;
                        oData.Result = "存在异常的编码，请联系mes工程师检查";
                        return;
                    }
                    #endregion

                    //===>获取编码所需的元素
                        
                    string sDateTime = System.DateTime.Now.ToString("yyMMdd");

                    //====>获取流水号
                    #region 获取流水号
                    int flowID = 0;
                    string sPrefix = "PalletNo_" + wrokshop + sDateTime;
                    sql = "select lastcount from rt_prefix_lastcount where prefix='" + sPrefix + "'";

                    using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader dr = cmd.ExecuteReader();

                        if (!dr.HasRows)
                        {
                            flowID = 1;
                        }
                        else
                        {
                            dr.Read();
                            flowID = Convert.ToInt32(dr[0]);
                            flowID++;
                        }
                    }

                    //====>更新流水号
                    if (flowID == 1)
                    {
                        sql = "insert into rt_prefix_lastcount(prefix,lastcount) values('" + sPrefix + "','" + flowID + "')";
                    }
                    else
                    {
                        sql = "update rt_prefix_lastcount set lastcount='" + flowID + "' where prefix='" + sPrefix + "'";
                    }

                    using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();

                    }

                    #endregion

                    int iImpgrade = Convert.ToInt32(imp_grade.Substring(1));
                    int iWorkshop = Convert.ToInt32(wrokshop.Substring(1));

                    //==>创建托盘号
                    string sPalletNo = modult_type + module_color + cristal_type + cell_consumption + power_grade
                        + iImpgrade.ToString() + iWorkshop.ToString() + sDateTime + flowID.ToString("000");

                    sql=
                        " insert into rt_mid_packing(carton_no,workorder_id,module_id,pmax,ff,                 "+
                        " voc,isc,state,packing_date,op_id,module_grade,imp_grade,product_type,vpm,ipm,test_date) "+
                        " values('" + sPalletNo + "','" + workorder + "','" + module_id + "','" + pmax +
                        "','" + ff + "','" + voc + "','" + isc + "','Wait',now(),'" + user + "','" + final_grade +
                        "','" + imp_grade + "','" + power_grade + "','" + vpm + "','" + ipm + "','" + test_time + "')                    ";
                    
                    //==>插入数据库
                    using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }

                    //==>生成托盘信息
                    string bus_bar_tye=module_id.Substring(3,1);
                    if (bus_bar_tye=="0"||bus_bar_tye=="1")
                    {
                        bus_bar_tye = "三栅";
                    }
                    else if (bus_bar_tye=="2"||bus_bar_tye=="3")
                    {
                        bus_bar_tye = "四栅";
                    }

                    sql =
                        "  insert into rt_pallet_info(pallet_no,state,final_grade,power_grade,                    " +
                        "  imp_grade,cell_type,cell_qty,jbox_spec,frame_spec,bus_bar_type,product_no1,product_no2) values(                " +
                        "  '" + sPalletNo + "','Wait','" + final_grade + "','"+power_grade+"','"+imp_grade+
                        "','"+cell_type+"','"+cell_consumption+"','"+jbox_desc+"','"+frame_desc+"','"+bus_bar_tye+"','"+product_no1+
                        "','"+product_no2+"') ";

                    using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                      
                    //==>返回托盘信息
                    sql =
                        " select carton_no,module_id,pmax,ipm from rt_mid_packing                 " +
                        " where carton_no='" + sPalletNo + "' order by packing_date desc        ";

                    using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader dr = cmd.ExecuteReader();


                        RowData oRawData = null;
                        oData.RowDatas = new List<RowData>();
                        while (dr.Read())
                        {
                            oRawData = new RowData();
                            setReportData40(ref dr, ref oRawData);
                            oData.RowDatas.Add(oRawData);
                        }
                    }

                    oData.Result = sPalletNo + "|" + pack_count + "|" + sPattern + "|";//返回托盘数量作为打印依据

                    sql =
                     //"   SELECT CONCAT(power_grade,'W') power,                                          " +
                     "   SELECT power_grade power,                                          " +
                     "   case when a.product_no1='' and a.product_no2='' then                                       " +
                     "   CONCAT('ZX',cell_type,'-',cell_qty,'-',power_grade)                                        " +
                     "   when a.product_no1<>'' and a.product_no2='' then                                           " +
                     "   CONCAT('ZX',cell_type,'-',cell_qty,'-',power_grade,'/',a.product_no1)                      " +
                     "   when a.product_no1='' and a.product_no2<>'' then                                           " +
                     "   CONCAT('ZX',cell_type,'-',cell_qty,'-',power_grade,'/',a.product_no2)                      " +
                     "   when a.product_no1<>'' and a.product_no2<>'' then                                          " +
                     "   CONCAT('ZX',cell_type,'-',cell_qty,'-',power_grade,'/',a.product_no1,a.product_no2)        " +
                     "   end pallet_type,                                                                           " +
                     "   a.jbox_spec,a.frame_spec,                                                                  " +
                     //"   CONCAT('Class ',a.final_grade) final_grade,a.bus_bar_type                                  " +
                     "   CONCAT('Class ',a.final_grade) final_grade,a.imp_grade                                  " +
                     "   FROM rt_pallet_info a                                                                      " +
                     "   where pallet_no='" + sPalletNo + "'                                                    ";

                    using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader dr = cmd.ExecuteReader();

                        i=0;
                        while (dr.Read())
                        {
                            oData.Result = oData.Result + (string)dr[i++] + "|" + (string)dr[i++] + "|" + (string)dr[i++] + "|"
                        + (string)dr[i++] + "|" + (string)dr[i++] + "|" + (string)dr[i++];
                        }
                    }
                    #endregion
                }
                else
                {
                    #region MyRegion
                    //====>写入已有托盘
                    sql =
                        " insert into rt_mid_packing(carton_no,workorder_id,module_id,pmax,ff,                 " +
                        " voc,isc,state,packing_date,op_id,module_grade,imp_grade,product_type,vpm,ipm,test_date) " +
                        " values('" + pallet + "','" + workorder + "','" + module_id + "','" + pmax +
                        "','" + ff + "','" + voc + "','" + isc + "','Wait',now(),'" + user + "','" + final_grade +
                        "','" + imp_grade + "','" + power_grade + "','" + vpm + "','" + ipm + "','" + test_time + "')                    ";

                    //==>插入数据库
                    using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }

                    //==>返回托盘信息
                    sql =
                        " select carton_no,module_id,pmax,ipm from rt_mid_packing                 " +
                        " where carton_no='" + pallet + "' order by packing_date desc        ";

                    using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader dr = cmd.ExecuteReader();


                        RowData oRawData = null;
                        oData.RowDatas = new List<RowData>();
                        while (dr.Read())
                        {
                            oRawData = new RowData();
                            setReportData40(ref dr, ref oRawData);
                            oData.RowDatas.Add(oRawData);
                        }
                    }

                    oData.Result = pallet + "|" + pack_count + "|" + sPattern+"|";//返回托盘数量作为打印依据

                    sql =
                     "   SELECT power_grade power,                                          " +
                     "   case when a.product_no1='' and a.product_no2='' then                                       " +
                     "   CONCAT('ZX',cell_type,'-',cell_qty,'-',power_grade)                                        " +
                     "   when a.product_no1<>'' and a.product_no2='' then                                           " +
                     "   CONCAT('ZX',cell_type,'-',cell_qty,'-',power_grade,'/',a.product_no1)                      " +
                     "   when a.product_no1='' and a.product_no2<>'' then                                           " +
                     "   CONCAT('ZX',cell_type,'-',cell_qty,'-',power_grade,'/',a.product_no2)                      " +
                     "   when a.product_no1<>'' and a.product_no2<>'' then                                          " +
                     "   CONCAT('ZX',cell_type,'-',cell_qty,'-',power_grade,'/',a.product_no1,a.product_no2)        " +
                     "   end pallet_type,                                                                           " +
                     "   a.jbox_spec,a.frame_spec,                                                                  " +
                     "   CONCAT('Class ',a.final_grade) final_grade,a.imp_grade                                  " +
                     "   FROM rt_pallet_info a                                                                      " +
                     "   where pallet_no='" + pallet + "'                                                    ";

                    using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader dr = cmd.ExecuteReader();

                        i = 0;
                        while (dr.Read())
                        {
                            oData.Result = oData.Result + (string)dr[i++] + "|" + (string)dr[i++] + "|" + (string)dr[i++] + "|"
                        + (string)dr[i++] + "|" + (string)dr[i++] + "|" + (string)dr[i++];
                        }
                    }
                    #endregion
                }
            }
            else//====>如果是包装过的组件
            {
                #region MyRegion
                //==>返回托盘信息
                sql =
                    " select carton_no,module_id,pmax,ipm from rt_mid_packing                 " +
                    " where carton_no='" + strCartonNo + "' order by packing_date desc        ";

                using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader dr = cmd.ExecuteReader();


                    RowData oRawData = null;
                    oData.RowDatas = new List<RowData>();
                    while (dr.Read())
                    {
                        oRawData = new RowData();
                        setReportData40(ref dr, ref oRawData);
                        oData.RowDatas.Add(oRawData);
                    }
                }
                oData.Result = strCartonNo + "|" + pack_count + "|" + sPattern+"|";//返回托盘数量作为打印依据
                
                sql =
                     "   SELECT power_grade power,                                          " +
                     "   case when a.product_no1='' and a.product_no2='' then                                       " +
                     "   CONCAT('ZX',cell_type,'-',cell_qty,'-',power_grade)                                        " +
                     "   when a.product_no1<>'' and a.product_no2='' then                                           " +
                     "   CONCAT('ZX',cell_type,'-',cell_qty,'-',power_grade,'/',a.product_no1)                      " +
                     "   when a.product_no1='' and a.product_no2<>'' then                                           " +
                     "   CONCAT('ZX',cell_type,'-',cell_qty,'-',power_grade,'/',a.product_no2)                      " +
                     "   when a.product_no1<>'' and a.product_no2<>'' then                                          " +
                     "   CONCAT('ZX',cell_type,'-',cell_qty,'-',power_grade,'/',a.product_no1,a.product_no2)        " +
                     "   end pallet_type,                                                                           " +
                     "   a.jbox_spec,a.frame_spec,                                                                  " +
                     "   CONCAT('Class ',a.final_grade) final_grade,a.imp_grade                                  " +
                     "   FROM rt_pallet_info a                                                                      " +
                     "   where pallet_no='" + strCartonNo + "'                                                    ";

                using (MySqlConnection conn = new MySqlConnection(sConnectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    i = 0;
                    while (dr.Read())
                    {
                        oData.Result = oData.Result + (string)dr[i++] + "|" + (string)dr[i++] + "|" + (string)dr[i++] + "|"
                        + (string)dr[i++] + "|" + (string)dr[i++] + "|" + (string)dr[i++];
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 更新托盘信息
        /// </summary>
        public void UpdatePalletInfo()
        {
            int i = 0;
            string palletno = oTagInfo.InputParams[i++];
            string pack_count = oTagInfo.InputParams[i++];

            string sql =
               " update rt_mid_packing set state='Packed' where Carton_no='" + palletno + "'";

            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }

            sql =
                "update rt_pallet_info set state='Packed' where pallet_no='" + palletno + "'";
           
            using (MySqlConnection conn = new MySqlConnection(sConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
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
