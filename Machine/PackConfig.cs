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

namespace SysConfig
{
    public partial class PackConfig : Form
    {
        #region local variables
        string _user = string.Empty;//当前用户
        FuncTagInfo functionTag = null;
        Dictionary<string, List<string>> _dicPowerGrade =null;
        Dictionary<string, List<string>> _dicPowerGradeFinal = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> _dicImpGradeFinal = new Dictionary<string, List<string>>();
        //List<string> lstPowerGrade=new List<string>();
        bool _bPowerGradeDoubleClick = false;
        //List<string> lstUpIMP = new List<string>();
        #endregion

        public PackConfig()
        {
            InitializeComponent();
        }

        private void PackConfig_Load(object sender, EventArgs e)
        {

            #region 获取当前主机的登录用户
            functionTag = new FuncTagInfo();
            functionTag.FuncName = "getCurrentUser";
            functionTag.ComputerName = Dns.GetHostName(); ;

            WcfRequests.SendGeneralRequest(onCurrentUserReceive, functionTag);
            #endregion

            #region 初始化comobox的下拉项
            List<string> inParameters = new List<string>();
            //inParameters.Add("equipment");
            inParameters.Add(BasicClass.comoboxItemName.Workshop);
            inParameters.Add(BasicClass.comoboxItemName.PowerTemplate);
            inParameters.Add(BasicClass.comoboxItemName.PackCount);
            
            functionTag = new FuncTagInfo();
            functionTag.FuncName = "GetComoboxItems";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onReceiveComoboxItems, functionTag);
            #endregion

            #region 功率档位设定datagridView
            //dgv_power_set.AllowUserToAddRows = false;
            dgv_power_set.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_power_set.Font = new System.Drawing.Font("宋体", 12, FontStyle.Regular);
            //dgv_power_set.ReadOnly = true;
            dgv_power_set.Columns.Add("powerGrade", "等级");
            dgv_power_set.Columns.Add("minPower", "瓦数起始");
            dgv_power_set.Columns.Add("maxPower", "瓦数结束");
            #endregion

            #region 电流档位设定datagridView
            dgv_imp_set.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv_imp_set.Font = new System.Drawing.Font("宋体", 12, FontStyle.Regular);
            dgv_imp_set.Columns.Add("powerGrade", "功率档位");
            dgv_imp_set.Columns.Add("ImpGrade", "电流档位");
            dgv_imp_set.Columns.Add("minImp", "imp起始");
            dgv_imp_set.Columns.Add("maxImp", "imp结束");
            #endregion

            #region trigger
            //功率模板选择
            cbx_template.SelectedIndexChanged += new EventHandler(cbx_template_SelectedIndexChanged);
            //双击功率模板
            dgv_power_set.CellDoubleClick += new DataGridViewCellEventHandler(dgv_power_set_CellDoubleClick);

