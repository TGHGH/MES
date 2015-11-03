using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DataContractAssembly;

namespace WIPWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWipService
    {
        [OperationContract]
        CallBackData Request(FuncTagInfo _Request);
    }

    //[DataContract]
    //public class WipFuncTagInfo
    //{
    //    //bool boolValue = true;
    //    //string stringValue = "Hello ";
    //    [DataMember]
    //    public string Workshop { get; set; }
    //    //ServiceType serviceType;
    //    [DataMember]
    //    public string DisplayPath { get; set; }
    //    [DataMember]
    //    public string FuncName { get; set; }
    //    [DataMember]
    //    public string EngDisplayName { get; set; }
    //    [DataMember]
    //    public string UUID { get; set; }
    //    [DataMember]
    //    public string Language { get; set; }
    //    //object timeperiod;
    //    [DataMember]
    //    public string UserID { get; set; }
    //    [DataMember]
    //    public string IsReport { get; set; }
    //    [DataMember]
    //    public string DisplayType { get; set; }
    //    [DataMember]
    //    public string ConditionId { get; set; }
    //    [DataMember]
    //    public string DataId { get; set; }
    //    [DataMember]
    //    public string UserName { get; set; }
    //    [DataMember]
    //    public string ExcelTempLate { get; set; }
    //    [DataMember]
    //    public List<string> InputParams { get; set; }
    //    //int accessSequence = 0;
    //    [DataMember]
    //    public string ComputerName { get; set; }
    //}

    //[DataContract]
    //public class WipData
    //{
    //    [DataMember]
    //    public string UUID { get; set; }
    //    [DataMember]
    //    public string Result { get; set; }
    //    [DataMember]
    //    public bool bIsOK { get; set; }
    //    [DataMember]
    //    public List<RowData> RowDatas { get; set; }

    //}
    ////待完善 这里应该需要放到basicclass中去。
    //public class RowData
    //{
    //    public List<string> rowData;
    
    //}
}
