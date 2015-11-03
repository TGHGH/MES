using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.BasicClassLib
{
    public class ComoboxItemName
    {
        #region common
        string workshop;            //车间
        string factory;             //厂区
        string dept;                //部门
        #endregion

        #region wo 创建工单
        string cell_size;           //电池规格
        string cell_type;           //片源类型
        string cell_consumption;    //电池消耗数
        string wo_type;             //工单类型
        string cristal_type;        //晶体类型
        string workflow;            //制造流程
        string module_type;         //组件类型
        string pid_type;            //pid类型
        string module_color;        //组件颜色
        string product_name1;       //产品编码1
        string product_name2;       //产品编码2
        #endregion

        #region cell distribute 分选
        string cell_comp;           //电池厂商
        string cell_color;          //电池片颜色
        #endregion

        #region cell binding 焊接
        string weldEqpType;             //焊接机台类型
        string hulian_comp;         //互联条厂商
        string huiliu_comp;         //汇流条厂商
        #endregion

        #region layup 叠层
        string layUpEqpType;            //叠层机台类型
        string tpt_desc;            //背板规格
        string tpt_comp;            //背板厂商
        string glass_desc;          //玻璃规格
        string glass_comp;          //玻璃厂商
        string eva_desc;            //eva规格
        string eva_comp;            //eva厂商
        #endregion

        #region merge 层压
        string cengyaEqpType;           //层压机台类型
        #endregion

        #region frame 装框
        string frameEqpType;            //装框机台类型
        string frame_desc;          //边框规格   
        string frame_comp;          //边框厂商
        string jbox_desc;           //接线盒规格
        string jbox_comp;           //接线盒厂商
        string glue_desc;           //线盒胶规格
        string glue_comp;           //线盒胶厂商
        string frame_glue_comp;     //型材胶厂商
        string frame_glue_desc;     //型材胶规格

        #endregion

        #region 清洗
        string cleanEqp;            //清洗机台
        #endregion

        #region pack 包装
        string powerTemplate;               //功率模板
        string packCount;                   //包装数量
        #endregion

        public ComoboxItemName()
        {
            #region common
            Factory = "";
            Workshop = "workshop";
            Dept = "dept";
            #endregion

            #region 工单
            Cristal_Type = "cristal_type";
            Workflow = "workflow";
            WO_type = "wo_type";
            Module_color = "module_color";
            PID_type = "pid_type";
            Module_type = "module_type";
            Cell_Consumption = "cell_consumption";
            Product_name1 = "product_name1";
            Product_name2 = "product_name2";
            #endregion

            #region 分选
            Cell_Size = "cell_size";
            Cell_Type = "cell_type";
            Cell_comp = "suppliers_cell";
            Cell_color = "cell_color";
            #endregion

            #region 焊接
            WeldEqpType = "HJ";
            Huiliu_Comp = "suppliers_huiliu";
            Hulian_Comp = "suppliers_hulian";
            #endregion

            #region 叠层
            LayUpEqpType = "DC";
            Glass_Desc = "spec_glass";
            Glass_Comp = "suppliers_glass";
            EVA_desc = "spec_eva";
            EVA_comp = "suppliers_eva";
            TPT_Desc = "spec_tpt";
            TPT_Comp = "suppliers_tpt";
            #endregion

            #region 层压
            CengyaEqpType = "CY";
            #endregion

            #region 装框
            FrameEqpType = "ZK";
            Frame_Desc = "spec_frame";
            Frame_Comp = "suppliers_frame";
            Jbox_Desc = "spec_jbox";
            Jbox_Comp = "suppliers_jbox";
            Glue_Desc = "spec_glue";
            Glue_Comp = "suppliers_glue";
            Frame_glue_comp = "spec_frame_glue";
            Frame_glue_desc = "suppliers_frame_glue";
            #endregion

            #region 清洗
            CleanEqp = "QX";
            #endregion

            #region 包装
            PowerTemplate = "PowerTemplate";
            PackCount = "PackCount";
            #endregion
        }

        #region 属性
        public string CleanEqp {
            get { return cleanEqp; }
            set { cleanEqp = value; }
        }

        public string Frame_glue_comp {
            get { return frame_glue_comp; }
            set { frame_glue_comp = value; }
        }

        public string Frame_glue_desc {
            get { return frame_glue_desc; }
            set { frame_glue_desc = value; }
        }
        
        public string PackCount {
            get { return packCount; }
            set { packCount = value; }
        }

        public string PowerTemplate{
            get { return powerTemplate; }
            set { powerTemplate = value; }
        }
        
        public string Cell_color {
            get { return cell_color; }
            set { cell_color = value; }
        }
        
        public string Cell_comp
        {
            get { return cell_comp; }
            set { cell_comp = value; }
        }

        public string Product_name1
        {
            get { return product_name1; }
            set { product_name1 = value; }
        }

        public string Product_name2
        {
            get { return product_name2; }
            set { product_name2 = value; }
        }

        public string Module_color
        {
            get { return module_color; }
            set { module_color = value; }
        }

        public string PID_type
        {
            get { return pid_type; }
            set { pid_type = value; }
        }

        public string Module_type
        {
            get { return module_type; }
            set { module_type = value; }
        }

        public string Workshop
        {
            get
            {
                return workshop;
            }
            set
            {
                workshop = value;
            }
        }

        public string Factory
        {
            get
            {
                return factory;
            }
            set
            {
                factory = value;
            }
        }

        public string Frame_Desc
        {
            get
            {
                return frame_desc;
            }
            set
            {
                frame_desc = value;
            }
        }

        public string Frame_Comp
        {
            get
            {
                return frame_comp;
            }
            set
            {
                frame_comp = value;
            }
        }

        public string TPT_Desc
        {
            get
            {
                return tpt_desc;
            }
            set
            {
                tpt_desc = value;
            }

        }

        public string TPT_Comp
        {
            get
            {
                return tpt_comp;
            }
            set
            {
                tpt_comp = value;
            }
        }

        public string Jbox_Desc
        {
            get
            {
                return jbox_desc;
            }

            set
            {
                jbox_desc = value;
            }
        }

        public string Jbox_Comp
        {
            get
            {
                return jbox_comp;
            }
            set
            {
                jbox_comp = value;
            }
        }

        public string Glue_Desc
        {
            get
            {
                return glue_desc;
            }
            set
            {
                glue_desc = value;
            }
        }

        public string Glue_Comp
        {
            get
            {
                return glue_comp;
            }
            set
            {
                glue_comp = value;
            }
        }

        public string Cell_Size
        {
            get
            {
                return cell_size;
            }
            set
            {
                cell_size = value;
            }
        }

        public string Cell_Type
        {
            get
            {
                return cell_type;
            }
            set
            {
                cell_type = value;
            }
        }

        public string Glass_Desc
        {
            get
            {
                return glass_desc;
            }
            set
            {
                glass_desc = value;
            }
        }

        public string Glass_Comp
        {
            get
            {
                return glass_comp;
            }
            set
            {
                glass_comp = value;
            }
        }

        public string Cell_Consumption
        {
            get
            {
                return cell_consumption;
            }
            set
            {
                cell_consumption = value;
            }
        }

        public string Huiliu_Comp
        {
            get
            {
                return huiliu_comp;
            }
            set
            {
                huiliu_comp = value;
            }
        }

        public string WO_type
        {
            get
            {
                return wo_type;
            }
            set
            {
                wo_type = value;
            }
        }

        public string Hulian_Comp
        {
            get
            {
                return hulian_comp;
            }
            set
            {
                hulian_comp = value;
            }
        }

        public string EVA_desc
        {
            get
            {
                return eva_desc;
            }
            set
            {
                eva_desc = value;
            }

        }

        public string EVA_comp
        {
            get
            {
                return eva_comp;
            }
            set
            {
                eva_comp = value;
            }
        }

        public string Cristal_Type
        {
            get
            {
                return cristal_type;
            }
            set
            {
                cristal_type = value;
            }
        }

        public string Workflow
        {
            get
            {
                return workflow;
            }
            set
            {
                workflow = value;
            }
        }

        public string Dept
        {
            get
            {
                return dept;
            }
            set
            {
                dept = value;
            }
        }

        public string WeldEqpType
        {
            get { return weldEqpType; }
            set { weldEqpType = value; }
        }

        public string LayUpEqpType
        {
            get { return layUpEqpType; }
            set { layUpEqpType = value; }
        }

        public string FrameEqpType
        {
            get { return frameEqpType; }
            set { frameEqpType = value; }
        }

        public string CengyaEqpType
        {
            get { return cengyaEqpType; }
            set { cengyaEqpType = value; }
        }
        #endregion
    }
   
}
