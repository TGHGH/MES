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
//using WIPWcfService;
using Module.BasicClassLib;
using System.IO;
using System.Xml.Linq;
using WcfRequestNS.ServiceReference;

namespace Producing
{
    public partial class CellBinding : Form
    {
        #region local variables
        string _user = string.Empty;//当前用户
        RowData[] _eqpInfo = null;
        string _currentSite = string.Empty;     //当前站点
        string _currentEqp = string.Empty;      //当前设备
        //DataTable _dataTable = null;
        Timer changeColorTimer = new Timer();
        int iScanCount = 0;

        Color color = new Color();
        #endregion
        
        string _currentBatch = string.Empty;

        public CellBinding()
        {
            InitializeComponent();
        }

        private void CellBinding_Load(object sender, EventArgs e)
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
            //inParameters.Add("equipment");
            inParameters.Add(BasicClass.comoboxItemName.Hulian_Comp);
            inParameters.Add(BasicClass.comoboxItemName.Huiliu_Comp);
            inParameters.Add(BasicClass.comoboxItemName.Workshop);
            //inParameters.Add("cell_size");
            //inParameters.Add("cell_consumption");
            //inParameters.Add("cell_type");
            //inParameters.Add("wo_type");

            functionTag = new FuncTagInfo();
            functionTag.FuncName = "GetComoboxItems";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onReceiveComoboxItems, functionTag);
            #endregion

            #region 物料信息明细datagridView
            dgv_cellbatch_info.AllowUserToAddRows = false;
            dgv_cellbatch_info.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_cellbatch_info.Font = new System.Drawing.Font("宋体", 12, FontStyle.Regular);
            dgv_cellbatch_info.ReadOnly = true;
            dgv_cellbatch_info.Columns.Add("columnBatchNo", "物料批次");
            dgv_cellbatch_info.Columns.Add("columnTotalCount", "批次数量");
            dgv_cellbatch_info.Columns.Add("columnCurrentCount", "剩余数量");
            dgv_cellbatch_info.Columns.Add("cell_comp", "电池片厂商");
            #endregion

            #region trigger
            //扫描电池片批次
            tbx_cell_batch.KeyDown += new KeyEventHandler(tbx_cell_batch_KeyDown);
            
            //扫描组件序列号
            tbx_module_id.KeyDown += new KeyEventHandler(tbx_module_id_KeyDown);

            //选择设备
            cbx_eqp.SelectedIndexChanged += new EventHandler(cbx_eqp_SelectedIndexChanged);

            //选择车间
            cbx_workshop.SelectedIndexChanged += new EventHandler(cbx_workshop_SelectedIndexChanged);
            
            #endregion

            cbx_eqp.Enabled = false;
            tbx_module_id.Enabled = false;
            btn_scan_count.Text = "0";

