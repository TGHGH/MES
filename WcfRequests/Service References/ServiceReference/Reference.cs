﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfRequestNS.ServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FuncTagInfo", Namespace="http://schemas.datacontract.org/2004/07/DataContractAssembly")]
    [System.SerializableAttribute()]
    public partial class FuncTagInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ComputerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ConditionIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DataIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DisplayPathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DisplayTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EngDisplayNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExcelTempLateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FuncNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] InputParamsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IsReportField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LanguageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UUIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WorkshopField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ComputerName {
            get {
                return this.ComputerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ComputerNameField, value) != true)) {
                    this.ComputerNameField = value;
                    this.RaisePropertyChanged("ComputerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ConditionId {
            get {
                return this.ConditionIdField;
            }
            set {
                if ((object.ReferenceEquals(this.ConditionIdField, value) != true)) {
                    this.ConditionIdField = value;
                    this.RaisePropertyChanged("ConditionId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DataId {
            get {
                return this.DataIdField;
            }
            set {
                if ((object.ReferenceEquals(this.DataIdField, value) != true)) {
                    this.DataIdField = value;
                    this.RaisePropertyChanged("DataId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DisplayPath {
            get {
                return this.DisplayPathField;
            }
            set {
                if ((object.ReferenceEquals(this.DisplayPathField, value) != true)) {
                    this.DisplayPathField = value;
                    this.RaisePropertyChanged("DisplayPath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DisplayType {
            get {
                return this.DisplayTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.DisplayTypeField, value) != true)) {
                    this.DisplayTypeField = value;
                    this.RaisePropertyChanged("DisplayType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EngDisplayName {
            get {
                return this.EngDisplayNameField;
            }
            set {
                if ((object.ReferenceEquals(this.EngDisplayNameField, value) != true)) {
                    this.EngDisplayNameField = value;
                    this.RaisePropertyChanged("EngDisplayName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ExcelTempLate {
            get {
                return this.ExcelTempLateField;
            }
            set {
                if ((object.ReferenceEquals(this.ExcelTempLateField, value) != true)) {
                    this.ExcelTempLateField = value;
                    this.RaisePropertyChanged("ExcelTempLate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FuncName {
            get {
                return this.FuncNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FuncNameField, value) != true)) {
                    this.FuncNameField = value;
                    this.RaisePropertyChanged("FuncName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] InputParams {
            get {
                return this.InputParamsField;
            }
            set {
                if ((object.ReferenceEquals(this.InputParamsField, value) != true)) {
                    this.InputParamsField = value;
                    this.RaisePropertyChanged("InputParams");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IsReport {
            get {
                return this.IsReportField;
            }
            set {
                if ((object.ReferenceEquals(this.IsReportField, value) != true)) {
                    this.IsReportField = value;
                    this.RaisePropertyChanged("IsReport");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Language {
            get {
                return this.LanguageField;
            }
            set {
                if ((object.ReferenceEquals(this.LanguageField, value) != true)) {
                    this.LanguageField = value;
                    this.RaisePropertyChanged("Language");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UUID {
            get {
                return this.UUIDField;
            }
            set {
                if ((object.ReferenceEquals(this.UUIDField, value) != true)) {
                    this.UUIDField = value;
                    this.RaisePropertyChanged("UUID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserID {
            get {
                return this.UserIDField;
            }
            set {
                if ((object.ReferenceEquals(this.UserIDField, value) != true)) {
                    this.UserIDField = value;
                    this.RaisePropertyChanged("UserID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Workshop {
            get {
                return this.WorkshopField;
            }
            set {
                if ((object.ReferenceEquals(this.WorkshopField, value) != true)) {
                    this.WorkshopField = value;
                    this.RaisePropertyChanged("Workshop");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CallBackData", Namespace="http://schemas.datacontract.org/2004/07/DataContractAssembly")]
    [System.SerializableAttribute()]
    public partial class CallBackData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ResultField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WcfRequestNS.ServiceReference.RowData[] RowDatasField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UUIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool bIsOKField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Result {
            get {
                return this.ResultField;
            }
            set {
                if ((object.ReferenceEquals(this.ResultField, value) != true)) {
                    this.ResultField = value;
                    this.RaisePropertyChanged("Result");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WcfRequestNS.ServiceReference.RowData[] RowDatas {
            get {
                return this.RowDatasField;
            }
            set {
                if ((object.ReferenceEquals(this.RowDatasField, value) != true)) {
                    this.RowDatasField = value;
                    this.RaisePropertyChanged("RowDatas");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UUID {
            get {
                return this.UUIDField;
            }
            set {
                if ((object.ReferenceEquals(this.UUIDField, value) != true)) {
                    this.UUIDField = value;
                    this.RaisePropertyChanged("UUID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool bIsOK {
            get {
                return this.bIsOKField;
            }
            set {
                if ((this.bIsOKField.Equals(value) != true)) {
                    this.bIsOKField = value;
                    this.RaisePropertyChanged("bIsOK");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RowData", Namespace="http://schemas.datacontract.org/2004/07/DataContractAssembly")]
    [System.SerializableAttribute()]
    public partial class RowData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] rowDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] rowData {
            get {
                return this.rowDataField;
            }
            set {
                if ((object.ReferenceEquals(this.rowDataField, value) != true)) {
                    this.rowDataField = value;
                    this.RaisePropertyChanged("rowData");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="GeneralWcfService", ConfigurationName="ServiceReference.IGeneralService")]
    public interface IGeneralService {
        
        [System.ServiceModel.OperationContractAttribute(Action="GeneralWcfService/IGeneralService/Request", ReplyAction="GeneralWcfService/IGeneralService/RequestResponse")]
        WcfRequestNS.ServiceReference.CallBackData Request(WcfRequestNS.ServiceReference.FuncTagInfo _Request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGeneralServiceChannel : WcfRequestNS.ServiceReference.IGeneralService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GeneralServiceClient : System.ServiceModel.ClientBase<WcfRequestNS.ServiceReference.IGeneralService>, WcfRequestNS.ServiceReference.IGeneralService {
        
        public GeneralServiceClient() {
        }
        
        public GeneralServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GeneralServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GeneralServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GeneralServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WcfRequestNS.ServiceReference.CallBackData Request(WcfRequestNS.ServiceReference.FuncTagInfo _Request) {
            return base.Channel.Request(_Request);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IWipService")]
    public interface IWipService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWipService/Request", ReplyAction="http://tempuri.org/IWipService/RequestResponse")]
        WcfRequestNS.ServiceReference.CallBackData Request(WcfRequestNS.ServiceReference.FuncTagInfo _Request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWipServiceChannel : WcfRequestNS.ServiceReference.IWipService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WipServiceClient : System.ServiceModel.ClientBase<WcfRequestNS.ServiceReference.IWipService>, WcfRequestNS.ServiceReference.IWipService {
        
        public WipServiceClient() {
        }
        
        public WipServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WipServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WipServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WipServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WcfRequestNS.ServiceReference.CallBackData Request(WcfRequestNS.ServiceReference.FuncTagInfo _Request) {
            return base.Channel.Request(_Request);
        }
    }
}