            //修改功率值
            dgv_power_set.CellValueChanged += new DataGridViewCellEventHandler(dgv_power_set_CellValueChanged);
            //删除一条功率档位
            dgv_power_set.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgv_power_set_UserDeletingRow);
            //修改电流值
            dgv_imp_set.CellValueChanged += new DataGridViewCellEventHandler(dgv_imp_set_CellValueChanged);
            //删除电流档位
            dgv_imp_set.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgv_imp_set_UserDeletingRow);
            
            //选择车间
            cbx_workshop.SelectedIndexChanged += new EventHandler(cbx_workshop_SelectedIndexChanged);

            //选择工单
            cbx_wo.SelectedIndexChanged += new EventHandler(cbx_wo_SelectedIndexChanged);
            #endregion

            cbx_wo.Enabled = false;

        }

        /// <summary>
        /// 选择工单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cbx_wo_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dicPowerGrade = null;
            _dicPowerGradeFinal = new Dictionary<string, List<string>>();
            _dicImpGradeFinal = new Dictionary<string, List<string>>();

            dgv_power_set.Rows.Clear();
            dgv_imp_set.Rows.Clear();

            #region 获取工单对应的功率参数信息
            
            #endregion
        }


        #region trigger received
        /// <summary>
        /// 选择车间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cbx_workshop_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbx_workshop.Enabled = false;   
            List<string> inParameters = new List<string>();
            //inParameters.Add("equipment");
            inParameters.Add(((ListItem)cbx_workshop.SelectedItem).ID);

            functionTag = new FuncTagInfo();
            functionTag.FuncName = "GetWorkOrder";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onGetWorkOrderReceived, functionTag);
        }
        
        
        /// <summary>
        /// 删除电流档位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_imp_set_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string powerGrade = e.Row.Cells[0].Value.ToString();
            string impGrade = e.Row.Cells[3].Value.ToString();
            _dicImpGradeFinal[powerGrade].Remove(impGrade);

            changeBackColor(powerGrade);
        }

        /// <summary>
        /// 修改电流档位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_imp_set_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_bPowerGradeDoubleClick)
            {//双击不触发
                return;
            }

            const int iUpImpColumn = 3;
            //=>只取最大电流
            if (e.ColumnIndex == iUpImpColumn)
            {
                string powerGrade = ((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString();
                string upImp = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                
                if (!_dicImpGradeFinal.Keys.Contains(powerGrade))
                {
                    _dicImpGradeFinal.Add(powerGrade, new List<string>());
                    
                }

                if (e.RowIndex<_dicImpGradeFinal[powerGrade].Count)
                {//修改
                    _dicImpGradeFinal[powerGrade][e.RowIndex] = upImp;
                }
                else
                {//添加
                    _dicImpGradeFinal[powerGrade].Add(upImp);
                }

                changeBackColor(powerGrade);
            }
        }

        /// <summary>
        /// 删除一条功率档位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_power_set_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string powerGrade = e.Row.Cells[0].Value.ToString();
            _dicPowerGradeFinal.Remove(powerGrade);
            _dicImpGradeFinal.Remove(powerGrade);
        }

        /// <summary>
        /// 修改功率档位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_power_set_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            const int iPowerColumn = 0;
            if (e.ColumnIndex == iPowerColumn)
            {

                string powerGrade=((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (_dicPowerGradeFinal.Keys.Contains(powerGrade))
                {
                    MessageBox.Show("功率档位不能重复！");
                    dgv_power_set.Rows.RemoveAt(e.RowIndex);
                    return;
                }

                _dicPowerGradeFinal.Add(powerGrade, new List<string>());
                changeBackColor(powerGrade);
            }
                    
            
        }

        /// <summary>
        /// 双击功率档位事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dgv_power_set_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _bPowerGradeDoubleClick = true;
            dgv_imp_set.Rows.Clear();

            int index = dgv_power_set.CurrentRow.Index;    //取得选中行的索引
            string powerGrade = dgv_power_set.Rows[index].Cells[0].Value.ToString();   //获取单元格的值  

            if (!_dicImpGradeFinal.ContainsKey(powerGrade))
            {
                return;
            }
            if (_dicImpGradeFinal[powerGrade].Count<2)
            {
                 return;
            }

            //如果配置了电流档，则显示
            //int rowIdx = 0;
            string preUpImp = string.Empty;
            string preLowImp = string.Empty;
            string curUpImp = string.Empty;
            string curLowImp = string.Empty;
            bool isFstRow = true;
            bool isLstRow = false;

            //const int iImpGradeStartIdx = 2;
            for (int i = 0; i < _dicImpGradeFinal[powerGrade].Count; i++)
            {
                dgv_imp_set.Rows.Add();

                if (i + 1 == _dicImpGradeFinal[powerGrade].Count)
                {
                    isLstRow = true;
                }
                
                if (isFstRow)
                {
                    curLowImp = "0";
                    isFstRow = false;
                }
                else
                {
                    curLowImp = preUpImp;
                }

                //if (isLstRow)
                //{
                //    curUpImp = "100";
                //}
                //else
                //{
                    curUpImp = _dicImpGradeFinal[powerGrade][i];
                //}

                //int rowIdx = i - iImpGradeStartIdx;
                dgv_imp_set.Rows[i].Cells[0].Value = powerGrade;
                dgv_imp_set.Rows[i].Cells[1].Value = "I" + (i + 1).ToString();
                dgv_imp_set.Rows[i].Cells[2].Value = curLowImp;
                dgv_imp_set.Rows[i].Cells[3].Value = curUpImp;

                preUpImp = curUpImp;
            }

            _bPowerGradeDoubleClick = false;
        }
        
        /// <summary>
        /// 选择功率模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cbx_template_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> inParameters = new List<string>();
            inParameters.Add(((ListItem)cbx_template.SelectedItem).ID);

            functionTag = new FuncTagInfo();
            functionTag.FuncName = "GetPowerTemplate";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onGetPowerTemplateReceived, functionTag);
        }
        
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ok_Click(object sender, EventArgs e)
        {
            #region check
            if (cbx_wo.Text==string.Empty||
                cbx_pack_count.Text==string.Empty)
            {
                MessageBox.Show("必选项不能为空！");
                return;
            }
            #endregion

            
            List<string> inParameters = new List<string>();
            inParameters.Add(cbx_wo.Text);                  //工单
            inParameters.Add(cbx_pack_count.Text);          //包装数量
            inParameters.Add(tbx_sale_order.Text);          //订单

            #region 获取功率配置参数
            foreach (var item in dgv_power_set.Rows)
            {
                if (((DataGridViewRow)item).Cells[0].Value==null)
                {
                    continue;
                }
                string powerGrade = ((DataGridViewRow)item).Cells[0].Value.ToString();
                string upPower = ((DataGridViewRow)item).Cells[1].Value.ToString();
                string lowPower = ((DataGridViewRow)item).Cells[2].Value.ToString();

                _dicPowerGradeFinal[powerGrade].Clear();
                _dicPowerGradeFinal[powerGrade].Add(powerGrade);
                _dicPowerGradeFinal[powerGrade].Add(lowPower);
                _dicPowerGradeFinal[powerGrade].Add(upPower);

                if (_dicImpGradeFinal.Keys.Contains(powerGrade))
                {
                    if (_dicImpGradeFinal[powerGrade].Count>1)
                    {
                        _dicPowerGradeFinal[powerGrade].AddRange(_dicImpGradeFinal[powerGrade]);
                    }
                }
            }

            //inParameters.Add(((ListItem)cbx_template.SelectedItem).ID);

            foreach (var item in _dicPowerGradeFinal.Values)
            {
                
                string PowerInfo=string.Empty;
                int lstCount=((List<string>)item).Count;

                if (lstCount<1)
                {
                    continue;
                }

                for (int i = 0; i < lstCount; i++)
                {
                    PowerInfo = PowerInfo + ((List<string>)item)[i] + "|";
                }
                PowerInfo = PowerInfo.Substring(0, PowerInfo.Length-1);
                inParameters.Add(PowerInfo);

                //string PowerInfo=
            }
            #endregion

            functionTag = new FuncTagInfo();
            functionTag.FuncName = "SetPowerGrade";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onSetPowerGradeReceived, functionTag);
        }
        #endregion
        


        #region wcf received
        /// <summary>
        /// 获取车间工单
        /// </summary>
        /// <param name="receiveData"></param>
        private void onGetWorkOrderReceived(CallBackData receiveData)
        {
            if (!receiveData.bIsOK)
            {
                MessageBox.Show(receiveData.Result);
                cbx_workshop.Enabled = true;   
                return;
            }

            string[] woItem = receiveData.Result.Split('|');

            cbx_wo.Items.AddRange(woItem);
            cbx_wo.Enabled = true;
        
        }
        
        /// <summary>
        /// 确定按钮返回
        /// </summary>
        /// <param name="receiveData"></param>
        private void onSetPowerGradeReceived(CallBackData receiveData)
        {
            _dicPowerGrade = null;
            _dicPowerGradeFinal = new Dictionary<string, List<string>>();
            _dicImpGradeFinal = new Dictionary<string, List<string>>();

            dgv_power_set.Rows.Clear();
            dgv_imp_set.Rows.Clear();
        }
        
        
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
                else if (ComoboxName == BasicClass.comoboxItemName.PowerTemplate)
                {
                    cbx_template.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.PackCount)
                {
                    cbx_pack_count.Items.Add(new ListItem(sourceName, displayName));
                }
            }
        }

        /// <summary>
        /// 获取功率模板
        /// </summary>
        private void onGetPowerTemplateReceived(CallBackData receiveData)
        {
            _dicPowerGradeFinal = new Dictionary<string, List<string>>();   //清空缓存
            _dicImpGradeFinal = new Dictionary<string, List<string>>();     //清空缓存

            TextReader oTR = new StringReader(receiveData.Result);
            XElement oResult = XElement.Load(oTR);
            oTR.Close();

            _dicPowerGrade = new Dictionary<string, List<string>>();

            IEnumerable<XElement> cReports =
                (from xNode in oResult.Descendants("PowerItem")
                 select xNode);
            foreach (XElement oModuleNode in cReports)
            {
                string sPowerGrade = ((XElement)oModuleNode).Attribute("POWER_GRADE").Value.ToString();
                string sUpperPower = ((XElement)oModuleNode).Attribute("UPPERPOWER").Value.ToString();
                string sLowerPower = ((XElement)oModuleNode).Attribute("LOWERPOWER").Value.ToString();
                string sUpperImp = ((XElement)oModuleNode).Attribute("UPPERIMP").Value.ToString();

                if (!_dicPowerGrade.Keys.Contains(sPowerGrade))
                {
                    _dicPowerGrade.Add(sPowerGrade, new List<string>());
                }
                _dicPowerGrade[sPowerGrade].Add(sUpperPower);
                _dicPowerGrade[sPowerGrade].Add(sLowerPower);


                if (!_dicImpGradeFinal.Keys.Contains(sPowerGrade))
                {
                    _dicImpGradeFinal.Add(sPowerGrade,new List<string>());
                }
                _dicImpGradeFinal[sPowerGrade].Add(sUpperImp);
            }


            #region 显示模板功率档位
            dgv_power_set.Rows.Clear();
            List<string> powerPara = new List<string>();
            int rowIdx = 0;
            foreach (var item in _dicPowerGrade)
            {
                powerPara = item.Value;

                dgv_power_set.Rows.Add();
                dgv_power_set.Rows[rowIdx].Cells[0].Value = item.Key;
                for (int j = 0; j < 2; j++)
                {
                    dgv_power_set.Rows[rowIdx].Cells[j+1].Value = powerPara[j];
                }

                //如果有电流档，则背景色改为绿色
                if (_dicImpGradeFinal[item.Key].Count>1)
                {
                    dgv_power_set.Rows[rowIdx].DefaultCellStyle.BackColor = Color.Green;
                }

                rowIdx++;
            }
            #endregion
        
        }

        #endregion

        /// <summary>
        /// 改变有电流档位的背景色
        /// </summary>
        /// <param name="powerGrade"></param>
        private void changeBackColor(string powerGrade)
        {
            if (_dicImpGradeFinal.ContainsKey(powerGrade))
            {
               if (_dicImpGradeFinal[powerGrade].Count > 1)
                {
                    for (int i = 0; i < dgv_power_set.Rows.Count; i++)
                    {
                        if (dgv_power_set.Rows[i].Cells[0].Value.ToString()==powerGrade)
                        {
                            dgv_power_set.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                            return;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dgv_power_set.Rows.Count; i++)
                    {
                        if (dgv_power_set.Rows[i].Cells[0].Value.ToString() == powerGrade)
                        {
                            dgv_power_set.Rows[i].DefaultCellStyle.BackColor = Color.White;
                            return;
                        }
                    }
                } 
            }
            else
            {
                for (int i = 0; i < dgv_power_set.Rows.Count; i++)
                {
                    if (dgv_power_set.Rows[i].Cells[0].Value.ToString() == powerGrade)
                    {
                        dgv_power_set.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        return;
                    }
                }
            }
            
        }
    }
}
