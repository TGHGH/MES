using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataContractAssembly
{
    [DataContract]
    public class CallBackData
    {
        [DataMember]
        public string UUID { get; set; }
        [DataMember]
        public string Result { get; set; }
        [DataMember]
        public bool bIsOK { get; set; }
        [DataMember]
        public List<RowData> RowDatas { get; set; }

    }

    //[DataContract]
    public class RowData
    {
        public List<string> rowData;

    }
}
