using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using WIPWcfService;
using WcfRequestNS;
//using WIPWcfService;
//using GeneralWcfService;
using System.IO;
using System.Xml.Linq;
using System.Net;
using Module.BasicClassLib;
using WcfRequestNS.ServiceReference;

namespace MaterialPlan
{
    public partial class BuildWO : Form
    {
        #region local variables
        Timer changeColorTimer = new Timer();
        string _user = string.Empty;//当前用户
        #endregion
        
        public BuildWO()
        {
            InitializeComponent();
        }

        private void BuildWO_Load(object sender, EventArgs e)
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
            inParameters.Add(BasicClass.comoboxItemName.WO_type);
            inParameters.Add(BasicClass.comoboxItemName.Cristal_Type);
            inParameters.Add(BasicClass.comoboxItemName.Cell_Size);
            inParameters.Add(BasicClass.comoboxItemName.Cell_Consumption);
            inParameters.Add(BasicClass.comoboxItemName.Cell_Type);
            inParameters.Add(BasicClass.comoboxItemName.Workflow);
            inParameters.Add(BasicClass.comoboxItemName.Module_color);
            inParameters.Add(BasicClass.comoboxItemName.Module_type);
            inParameters.Add(BasicClass.comoboxItemName.PID_type);
            inParameters.Add(BasicClass.comoboxItemName.Product_name1);
            inParameters.Add(BasicClass.comoboxItemName.Product_name2);

            functionTag = new FuncTagInfo();
            functionTag.FuncName = "GetComoboxItems";
            functionTag.InputParams = inParameters.ToArray(); ;

            WcfRequests.SendGeneralRequest(onReceiveComoboxItems, functionTag);
            #endregion


            BasicClass.EnterToTab(groupBox_woinfo);
            //asc.controllInitializeSize(this);

            //this.SizeChanged += new EventHandler(MachineMaintain_SizeChanged);

            changeColorTimer.Interval = 1000 * 60;//60 second
            changeColorTimer.Tick += new EventHandler(changeColorTimer_Tick);
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            List<string> sInputParas = new List<string>();

            #region 检查必输项
            if (comboBox_workshop.Text==string.Empty||                  //车间
                comboBox_wo_type.Text==string.Empty||                   //工单类型
                cbx_module_type.Text==string.Empty||                    //组件类型
                cbx_pid_type.Text==string.Empty||                       //pid类型
                comboBox_order_id.Text==string.Empty||                  //订单
                comboBox_producing_flow.Text==string.Empty||            //制造流程
                cbx_module_color.Text==string.Empty||                   //组件颜色
                cbx_module_type.Text==string.Empty||                    //组件类型
                comboBox_cristal_type.Text==string.Empty||              //晶体类型
                comboBox_cell_size.Text==string.Empty||                 //片源规格
                comboBox_cell_consumption.Text==string.Empty||          //片源用量
                comboBox_cell_type.Text==string.Empty||                 //片源类型
                cbx_pid_type.Text==string.Empty)
            {
                MessageBox.Show("必输项不能为空！");
                return;
            }

            if (!BasicClass.isNumeric(textBox_wo_qty.Text.Trim())||
                !BasicClass.isNumeric(textBox_wo_seq.Text.Trim()))
            {
                MessageBox.Show("非数字类型错误！");
                return;
            }

            #endregion

            sInputParas.Add(((ListItem)comboBox_workshop.SelectedItem).ID);            //车间
            sInputParas.Add(((ListItem)comboBox_wo_type.SelectedItem).ID);             //工单类型
            sInputParas.Add(((ListItem)cbx_module_type.SelectedItem).ID);            //组件类型
            sInputParas.Add(((ListItem)cbx_pid_type.SelectedItem).ID);           //pid类型
            sInputParas.Add(comboBox_order_id.Text);            //订单编号
            sInputParas.Add(textBox_customer.Text);            //客户
            sInputParas.Add(textBox_wo_qty.Text);          //投产总数
            sInputParas.Add(((ListItem)comboBox_producing_flow.SelectedItem).ID);           //制造流程
            sInputParas.Add(textBox_wo_seq.Text);            //工单次数
            sInputParas.Add(((ListItem)cbx_module_color.SelectedItem).ID);            //组件颜色
            sInputParas.Add(((ListItem)comboBox_cristal_type.SelectedItem).ID);          //晶体类型
            sInputParas.Add(((ListItem)comboBox_cell_size.SelectedItem).ID);           //片源规格
            sInputParas.Add(((ListItem)comboBox_cell_consumption.SelectedItem).ID);           //片源用量
            sInputParas.Add(((ListItem)comboBox_cell_type.SelectedItem).ID);           //片源类型

