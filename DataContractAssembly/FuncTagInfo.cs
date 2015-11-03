using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataContractAssembly
{
    [DataContract]
    public class FuncTagInfo
    {
        //bool boolValue = true;
        //string stringValue = "Hello ";
        [DataMember]
        public string Workshop { get; set; }
        //ServiceType serviceType;
        [DataMember]
        public string DisplayPath { get; set; }
        [DataMember]
        public string FuncName { get; set; }
        [DataMember]
        public string EngDisplayName { get; set; }
        [DataMember]
        public string UUID { get; set; }
        [DataMember]
        public string Language { get; set; }
        //object timeperiod;
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string IsReport { get; set; }
        [DataMember]
        public string DisplayType { get; set; }
        [DataMember]
        public string ConditionId { get; set; }
        [DataMember]
        public string DataId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string ExcelTempLate { get; set; }
        [DataMember]
        public string ComputerName { get; set; }
        [DataMember]
        public List<string> InputParams { get; set; }
    }
}