            changeColorTimer.Interval = 1000 * 3;//2 second
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
            this.BackColor = color;
            //toolStripStatusLabelRight.Text = "";
            //toolStripStatusLabelRight.BackColor = Color.Transparent;
            //machine_id.Focus();
        }
        
        
        /// <summary>
        /// 选择车间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cbx_workshop_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbx_workshop.Enabled = false;

            List<string> lstInputParas = new List<string>();
            lstInputParas.Add(BasicClass.comoboxItemName.WeldEqpType);
            lstInputParas.Add(((ListItem)cbx_workshop.SelectedItem).ID);

            FuncTagInfo wipFunctag = new FuncTagInfo();
            wipFunctag = new FuncTagInfo();
            wipFunctag.FuncName = "GetEQPInfo";
            wipFunctag.InputParams = lstInputParas.ToArray();


            WcfRequests.SendWipRequest(onGetEQPInfoReceived, wipFunctag);

        }
        
        
        /// <summary>
        /// 改变当前站点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cbx_eqp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //代完善，用具体数字不是个好方法
            tbx_current_site.Text = _eqpInfo[cbx_eqp.SelectedIndex].rowData[2];
            _currentSite = tbx_current_site.Text;
            _currentEqp = _eqpInfo[cbx_eqp.SelectedIndex].rowData[1];

            tbx_module_id.Enabled = true;
            cbx_eqp.Enabled = false;
        }

        /// <summary>
        /// 扫描组件号
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
        /// 扫描电池片批号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tbx_cell_batch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                iScanCount = 0;
                btn_scan_count.Text = iScanCount.ToString();
                _currentBatch = tbx_cell_batch.Text.Trim();

                //获取批号信息
                FuncTagInfo functionTag = new FuncTagInfo();

                List<string> lstInputParas = new List<string>();
                lstInputParas.Add(_currentBatch);

                functionTag.FuncName = "GetCellBatchInfo";
                functionTag.InputParams = lstInputParas.ToArray();

                WcfRequests.SendWipRequest(onCellBatchInfoReceive, functionTag);
            }
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ok_Click(object sender, EventArgs e)
        {
            #region 信息检查
            //if (tbx_module_id.Text.Trim()==string.Empty)
            //{
            //    MessageBox.Show("请输入组件序列号");
            //    return;
            //}
            tbx_module_id.Text = tbx_module_id.Text.Trim().ToUpper();
            if (!System.Text.RegularExpressions.Regex.IsMatch(tbx_module_id.Text.Trim(), BasicClass.sModulePattern))
            {
                //MessageBox.Show("组件序列号输入有误！");
                toolStripStatusLabelRight.Text = "组件序列号输入有误！";

                this.BackColor = Color.Red;
                changeColorTimer.Start();

                tbx_module_id.Focus();
                tbx_module_id.SelectAll();
                return;
            }

            if (cbx_eqp.Text==string.Empty||
                cbx_workshop.Text==string.Empty)
            {
                //MessageBox.Show("必输项不能为空！");
                toolStripStatusLabelRight.Text = "必输项不能为空！";

                this.BackColor = Color.Red;
                changeColorTimer.Start();

                tbx_module_id.Focus();
                tbx_module_id.SelectAll();
                return;
            }

            if (dgv_cellbatch_info.RowCount<1)
            {
                //MessageBox.Show("请先扫描电池片批次信息");
                toolStripStatusLabelRight.Text = "请先扫描电池片批次信息！";

                this.BackColor = Color.Red;
                changeColorTimer.Start();

                tbx_cell_batch.Focus();
                tbx_cell_batch.SelectAll();
                return;
            }


            //判断当前数量是否大于所要扣除的数量，如果小于则不扣；
            int currentBatchCount = int.Parse(dgv_cellbatch_info.Rows[0].Cells["columnCurrentCount"].Value.ToString());

            if (currentBatchCount < 60)
            {
                //MessageBox.Show("当前物料数量不足，请更新批次！");
                toolStripStatusLabelRight.Text = "当前物料数量不足，请更新批次！";

                this.BackColor = Color.Red;
                changeColorTimer.Start();
                tbx_module_id.Focus();
                tbx_module_id.SelectAll();
                return;
            }

            #endregion

            string module_id = tbx_module_id.Text.Trim();
            tbx_module_id.Text = string.Empty;
            tbx_cell_batch.Text = string.Empty;


            ////记录到数据库中 电池片绑定没有检查站点，因为是第一站，无所谓
            FuncTagInfo functionTag = new FuncTagInfo();

            List<string> lstInputParas = new List<string>();
            lstInputParas.Add(_currentBatch);                               //批次号
            lstInputParas.Add(module_id);                   //组件号
            lstInputParas.Add(cbx_hulian_comp.Text);                        //互联条
            lstInputParas.Add(cbx_huiliu_comp.Text);                        //汇流条
            lstInputParas.Add(_currentEqp);     //设备号
            lstInputParas.Add(_user);
            lstInputParas.Add(((ListItem)cbx_workshop.SelectedItem).ID);    //车间
            lstInputParas.Add(BasicClass.getTime());

            functionTag.FuncName = "CellBinding";
            functionTag.InputParams = lstInputParas.ToArray();

            WcfRequests.SendWipRequest(onCellBindingReceive, functionTag);
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
        /// 获取电池片批次信息
        /// </summary>
        /// <param name="wipDataReceive"></param>
        private void onCellBatchInfoReceive(CallBackData wipDataReceive)
        {
            if (!wipDataReceive.bIsOK)
            {
                tbx_module_id.Enabled = false;
                //MessageBox.Show(wipDataReceive.Result);
                //return;

                this.BackColor = Color.Red;
                toolStripStatusLabelRight.Text = wipDataReceive.Result;
                toolStripStatusLabelRight.BackColor = Color.Red;

                tbx_cell_batch.Focus();
                tbx_cell_batch.SelectAll();

                changeColorTimer.Start();

                return;
            }

            tbx_module_id.Enabled = true;

            dgv_cellbatch_info.Rows.Clear();

            
            //DataTable dt = new DataTable();
            RowData[] row40data = wipDataReceive.RowDatas;

            for (int i = 0; i < row40data.Length; i++)
            {
                dgv_cellbatch_info.Rows.Add();
                

                string[] rowdata = row40data[i].rowData;

                for (int j = 0; j < rowdata.Length; j++)
                {
                    dgv_cellbatch_info.Rows[i].Cells[j].Value = rowdata[j];
                }
            }

            tbx_module_id.Focus();
            tbx_module_id.SelectAll();
        }


        /// <summary>
        /// 记录到数据库中
        /// </summary>
        /// <param name="wipDataReceive"></param>
        private void onCellBindingReceive(CallBackData wipDataReceive)
        {
            if (!wipDataReceive.bIsOK)
            {
                //MessageBox.Show(wipDataReceive.Result);
                toolStripStatusLabelRight.Text = wipDataReceive.Result;
                this.BackColor = Color.Red;
                toolStripStatusLabelRight.BackColor = Color.Red;

                tbx_module_id.Focus();
                tbx_module_id.SelectAll();

                changeColorTimer.Start();
                return;
            }

            iScanCount++;
            btn_scan_count.Text = iScanCount.ToString();
            
            dgv_cellbatch_info.Rows.Clear();

            //DataTable dt = new DataTable();
            RowData[] row40data = wipDataReceive.RowDatas;

            for (int i = 0; i < row40data.Length; i++)
            {
                dgv_cellbatch_info.Rows.Add();


                string[] rowdata = row40data[i].rowData;

                for (int j = 0; j < rowdata.Length; j++)
                {
                    dgv_cellbatch_info.Rows[i].Cells[j].Value = rowdata[j];
                }
            }

            btn_site_count.Text = wipDataReceive.Result;

            toolStripStatusLabelRight.Text = "插入数据成功";
            toolStripStatusLabelRight.BackColor = Color.Green;
            changeColorTimer.Start();

            tbx_module_id.Focus();
            tbx_module_id.SelectAll();

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

                if (ComoboxName == BasicClass.comoboxItemName.Hulian_Comp)
                {
                    cbx_hulian_comp.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Huiliu_Comp)
                {
                    cbx_huiliu_comp.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Workshop)
                {
                    cbx_workshop.Items.Add(new ListItem(sourceName, displayName));
                }
            }
        }


        /// <summary>
        /// 获取到机台信息
        /// </summary>
        /// <param name="receiveData"></param>
        private void onGetEQPInfoReceived(CallBackData receiveData)
        {
            tbx_current_site.Text = string.Empty;
            _currentSite = string.Empty;
            _currentEqp = string.Empty;
            if (!receiveData.bIsOK)
            {
                MessageBox.Show("系统中没有找到合适的机台");
                cbx_workshop.Enabled = true;
                return;
            }

            _eqpInfo = receiveData.RowDatas;


            for (int i = 0; i < _eqpInfo.Count(); i++)
            {
                cbx_eqp.Items.Add(_eqpInfo[i].rowData[0]);
            }

            cbx_eqp.Enabled = true;
            //tbx_module_id.Enabled = true;
        }

        
        #endregion

        
    }
}