            if (comboBox_product_id1.Text.Trim()!=string.Empty)
            {
                sInputParas.Add(((ListItem)comboBox_product_id1.SelectedItem).ID);         //产品编码1
            }
            else
            {
                sInputParas.Add(string.Empty);         //产品编码1
            }
            if (comboBox_product_id2.Text.Trim()!=string.Empty)
            {
                sInputParas.Add(((ListItem)comboBox_product_id2.SelectedItem).ID);           //产品编码2
            }
            else
            {
                sInputParas.Add(string.Empty);           //产品编码2
            }
            
            FuncTagInfo funcTagInfo = new FuncTagInfo();
            funcTagInfo.FuncName = "CreateWO";
            funcTagInfo.UserName = _user;

            funcTagInfo.InputParams = sInputParas.ToArray() ;
            WcfRequests.SendGeneralRequest(onCreateWOCompleted, funcTagInfo);

        }


        private void button_cancel_Click(object sender, EventArgs e)
        {

        }

        #region wcf received
        private void onCreateWOCompleted(CallBackData wipReceiveData)
        {
            if (wipReceiveData.bIsOK)
            {
                toolStripStatusLabelRight.Text = "成功插入一条新数据";
                toolStripStatusLabelRight.BackColor = Color.Green;
                changeColorTimer.Start();
                BasicClass.resetControlState(groupBox_woinfo);
                //resetControlState();
            }
            else
            {
                MessageBox.Show(wipReceiveData.Result);
            }
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
                    comboBox_workshop.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.WO_type)
                {
                    comboBox_wo_type.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Cristal_Type)
                {
                    comboBox_cristal_type.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Cell_Size)
                {
                    comboBox_cell_size.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Cell_Consumption)
                {
                    comboBox_cell_consumption.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Cell_Type)
                {
                    comboBox_cell_type.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Workflow)
                {
                    comboBox_producing_flow.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Module_color)
                {
                    cbx_module_color.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Module_type)
                {
                    cbx_module_type.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.PID_type)
                {
                    cbx_pid_type.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Product_name1)
                {
                    comboBox_product_id1.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Product_name2)
                {
                    comboBox_product_id2.Items.Add(new ListItem(sourceName, displayName));
                }
            }

        }



        /// <summary>
        /// 获取到当前用户
        /// </summary>
        /// <param name="receiveData"></param>
        private void onCurrentUserReceive(CallBackData receiveData)
        {
            _user = receiveData.Result;
        }
        #endregion


        #region useful functions
        /// <summary>
        /// 重置状态栏颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void changeColorTimer_Tick(object sender, EventArgs e)
        {
            changeColorTimer.Stop();
            toolStripStatusLabelRight.Text = "";
            toolStripStatusLabelRight.BackColor = Color.Transparent;
        }


        /// <summary>
        /// 清空所有文本框的内容
        /// </summary>
        private void resetControlState()
        {
            foreach (Control item in groupBox_woinfo.Controls)
            {
                if (item is TextBox)
                {
                    if (!((TextBox)item).ReadOnly)
                    {
                        ((TextBox)item).Text = "";
                    }

                }
            }

            //machine_id.Focus();
        }

        //void MachineMaintain_SizeChanged(object sender, EventArgs e)
        //{
        //    asc.controllInitializeSize(this);
        //}
        #endregion

        
        
        
    }
}
