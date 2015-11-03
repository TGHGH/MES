using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using GeneralWcfService;
using WcfRequestNS;
using System.Net;
using System.IO;
using System.Xml.Linq;
using Module.BasicClassLib;
//using WIPWcfService;
using WcfRequestNS.ServiceReference;

namespace Producing
{
    public partial class Pack : Form
    {
        string _user = string.Empty;//当前用户
        Dictionary<string, string> _dicPalletPattern = new Dictionary<string, string>();
        string _currentPallet = string.Empty;//当前托盘号
        string _currentWorkshop = string.Empty;     //当前车间

        Timer changeColorTimer = new Timer();
        Color color = new Color();

        public Pack()
        {
            InitializeComponent();
        }

        private void Pack_Load(object sender, EventArgs e)
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
            inParameters.Add(Module.BasicClassLib.BasicClass.comoboxItemName.Workshop);
            //inParameters.Add("frame_comp");
            //inParameters.Add("glue_comp");
            //inParameters.Add("jbox_desc");
            //inParameters.Add("frame_desc");
            //inParameters.Add("glue_desc");

            functionTag = new FuncTagInfo();
            functionTag.FuncName = "GetComoboxItems";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onReceiveComoboxItems, functionTag);
            #endregion

            #region 设置datagridView
            dgv_pallet_info.AllowUserToAddRows = false;
            dgv_pallet_info.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_pallet_info.Font = new System.Drawing.Font("宋体", 12, FontStyle.Regular);
            dgv_pallet_info.ReadOnly = true;
            dgv_pallet_info.Columns.Add("carton_no", "托盘号");
            dgv_pallet_info.Columns.Add("module_id", "组件序列号");
            dgv_pallet_info.Columns.Add("pmax", "最大功率");
            dgv_pallet_info.Columns.Add("ipm", "最大电流");
            #endregion

            #region trigger
            tbx_module_id.KeyDown += new KeyEventHandler(tbx_module_id_KeyDown);

            cbx_workshop.SelectedIndexChanged += new EventHandler(cbx_workshop_SelectedIndexChanged);
            #endregion

            tbx_module_id.Enabled = false;

            //创建文件夹，打印用
            if (!Directory.Exists(@"c:\BarcodePrint"))
            {
                Directory.CreateDirectory(@"c:\BarcodePrint");
            }

            checkBox_final_grade.Checked = true;
            checkBox_imp.Checked = true;
            checkBox_module_color.Checked = true;
            checkBox_module_count.Checked = true;
            checkBox_power.Checked = true;
            checkBox_sale_order.Checked = true;
            checkBox_workorder.Checked = true;


            changeColorTimer.Interval = 1000 * 2;//2 second
            changeColorTimer.Tick += new EventHandler(changeColorTimer_Tick);

