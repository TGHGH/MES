using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using GeneralWcfService;
using System.Net;
using WcfRequestNS;
using System.IO;
using System.Xml.Linq;
using Module.BasicClassLib;
//using WIPWcfService;
using System.Diagnostics;
using WcfRequestNS.ServiceReference;

namespace MaterialPlan
{
    public partial class CreateLabel : Form
    {
        string _user = string.Empty;//当前用户
        RowData[] _woWIPinfo = null;
        string _modulePrefix = string.Empty;
        string _createLabelDateTime = string.Empty;

        public CreateLabel()
        {
            InitializeComponent();
        }

        private void CreateLabel_Load(object sender, EventArgs e)
        {
            #region 获取当前主机的登录用户
            FuncTagInfo functionTag = null;
            functionTag = new FuncTagInfo();
            functionTag.FuncName = "getCurrentUser";
            functionTag.ComputerName = Dns.GetHostName(); ;

            WcfRequests.SendGeneralRequest(onCurrentUserReceive, functionTag);
            #endregion

            #region 初始化comobox的下拉项
            List<string> inParameters = new List<string>();
            inParameters.Add(BasicClass.comoboxItemName.Workshop);
            //inParameters.Add("wo_type");
            //inParameters.Add("cristal_type");
            //inParameters.Add("cell_size");
            //inParameters.Add("cell_consumption");
            //inParameters.Add("cell_type");
            //inParameters.Add("workflow");

            functionTag = new FuncTagInfo();
            functionTag.FuncName = "GetComoboxItems";
            functionTag.InputParams = inParameters.ToArray(); ;

            WcfRequests.SendGeneralRequest(onReceiveComoboxItems, functionTag);
            #endregion


            cbx_workorder.Enabled = false;

            #region trigger
            //生码数量
            tbx_label_count.KeyDown += new KeyEventHandler(tbx_label_count_KeyDown);
            
            //选择工单
            cbx_workorder.SelectedIndexChanged += new EventHandler(cbx_workorder_SelectedIndexChanged);
            
            //选择车间
            cbx_workshop.SelectedIndexChanged += new EventHandler(cbx_workshop_SelectedIndexChanged);
            #endregion

            #region 组件序列号明细试图
            dgv_module_id.AllowUserToAddRows = false;
            dgv_module_id.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_module_id.Font = new System.Drawing.Font("宋体", 12, FontStyle.Regular);
            dgv_module_id.ReadOnly = true;
            dgv_module_id.Columns.Add("ModuleId", "组件序列号");
            #endregion

            //创建文件夹，打印用
            if (!Directory.Exists(@"c:\BarcodePrint"))
            {
                Directory.CreateDirectory(@"c:\BarcodePrint");
            }
        }


        #region trigger receiced
        /// <summary>
        /// 选择车间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cbx_workshop_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbx_workshop.Enabled = false;

            //刷新页面
            refleshPage();

            cbx_workorder.Enabled = true;
        }


