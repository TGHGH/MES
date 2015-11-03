using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WcfRequestNS.ServiceReference;
using System.Net;
using WcfRequestNS;
using Module.BasicClassLib;
using System.IO;
using System.Xml.Linq;

namespace MaterialPlan
{
    public partial class CellReturn : Form
    {
        #region local variables
        string _user = string.Empty;            //当前用户
        FuncTagInfo functionTag = null;

        Color color = new Color();
        Timer changeColorTimer = new Timer();

        int _iCurrentScanCount = 0;
        bool _isFstModule = true;
        string _currentPattern = string.Empty;
        List<string> lstScanedModule = new List<string>();
        string _currentBatch = string.Empty;
        string _cellConsumption = string.Empty;

        #endregion
        

        public CellReturn()
        {
            InitializeComponent();
        }

        private void CellReturn_Load(object sender, EventArgs e)
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

            functionTag = new FuncTagInfo();
            functionTag.FuncName = "GetComoboxItems";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onReceiveComoboxItems, functionTag);
            #endregion

            #region trigger
            tbx_module.KeyDown += new KeyEventHandler(tbx_module_KeyDown);
            #endregion

            #region 退库明细datagridView
            dgv_cellreturn_detail.AllowUserToAddRows = false;
            dgv_cellreturn_detail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_cellreturn_detail.Font = new System.Drawing.Font("宋体", 12, FontStyle.Regular);
            dgv_cellreturn_detail.ReadOnly = true;
            dgv_cellreturn_detail.Columns.Add("moduleID", "序列号");
            dgv_cellreturn_detail.Columns.Add("cellBatch", "批号");
            dgv_cellreturn_detail.Columns.Add("eqp", "机台");
            #endregion

            color = this.BackColor;

            changeColorTimer.Interval = 1000 * 3;//2 second
            changeColorTimer.Tick += new EventHandler(changeColorTimer_Tick);
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
            this.BackColor = color;
            //toolStripStatusLabelRight.Text = "";
            //toolStripStatusLabelRight.BackColor = Color.Transparent;
            //machine_id.Focus();


        }
        
        
        /// <summary>
        /// 扫描组件号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tbx_module_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                if (lstScanedModule.Contains(tbx_module.Text))
                {
                    return;
                }

                tbx_module.Enabled = false; 
                //获取批次信息
                List<string> inParameters = new List<string>();
                inParameters.Add(tbx_module.Text);

                functionTag = new FuncTagInfo();
                functionTag.FuncName = "GetModuleBingingInfo";
                functionTag.InputParams = inParameters.ToArray();

                WcfRequests.SendGeneralRequest(onGetModuleBingingInfoReceived, functionTag);
            }
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
        /// 获取到组件绑定信息
        /// </summary>
        /// <param name="receiveData"></param>
        private void onGetModuleBingingInfoReceived(CallBackData receiveData)
        {
            if (!receiveData.bIsOK)
            {
                tbx_module.Enabled = true;

                this.BackColor = Color.Red;
                toolStripStatusLabelRight.Text = receiveData.Result;
                toolStripStatusLabelRight.BackColor = Color.Red;

                tbx_module.Focus();
                tbx_module.SelectAll();

                changeColorTimer.Start();

                return;
            }

            string[] row40data = receiveData.Result.Split('|');
            string sPattern = row40data[1];
            string currentBatch = row40data[1];
            string cell_consumption = row40data[3];
            if (!_isFstModule)
            {
                if (sPattern!=_currentPattern)
                {
                    this.BackColor = Color.Red;
                    toolStripStatusLabelRight.Text = "不允许不同批次的片源退库";
                    toolStripStatusLabelRight.BackColor = Color.Red;

                    tbx_module.Focus();
                    tbx_module.SelectAll();

                    changeColorTimer.Start();

                    return;
                }
            }
            else
            {
                _isFstModule = false;
                _currentPattern = sPattern;
                _currentBatch = currentBatch;
                _cellConsumption = cell_consumption;
            }

            dgv_cellreturn_detail.Rows.Add();
            for (int i = 0; i < 3; i++)
            {
                dgv_cellreturn_detail.Rows[_iCurrentScanCount].Cells[i].Value = row40data[i];

            }

            _iCurrentScanCount++;
            btn_scanCount.Text = _iCurrentScanCount.ToString();

            lstScanedModule.Add(tbx_module.Text);

            toolStripStatusLabelRight.Text = "";
            toolStripStatusLabelRight.BackColor = Color.Transparent;

            tbx_module.Enabled = true;
            tbx_module.Focus();
            tbx_module.SelectAll();
        
        }

        private void onReleaseModuleBingingInfoReceived(CallBackData receiveData)
        {
            this.BackColor = Color.LightGreen;
            toolStripStatusLabelRight.Text = "接触绑定信息成功";
            toolStripStatusLabelRight.BackColor = Color.Green;
            changeColorTimer.Start();

            dgv_cellreturn_detail.Rows.Clear();
            tbx_module.Focus();
            tbx_module.SelectAll();


        }

        #endregion

        private void btn_ok_Click(object sender, EventArgs e)
        {
            tbx_module.Text = tbx_module.Text.Trim().ToUpper();
            #region check
            if (lstScanedModule.Count<1)
            {
                return;
            }
            #endregion
            List<string> inParameters = new List<string>();
            inParameters.Add(_user);                                                        //解绑人员
            inParameters.Add(_currentBatch);                                                //解绑批次
            int unBindingCellCount = _iCurrentScanCount * int.Parse(_cellConsumption);      
            inParameters.Add(unBindingCellCount.ToString());                                //解绑数量
            inParameters.AddRange(lstScanedModule);                                         //解绑组件

            functionTag = new FuncTagInfo();
            functionTag.FuncName = "ReleaseModuleBingingInfo";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onReleaseModuleBingingInfoReceived, functionTag);
            
            
            lstScanedModule.Clear();
            _iCurrentScanCount = 0;
            btn_scanCount.Text = string.Empty;
            _currentBatch = string.Empty;
            _currentPattern = string.Empty;
            _cellConsumption = string.Empty;


        }
        
    }
}