            color = this.BackColor;
        }

        

        #region trigger received
        /// <summary>
        /// 重置状态栏颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void changeColorTimer_Tick(object sender, EventArgs e)
        {
            changeColorTimer.Stop();
            //toolStripStatusLabelRight.Text = "";
            //toolStripStatusLabelRight.BackColor = Color.Transparent;

            this.BackColor = color;
            //machine_id.Focus();
        }
        
        /// <summary>
        /// 选择车间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cbx_workshop_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbx_module_id.Enabled = true;
            cbx_workshop.Enabled = false;

            _currentWorkshop = ((ListItem)cbx_workshop.SelectedItem).ID;


        }

        /// <summary>
        /// 扫描组件序列号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tbx_module_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                btn_ok_Click(this, EventArgs.Empty);
            }

        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ok_Click(object sender, EventArgs e)
        {
            tbx_module_id.Text = tbx_module_id.Text.Trim().ToUpper();
            btn_ok.Enabled = false;
            tbx_module_id.Enabled = false;  

            List<string> inParameters = new List<string>();
            inParameters.Add(tbx_module_id.Text);                               //组件号

            FuncTagInfo functionTag = new FuncTagInfo();
            functionTag.FuncName = "GetModuleInfo";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendWipRequest(onGetModuleInfoReceived, functionTag);
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
        /// 获取完组件信息
        /// </summary>
        /// <param name="WipDataReceived"></param>
        private void onGetModuleInfoReceived(CallBackData WipDataReceived)
        {
            //-->有问题，则show出来
            if (!WipDataReceived.bIsOK)
            {
                btn_ok.Enabled = true;
                tbx_module_id.Enabled = true;  

                //MessageBox.Show(WipDataReceived.Result);

                this.BackColor = Color.Red;
                changeColorTimer.Start();

                toolStripStatusLabelRight.Text = WipDataReceived.Result;
                toolStripStatusLabelRight.BackColor = Color.Red;

                tbx_module_id.Focus();
                tbx_module_id.SelectAll();

                return;
            }

            //-->没问题，则继续
            string[] rowDatas = WipDataReceived.RowDatas[0].rowData;

            int i=0;
            string module_id =rowDatas[i++];
            string workorder = rowDatas[i++];
            string sale_order = rowDatas[i++];
            string final_grade = rowDatas[i++];
            string power_grade = rowDatas[i++];
            string imp_grade = rowDatas[i++];
            string cell_consumption = rowDatas[i++];
            string module_color = rowDatas[i++];
            string pack_count = rowDatas[i++];
            string jbox_desc = rowDatas[i++];
            string frame_desc = rowDatas[i++];
            string pmax = rowDatas[i++];
            string vpm = rowDatas[i++];
            string ipm = rowDatas[i++];
            string ff = rowDatas[i++];
            string voc = rowDatas[i++];
            string isc = rowDatas[i++];
            string test_time = rowDatas[i++];
            string surf_temp = rowDatas[i++];
            string modult_type = rowDatas[i++];
            string cristal_type = rowDatas[i++];
            string wrokshop = rowDatas[i++];
            string cell_type = rowDatas[i++];
            string product_no1 = rowDatas[i++];
            string product_no2 = rowDatas[i++];

            //test_time = Convert.ToDateTime(test_time).ToString("yyyy-MM-dd HH:mm:ss");
            //test_time = "1970-01-01 00:00:00";  //bug

            //====>检查信息是否完整

            #region MyRegion
            if (power_grade==string.Empty)
            {
                //MessageBox.Show("没有功率测试信息");
                tbx_module_id.Enabled = true;

                this.BackColor = Color.Red;
                changeColorTimer.Start();

                toolStripStatusLabelRight.Text = "没有功率测试信息";
                toolStripStatusLabelRight.BackColor = Color.Red;

                tbx_module_id.Focus();
                tbx_module_id.SelectAll();


                return;
            }
            #endregion

            //==>显示组件工单，质量等级，功率等级，电流等级
            tbx_work_order.Text = workorder;
            btn_final_grade.Text = final_grade;
            btn_power_grade.Text = power_grade;
            btn_Imp_Grade.Text = imp_grade;

            //====>构建pattern，作为拼托盘依据
            string sPattern = string.Empty;
            if (checkBox_sale_order.Checked)
            {
                sPattern = sPattern + sale_order + "!";
            }
            if (checkBox_workorder.Checked)
            {
                sPattern = sPattern + workorder + "!";
            }
            if (checkBox_final_grade.Checked)
            {
                sPattern = sPattern + final_grade + "!";
            }
            if (checkBox_power.Checked)
            {
                sPattern = sPattern + power_grade + "!";
            }
            if (checkBox_imp.Checked)
            {
                sPattern = sPattern + imp_grade + "!";
            }
            if (checkBox_module_color.Checked)
            {
                sPattern = sPattern + module_color + "!";
            }

            if (sPattern==string.Empty)
            {
                //MessageBox.Show("包装条件不能为空！");
                this.BackColor = Color.Red;
                changeColorTimer.Start();

                toolStripStatusLabelRight.Text = "包装条件不能为空!";
                toolStripStatusLabelRight.BackColor = Color.Red;

                tbx_module_id.Focus();
                tbx_module_id.SelectAll();
                return;
            }

            //====>如果当前没有托盘，那么则产生托盘，如果有，则跟托盘的pattern做比较，相同则包到一块儿
            #region MyRegion
            bool FIND_THE_SAME_PATTERN = false;
            //字典中有托盘，则比较托盘的pattern和当前pattern是否相同
            foreach (var item in _dicPalletPattern)
            {
                if (item.Value==sPattern)
                {
                    _currentPallet = item.Key;
                    FIND_THE_SAME_PATTERN = true;
                }
            }

            List<string> inputParas = new List<string>();
            if (FIND_THE_SAME_PATTERN)
            {
                inputParas.Add(_currentPallet);
            }
            else
            {
                #region 如果需要自动分拖，则将此段代码注释掉
                if (_dicPalletPattern.Count>0)
                {
                    this.BackColor = Color.Red;
                    changeColorTimer.Start();

                    toolStripStatusLabelRight.Text = "这块组件不符合成拖条件！";
                    toolStripStatusLabelRight.BackColor = Color.Red;

                    tbx_module_id.Enabled = true;
                    tbx_module_id.Focus();
                    tbx_module_id.SelectAll();

                    return;
                }
                #endregion

                inputParas.Add("");
            }
            inputParas.Add(sPattern);
            inputParas.Add(pack_count);
            inputParas.Add(module_id);
            inputParas.Add(final_grade);
            inputParas.Add(power_grade);
            inputParas.Add(imp_grade);
            inputParas.Add(pmax);
            inputParas.Add(vpm);
            inputParas.Add(ipm);
            inputParas.Add(ff);
            inputParas.Add(voc);
            inputParas.Add(isc);
            inputParas.Add(test_time);
            inputParas.Add(surf_temp);
            inputParas.Add(modult_type);
            inputParas.Add(cristal_type);
            inputParas.Add(wrokshop);
            inputParas.Add(module_color);
            inputParas.Add(cell_consumption);
            inputParas.Add(workorder);
            inputParas.Add(_user);
            inputParas.Add(cell_type);
            inputParas.Add(jbox_desc);
            inputParas.Add(frame_desc);
            inputParas.Add(product_no1);
            inputParas.Add(product_no2);

            FuncTagInfo wipFunctionTag = new FuncTagInfo();
            wipFunctionTag.FuncName = "GetPalletInfo";
            wipFunctionTag.InputParams = inputParas.ToArray();

            WcfRequests.SendWipRequest(onCreatePckInfoReceived, wipFunctionTag);

            #endregion

        }

        /// <summary>
        /// 获取到托盘信息
        /// </summary>
        /// <param name="wipDataReceived"></param>
        private void onCreatePckInfoReceived(CallBackData wipDataReceived)
        {
            if (!wipDataReceived.bIsOK)
            {
                //MessageBox.Show(wipDataReceived.Result);
                tbx_module_id.Enabled = true;
                btn_ok.Enabled = true;

                this.BackColor = Color.Red;
                changeColorTimer.Start();

                toolStripStatusLabelRight.Text = wipDataReceived.Result;
                toolStripStatusLabelRight.BackColor = Color.Red;

                tbx_module_id.Focus();
                tbx_module_id.SelectAll();
                return;
            }

            //==>如果托盘内组件数量等于设定数量，则自动打印，否则，把托盘信息加入字典
            string[] palletTag = wipDataReceived.Result.Split('|');
            int i=0;
            _currentPallet = palletTag[i++];                      //托盘号
            int iPrintCount = Convert.ToInt32(palletTag[i++]);    //预设包装数量
            string palletTagInfo = palletTag[i++];                //托盘pattern
            string palletPower = palletTag[i++];                //A4_功率
            string pallettype = palletTag[i++];                //A4_类型
            string jbox_spec = palletTag[i++];                //A4_线盒规格
            string frame_spec = palletTag[i++];                //A4_边框规格
            string pallet_class_grade= palletTag[i++];                //A4_质量等级
            string busbar_type = palletTag[i++];                //A4_三栅四删类型  需求改为了电流等级2015-10-07

            RowData[] rowDatas = wipDataReceived.RowDatas;//组件数量

            //显示包装数量
            btn_packing_count.Text = rowDatas.Count().ToString();

            if (rowDatas.Count()==iPrintCount)
            {//====>打印，并删除托盘字典中托盘号
                #region create file stream
                string strWriteFileStream =
                    _currentPallet + "," + palletPower + "," + pallettype + "," + jbox_spec + "," + frame_spec + "," + pallet_class_grade + ",";

                strWriteFileStream = strWriteFileStream + busbar_type+",";
                
                for (int idx = 0; idx < rowDatas.Length; idx++)
                {
                    string[] rowdata = rowDatas[idx].rowData;
                    string module_d_temp=rowdata[1];
                    strWriteFileStream = strWriteFileStream + module_d_temp + ",";
                }
                
                #endregion

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\BarcodePrint\A4Label.txt", false))
                {
                    file.WriteLine(strWriteFileStream);
                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\BarcodePrint\usb.bat", false))
                {
                    file.WriteLine("@echo off");
                    if (busbar_type=="三栅")
                    {
                        file.WriteLine("Bartend.exe /F=\"C:\\BarcodePrint\\China3.btw\" /D=\"C:\\BarcodePrint\\A4Label.txt\" /P /x");
                    }
                    else if (busbar_type=="三栅")
                    {
                        file.WriteLine("Bartend.exe /F=\"C:\\BarcodePrint\\China4.btw\" /D=\"C:\\BarcodePrint\\A4Label.txt\" /P /x");
                    }
                    //file.WriteLine("Bartend.exe /F=\"C:\\BarcodePrint\\China.btw\" /D=\"C:\\BarcodePrint\\A4Label.txt\" /P /x");
                }

                try
                {
                    System.Diagnostics.Process.Start(@"c:\BarcodePrint\usb.bat");
                }
                catch (Exception)
                {
                    throw;
                }

                _dicPalletPattern.Remove(_currentPallet);
                dgv_pallet_info.Rows.Clear();
                //printLabel();
                //清空相关空间内容
                btn_packing_count.Text = string.Empty;
                btn_power_grade.Text = string.Empty;
                btn_Imp_Grade.Text = string.Empty;
                btn_final_grade.Text = string.Empty;

                //==>更新托盘信息
                List<string> lstInparas = new List<string>();
                lstInparas.Add(_currentPallet);
                lstInparas.Add(iPrintCount.ToString());

                FuncTagInfo wipFunctionTag = new FuncTagInfo();
                wipFunctionTag.FuncName = "UpdatePalletInfo";
                wipFunctionTag.InputParams = lstInparas.ToArray();

                WcfRequests.SendWipRequest(onUpdatePalletInfoReceived, wipFunctionTag);
            }
            else
            {//====>加入托盘字典，并更新托盘明细信息
                if (!_dicPalletPattern.ContainsKey(_currentPallet))
                {
                    _dicPalletPattern.Add(_currentPallet, palletTagInfo);

                }
                
                dgv_pallet_info.Rows.Clear();
                DataTable dt = new DataTable();
                RowData[] row40data = wipDataReceived.RowDatas;
                for (i = 0; i < row40data.Length; i++)
                {
                    dgv_pallet_info.Rows.Add();

                    string[] rowdata = row40data[i].rowData;

                    for (int j = 0; j < rowdata.Length; j++)
                    {
                        dgv_pallet_info.Rows[i].Cells[j].Value = rowdata[j];
                    }
                }
                
            }
            
            btn_ok.Enabled = true;
            tbx_module_id.Enabled = true;
            tbx_module_id.Focus();
            tbx_module_id.SelectAll();
            
            
        }

        private void onUpdatePalletInfoReceived(CallBackData wipDataReceived)
        {
        //    tbx_module_id.Focus();
        //    tbx_module_id.SelectAll();
        }

        #endregion

        /// <summary>
        /// 打印条码
        /// </summary>
        private void printLabel()
        {
            MessageBox.Show("成功打印label");
        }

    }
}
