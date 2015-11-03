using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using WIPWcfService;
//using GeneralWcfService;
using WcfRequestNS;
using System.Net;
using System.IO;
using System.Xml.Linq;
using Module.BasicClassLib;
using WcfRequestNS.ServiceReference;

namespace Producing
{
    public partial class Frame : Form
    {
        string _user = string.Empty;//当前用户
        RowData[] _eqpInfo = null;
        string _currentSite = string.Empty;     //当前站点
        string _currentEqp = string.Empty;      //当前设备
        Timer changeColorTimer = new Timer();

        Color color = new Color();

        public Frame()
        {
            InitializeComponent();
        }

        private void Frame_Load(object sender, EventArgs e)
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
            inParameters.Add(BasicClass.comoboxItemName.Jbox_Comp);
            inParameters.Add(BasicClass.comoboxItemName.Frame_Comp);
            inParameters.Add(BasicClass.comoboxItemName.Glue_Comp);
            inParameters.Add(BasicClass.comoboxItemName.Jbox_Desc);
            inParameters.Add(BasicClass.comoboxItemName.Frame_Desc);
            inParameters.Add(BasicClass.comoboxItemName.Glue_Desc);
            inParameters.Add(BasicClass.comoboxItemName.Workshop);
            inParameters.Add(BasicClass.comoboxItemName.Frame_glue_comp);
            inParameters.Add(BasicClass.comoboxItemName.Frame_glue_desc);

            functionTag = new FuncTagInfo();
            functionTag.FuncName = "GetComoboxItems";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onReceiveComoboxItems, functionTag);
            #endregion

            #region trigger
            //选择车间
            cbx_workshop.SelectedIndexChanged += new EventHandler(cbx_workshop_SelectedIndexChanged);
            
            //选择设备
            cbx_eqp.SelectedIndexChanged += new EventHandler(cbx_eqp_SelectedIndexChanged);

            //扫描组件号
            tbx_module_id.KeyDown += new KeyEventHandler(tbx_module_id_KeyDown);
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

            #region 获取设备机台号
            List<string> lstInputParas = new List<string>();
            lstInputParas.Add(BasicClass.comoboxItemName.FrameEqpType);
            lstInputParas.Add(((ListItem)cbx_workshop.SelectedItem).ID);

            FuncTagInfo wipFunctag = new FuncTagInfo();
            wipFunctag = new FuncTagInfo();
            wipFunctag.FuncName = "GetEQPInfo";
            wipFunctag.InputParams = lstInputParas.ToArray();


            WcfRequests.SendWipRequest(onGetEQPInfoReceived, wipFunctag);
            #endregion
        }

        /// <summary>
        /// 扫描组件
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
            tbx_current_site.Text = _eqpInfo[cbx_eqp.SelectedIndex].rowData[2];
            _currentSite = tbx_current_site.Text;
            _currentEqp = _eqpInfo[cbx_eqp.SelectedIndex].rowData[1];

            tbx_module_id.Enabled = true;
            cbx_eqp.Enabled = false;
        }
        
        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ok_Click(object sender, EventArgs e)
        {
            tbx_module_id.Text = tbx_module_id.Text.Trim().ToUpper();

            #region 检查输入项
            if (cbx_jbox_comp.Text == string.Empty ||
                cbx_frame_comp.Text == string.Empty ||
                cbx_glue_comp.Text == string.Empty ||
                cbx_jobx_desc.Text == string.Empty ||
                cbx_frame_desc.Text == string.Empty ||
                cbx_glue_desc.Text == string.Empty||
                cbx_frame_glue_comp.Text==string.Empty||
                cbx_fram_glue_desc.Text==string.Empty)
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

            if (!System.Text.RegularExpressions.Regex.IsMatch(tbx_module_id.Text,BasicClass.sModulePattern))
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

            string module_id = tbx_module_id.Text.Trim();
            tbx_module_id.Text = string.Empty;
            #endregion


            tbx_module_id.Enabled = false;
            List<string> inParameters = new List<string>();
            inParameters.Add(module_id);                               //组件号
            inParameters.Add(_currentSite);                                     //当前站点
            inParameters.Add(_currentEqp);                                      //当前设备
            inParameters.Add(((ListItem)cbx_jbox_comp.SelectedItem).ID);       //jbox厂商
            inParameters.Add(((ListItem)cbx_frame_comp.SelectedItem).ID);         //型材厂商
            inParameters.Add(((ListItem)cbx_glue_comp.SelectedItem).ID);         //硅胶厂商
            inParameters.Add(((ListItem)cbx_jobx_desc.SelectedItem).ID);       //jbox规格
            inParameters.Add(((ListItem)cbx_frame_desc.SelectedItem).ID);         //型材规格
            inParameters.Add(((ListItem)cbx_glue_desc.SelectedItem).ID);         //硅胶规格
            inParameters.Add(_user);
            inParameters.Add(((ListItem)cbx_workshop.SelectedItem).ID);         //车间
            inParameters.Add(BasicClass.getTime());                             //白晚班  
            inParameters.Add(((ListItem)cbx_frame_glue_comp.SelectedItem).ID); //型材胶厂商
            inParameters.Add(((ListItem)cbx_fram_glue_desc.SelectedItem).ID); //型材胶规格

            FuncTagInfo functionTag = new FuncTagInfo();
            functionTag.FuncName = "InsertMoudleFrameInfo";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onInsertModuleFrameInfoReceived, functionTag);
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

                if (ComoboxName == BasicClass.comoboxItemName.Jbox_Comp)
                {
                    cbx_jbox_comp.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Frame_Comp)
                {
                    cbx_frame_comp.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Glue_Comp)
                {
                    cbx_glue_comp.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Jbox_Desc)
                {
                    cbx_jobx_desc.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Frame_Desc)
                {
                    cbx_frame_desc.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Glue_Desc)
                {
                    cbx_glue_desc.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Workshop)
                {
                    cbx_workshop.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Frame_glue_desc)
                {
                    cbx_fram_glue_desc.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Frame_glue_comp)
                {
                    cbx_frame_glue_comp.Items.Add(new ListItem(sourceName, displayName));
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
        /// 插入装框信息返回
        /// </summary>
        /// <param name="receiveData"></param>
        private void onInsertModuleFrameInfoReceived(CallBackData receiveData)
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
            toolStripStatusLabelRight.Text = "成功插入一条新数据:" + tbx_module_id.Text;
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