        /// <summary>
        /// 生码数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tbx_label_count_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                btn_preview_Click(this, EventArgs.Empty);
            }
            
        }

        /// <summary>
        /// 选择工单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cbx_workorder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex==-1)
            {
                return;
            }
            string woPalnCount = _woWIPinfo[cbx_workorder.SelectedIndex].rowData[1];
            string woWipCount = _woWIPinfo[cbx_workorder.SelectedIndex].rowData[2];

            tbx_wip_count.Text = woWipCount;
            tbx_count_remain.Text = (int.Parse(woPalnCount) - int.Parse(woWipCount)).ToString();
            tbx_plan_count.Text = woPalnCount;
        }

        /// <summary>
        /// 生成按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_preview_Click(object sender, EventArgs e)
        {
            #region check
            if (!BasicClass.isNumeric(tbx_label_count.Text))
            {
                MessageBox.Show("您输入的格式不正确，请检查");
                return;
            }
            if (cbx_workorder.Text==string.Empty||cbx_workshop.Text==string.Empty)
            {
                MessageBox.Show("必选项不能为空！");
                return;
            }

            if (int.Parse(tbx_label_count.Text)>int.Parse(tbx_count_remain.Text))
            {
                MessageBox.Show("生码数量已超过范围");
                return;
            }

            if (int.Parse(tbx_label_count.Text)>1000)
            {
                MessageBox.Show("生码数量过多，请分批操作");
                return;
            }

            #endregion

            dgv_module_id.Rows.Clear();

            string sFac = "ZX";
            string moduleType= _woWIPinfo[cbx_workorder.SelectedIndex].rowData[3];
            string pidType = _woWIPinfo[cbx_workorder.SelectedIndex].rowData[4];
            string woSeq = _woWIPinfo[cbx_workorder.SelectedIndex].rowData[5];

            string sDate = System.DateTime.Now.ToString("yyMMdd");
            _createLabelDateTime=sDate;
            string sYear = (int.Parse(sDate.Substring(0,2)) + 15).ToString();
            string sMonth = (int.Parse(sDate.Substring(2, 2)) + 18).ToString();
            string sDay = (int.Parse(sDate.Substring(4, 2)) + 12).ToString();
            sDate = sYear + sMonth + sDay;
            //_createLabelDateTime=sDate
            string workshop = ((ListItem)cbx_workshop.SelectedItem).ID;

            if (System.Text.RegularExpressions.Regex.IsMatch(workshop,"^[A-Z]\\d{2}$"))
            {
                workshop = workshop.Substring(1);
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(workshop,"^\\d{2}$"))
            {
                MessageBox.Show("车间代码有错误，请检查配置");
                return;
            }

            int iWorkshop = int.Parse(workshop);
            int iStart = 0;
            #region 流水号每天归零
            string sLastFlowID=_woWIPinfo[cbx_workorder.SelectedIndex].rowData[7];
            if (sLastFlowID==string.Empty)
            {
                iStart = 1;
            }
            else
            {
                iStart = int.Parse(sLastFlowID)+1;
            }
            #endregion
            //int iStart = int.Parse(tbx_wip_count.Text) + 1;
            _modulePrefix = sFac + moduleType + pidType + woSeq + iWorkshop.ToString() + sDate;
            for (int i = 0; i < int.Parse(tbx_label_count.Text); i++)
            {
                dgv_module_id.Rows.Add();

                string moduleId = _modulePrefix + (iStart + i).ToString("0000");

                dgv_module_id.Rows[i].Cells[0].Value = moduleId;
            }

            //取消可编辑状态，防止错绑
            cbx_workorder.Enabled = false;
            cbx_workshop.Enabled = false;

        }

        /// <summary>
        /// 确认按钮-写入数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ok_Click(object sender, EventArgs e)
        {
            #region 检查是否先生成序列号
            if (dgv_module_id.Rows.Count<1)
            {
                MessageBox.Show("请先生成序列号！");
                return;
            }
            #endregion

            //写入数据库
            string workorder = cbx_workorder.Text;                                 
            string workshop = ((ListItem)cbx_workshop.SelectedItem).ID;
            #region 开始流水号
            int iStart = 0;
            string sLastFlowID = _woWIPinfo[cbx_workorder.SelectedIndex].rowData[7];
            if (sLastFlowID == string.Empty)
            {
                iStart = 1;
            }
            else
            {
                iStart = int.Parse(sLastFlowID) + 1;
            }
            #endregion
            string startModuleIdx = iStart.ToString();
            string endModuleIdx=(int.Parse(startModuleIdx)+int.Parse(tbx_label_count.Text)).ToString();

            List<string> lstInputParas = new List<string>();
            lstInputParas.Add(workorder);                         //工单
            lstInputParas.Add(workshop);                          //车间
            lstInputParas.Add(startModuleIdx);                    //起始序列号
            lstInputParas.Add(_modulePrefix);                     //组件前缀
            lstInputParas.Add(endModuleIdx);                      //截至序列号
            lstInputParas.Add(_user);
            lstInputParas.Add(_createLabelDateTime);                //生码日期

            FuncTagInfo wipFunctag = new FuncTagInfo();
            wipFunctag = new FuncTagInfo();
            wipFunctag.FuncName = "CreateLabel";
            wipFunctag.InputParams = lstInputParas.ToArray();


            WcfRequests.SendWipRequest(onCreateLabelReceived, wipFunctag);


        }


        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            refleshPage();
            //dgv_module_id.Rows.Clear();
            //cbx_workorder.Enabled = true;
            //cbx_workshop.Enabled = true;

            //tbx_label_count.Text = string.Empty;

            //_modulePrefix = string.Empty;
        }

        #endregion
        
 
        #region wcf received
        /// <summary>
        /// 获取到当前用户
        /// </summary>
        /// <param name="receiveData"></param>
        private void onCurrentUserReceive(CallBackData receiveData)
        {
            _user = receiveData.Result;
        }



        /// <summary>
        /// 初始化下拉项
        /// </summary>
        /// <param name="receiveData"></param>
        private void onReceiveComoboxItems(CallBackData receiveData)
        {
            TextReader oTR = new StringReader(receiveData.Result);
            XElement oResult = XElement.Load(oTR);
            oTR.Close();

            IEnumerable<XElement> cReports =
                (from xNode in oResult.Descendants("Comoboxes")
                 select xNode);

            foreach (XElement oModuleNode in cReports)
            {
                string ComoboxName = ((XElement)oModuleNode).Attribute("ComoboxName").Value.ToString();
                string displayName = ((XElement)oModuleNode).Attribute("displayName").Value.ToString();
                string sourceName = ((XElement)oModuleNode).Attribute("sourceName").Value.ToString();

                if (ComoboxName == BasicClass.comoboxItemName.Workshop)
                {
                    cbx_workshop.Items.Add(new ListItem(sourceName, displayName));
                }
            }

        }


        /// <summary>
        /// 初始化工单信息
        /// </summary>
        /// <param name="receiveData"></param>
        private void onWoInfoReceived(CallBackData receiveData)
        {
            if (!receiveData.bIsOK)
            {
                MessageBox.Show("系统中没有此车间的工单");
                return;
            }

            _woWIPinfo = receiveData.RowDatas;

            cbx_workorder.Items.Clear();

            for (int i = 0; i < _woWIPinfo.Count(); i++)
            {
                cbx_workorder.Items.Add(_woWIPinfo[i].rowData[0]);
            }

            tbx_count_remain.Text = string.Empty;
            tbx_plan_count.Text = string.Empty;
            tbx_wip_count.Text = string.Empty;

            cbx_workorder.SelectedIndex = -1;   //工单号清空

            _modulePrefix = string.Empty;   //组件前缀清空
            dgv_module_id.Rows.Clear();     //清空组件序列号

            tbx_label_count.Text = string.Empty;

            cbx_workorder.Enabled = true;       //使能工单选项
        }

        /// <summary>
        /// 写入数据库返回
        /// </summary>
        /// <param name="receiveData"></param>
        private void onCreateLabelReceived(CallBackData receiveData)
        {
            if (!receiveData.bIsOK)
            {
                MessageBox.Show(receiveData.Result);
                return;
            }

            //打印序列号  待完善
            //string strWriteFileStream = BuildModuleIDLabelContent(_modulePrefix, Convert.ToInt32(tbx_wip_count.Text)+1, Convert.ToInt32(tbx_label_count.Text));
            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\BarcodePrint\ModulePrint.cmd", false))
            //{
            //    file.WriteLine(strWriteFileStream);
            //}

            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\BarcodePrint\usb.bat", false))
            //{
            //    file.WriteLine(@"net use LPT3: \\127.0.0.1\LabelPrint");
            //    file.WriteLine(@"print /D:LPT3 C:\BarcodePrint\ModulePrint.cmd pause");
            //    //file.WriteLine(@"pause");
            //}

            //try
            //{
            //    System.Diagnostics.Process.Start(@"c:\BarcodePrint\usb.bat");
            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            refleshPage();
        }


        #endregion


        #region common function
        /// <summary>
        /// 刷新页面
        /// </summary>
        private void refleshPage()
        {
             _woWIPinfo = null;

            List<string> lstInputParas = new List<string>();
            lstInputParas.Add(((ListItem)cbx_workshop.SelectedItem).ID);    //车间

            FuncTagInfo wipFunctag = new FuncTagInfo();
            wipFunctag = new FuncTagInfo();
            wipFunctag.FuncName = "GetWoWipInfo";
            wipFunctag.InputParams = lstInputParas.ToArray();


            WcfRequests.SendWipRequest(onWoInfoReceived, wipFunctag);

            //tbx_count_remain.Text = string.Empty;
            //tbx_plan_count.Text = string.Empty;
            //tbx_wip_count.Text = string.Empty;

            //cbx_workorder.SelectedIndex = -1;   //工单号清空

            //_modulePrefix = string.Empty;   //组件前缀清空
            //dgv_module_id.Rows.Clear();     //清空组件序列号

            //cbx_workorder.Enabled = true;       //使能工单选项

           
        }


        private string BuildModuleIDLabelContent(string sPrefix, Int32 lStartSerialNo, Int32 lAddNum)
        {
            string sContent = "";

            sContent = "^Q17,3" + "\r\n";
            sContent = sContent + "^W100" + "\r\n";
            sContent = sContent + "^H9" + "\r\n";
            sContent = sContent + "^P" + lAddNum + "\r\n";
            sContent = sContent + "^S2" + "\r\n";
            sContent = sContent + "^AT" + "\r\n";
            sContent = sContent + "^C1" + "\r\n";
            sContent = sContent + "^R0" + "\r\n";
            sContent = sContent + "~Q+0" + "\r\n";
            sContent = sContent + "^O0" + "\r\n";
            sContent = sContent + "^D0" + "\r\n";
            sContent = sContent + "^E12" + "\r\n";
            sContent = sContent + "~R200" + "\r\n";
            sContent = sContent + "^L" + "\r\n";
            sContent = sContent + "Dy2-Me-dd" + "\r\n";
            sContent = sContent + "Th:m:s" + "\r\n";
            sContent = sContent + "C0," + lStartSerialNo.ToString("0000") + ",+1,Prompt" + "\r\n";
            sContent = sContent + "BQ,445,2,2,4,24,0,0," + sPrefix + "^C0" + "\r\n";
            sContent = sContent + "BQ,33,78,2,3,26,0,0," + sPrefix + "^C0" + "\r\n";
            sContent = sContent + "Y30,17,Logo" + "\r\n";
            sContent = sContent + "ATA,38,100,36,33,0,0,0,0," + sPrefix + "^C0" + "\r\n";
            sContent = sContent + "ATA,452,22,36,33,0,0,0,0," + sPrefix + "^C0" + "\r\n";
            sContent = sContent + "BQ,537,80,2,4,24,0,0," + sPrefix + "^C0" + "\r\n";
            sContent = sContent + "ATA,452,100,36,33,0,0,0,0," + sPrefix + "^C0" + "\r\n";
            sContent = sContent + "E" + "\r\n";

            return sContent;
        }
    
        #endregion
        
    }
}
