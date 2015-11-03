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
using WcfRequestNS.ServiceReference;
//using WIPWcfService;

namespace Producing
{
    public partial class LayUp : Form
    {
        #region local variables
        
        string _user = string.Empty;            //当前用户
        RowData[] _eqpInfo = null;              //设备列表
        string _currentSite = string.Empty;     //当前站点
        string _currentEqp = string.Empty;      //当前设备

        Timer changeColorTimer = new Timer();
        Color color = new Color();
        #endregion
        
        public LayUp()
        {
            InitializeComponent();
        }

        private void LayUp_Load(object sender, EventArgs e)
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
            inParameters.Add(BasicClass.comoboxItemName.Glass_Comp);
            inParameters.Add(BasicClass.comoboxItemName.TPT_Comp);
            inParameters.Add(BasicClass.comoboxItemName.EVA_comp);
            inParameters.Add(BasicClass.comoboxItemName.Glass_Desc);
            inParameters.Add(BasicClass.comoboxItemName.TPT_Desc);
            inParameters.Add(BasicClass.comoboxItemName.EVA_desc);
            inParameters.Add(BasicClass.comoboxItemName.Workshop);

            functionTag = new FuncTagInfo();
            functionTag.FuncName = "GetComoboxItems";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onReceiveComoboxItems, functionTag);
            #endregion

            #region trigger
            //改变设备机台
            cbx_eqp.SelectedIndexChanged += new EventHandler(cbx_eqp_SelectedIndexChanged);

            //扫描组件序列号
            tbx_module_id.KeyDown += new KeyEventHandler(tbx_module_id_KeyDown);

            //选择车间
            cbx_workshop.SelectedIndexChanged += new EventHandler(cbx_workshop_SelectedIndexChanged);

            #endregion

            tbx_module_id.Enabled = false;
            cbx_eqp.Enabled = false;

            changeColorTimer.Interval = 1000 * 15;//2 second
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
            cbx_workshop.Enabled = false;

            #region 获取设备机台号
            List<string> lstInputParas = new List<string>();
            lstInputParas.Add(BasicClass.comoboxItemName.LayUpEqpType);
            lstInputParas.Add(((ListItem)cbx_workshop.SelectedItem).ID);

            FuncTagInfo wipFunctag = new FuncTagInfo();
            wipFunctag = new FuncTagInfo();
            wipFunctag.FuncName = "GetEQPInfo";
            wipFunctag.InputParams = lstInputParas.ToArray();


            WcfRequests.SendWipRequest(onGetEQPInfoReceived, wipFunctag);
            #endregion
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
        /// 改变当前站点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cbx_eqp_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbx_current_site.Text=_eqpInfo[cbx_eqp.SelectedIndex].rowData[2];
            _currentSite = tbx_current_site.Text;
            _currentEqp = _eqpInfo[cbx_eqp.SelectedIndex].rowData[1];

            tbx_module_id.Enabled = true;
            cbx_eqp.Enabled = false;
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ok_Click(object sender, EventArgs e)
        {
            tbx_module_id.Text = tbx_module_id.Text.Trim().ToUpper();
            #region 检查输入项
            if (cbx_glass_comp.Text == string.Empty ||
                cbx_tpt_comp.Text == string.Empty ||
                cbx_eva_comp.Text == string.Empty ||
                cbx_glass_desc.Text == string.Empty ||
                cbx_tpt_desc.Text == string.Empty ||
                cbx_eva_desc.Text == string.Empty)
            {
                //MessageBox.Show("必选项不能为空！");

                toolStripStatusLabelRight.Text = "必选项不能为空！";
                toolStripStatusLabelRight.BackColor = Color.Red;

                this.BackColor = Color.Red;
                changeColorTimer.Start();

                tbx_module_id.Focus();
                tbx_module_id.SelectAll();

                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(tbx_module_id.Text, BasicClass.sModulePattern))
            {
                //MessageBox.Show("您输入的组件序列号格式有误，请检查！");

                toolStripStatusLabelRight.Text = "您输入的组件序列号格式有误，请检查！";
                toolStripStatusLabelRight.BackColor = Color.Red;

                this.BackColor = Color.Red;
                changeColorTimer.Start();

                tbx_module_id.Focus();
                tbx_module_id.SelectAll();

                return;
            }
            #endregion

            string module_id = tbx_module_id.Text.Trim();
            tbx_module_id.Text = string.Empty;

            tbx_module_id.Enabled = false;
            List<string> inParameters = new List<string>();
            inParameters.Add(module_id);                               //组件号
            inParameters.Add(_currentSite);                                     //当前站点
            inParameters.Add(_currentEqp);                                      //当前设备
            inParameters.Add(((ListItem)cbx_glass_comp.SelectedItem).ID);       //玻璃厂商
            inParameters.Add(((ListItem)cbx_tpt_comp.SelectedItem).ID);         //背板厂商
            inParameters.Add(((ListItem)cbx_eva_comp.SelectedItem).ID);         //eva厂商
            inParameters.Add(((ListItem)cbx_glass_desc.SelectedItem).ID);       //玻璃规格
            inParameters.Add(((ListItem)cbx_tpt_desc.SelectedItem).ID);         //背板规格
            inParameters.Add(((ListItem)cbx_eva_desc.SelectedItem).ID);         //eva规格
            inParameters.Add(_user);
            inParameters.Add(((ListItem)cbx_workshop.SelectedItem).ID);         //车间
            inParameters.Add(BasicClass.getTime());                             //白晚班        
            


            FuncTagInfo functionTag = new FuncTagInfo();
            functionTag.FuncName = "InsertModuleLayupInfo";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onInsertModuleLayupInfoReceived, functionTag);
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

                if (ComoboxName == BasicClass.comoboxItemName.Glass_Comp)
                {
                    cbx_glass_comp.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.TPT_Comp)
                {
                    cbx_tpt_comp.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.EVA_comp)
                {
                    cbx_eva_comp.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Glass_Desc)
                {
                    cbx_glass_desc.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.TPT_Desc)
                {
                    cbx_tpt_desc.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.EVA_desc)
                {
                    cbx_eva_desc.Items.Add(new ListItem(sourceName, displayName));
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
        }

        /// <summary>
        /// 插入叠层信息返回
        /// </summary>
        /// <param name="receiveData"></param>
        private void onInsertModuleLayupInfoReceived(CallBackData receiveData)
        {
            tbx_module_id.Enabled = true;
            if (!receiveData.bIsOK)
            {
                toolStripStatusLabelRight.Text = receiveData.Result;
                toolStripStatusLabelRight.BackColor = Color.Red;

                this.BackColor = Color.Red;
                changeColorTimer.Start();

                tbx_module_id.Focus();
                tbx_module_id.SelectAll();

                //MessageBox.Show(receiveData.Result);
                return;
            }

            btn_scan_count.Text = receiveData.Result;

            this.BackColor = Color.LightGreen;
            toolStripStatusLabelRight.Text = "成功插入一条新数据";
            toolStripStatusLabelRight.BackColor = Color.Green;

            changeColorTimer.Start();

            tbx_module_id.Focus();
            tbx_module_id.SelectAll();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
