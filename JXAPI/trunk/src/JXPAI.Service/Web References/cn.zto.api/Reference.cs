﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.18408 版自动生成。
// 
#pragma warning disable 1591

namespace JXPAI.Service.cn.zto.api {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WebServiceSoap", Namespace="http://api.zto.cn/")]
    public partial class WebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SearchOperationCompleted;
        
        private System.Threading.SendOrPostCallback SearchZtoBillOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WebService() {
            this.Url = global::JXPAI.Service.Properties.Settings.Default.JXPAI_Service_cn_zto_api_WebService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event SearchCompletedEventHandler SearchCompleted;
        
        /// <remarks/>
        public event SearchZtoBillCompletedEventHandler SearchZtoBillCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://api.zto.cn/Search", RequestNamespace="http://api.zto.cn/", ResponseNamespace="http://api.zto.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable Search(string Userid, string Pwd, string SrtjobNo) {
            object[] results = this.Invoke("Search", new object[] {
                        Userid,
                        Pwd,
                        SrtjobNo});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void SearchAsync(string Userid, string Pwd, string SrtjobNo) {
            this.SearchAsync(Userid, Pwd, SrtjobNo, null);
        }
        
        /// <remarks/>
        public void SearchAsync(string Userid, string Pwd, string SrtjobNo, object userState) {
            if ((this.SearchOperationCompleted == null)) {
                this.SearchOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSearchOperationCompleted);
            }
            this.InvokeAsync("Search", new object[] {
                        Userid,
                        Pwd,
                        SrtjobNo}, this.SearchOperationCompleted, userState);
        }
        
        private void OnSearchOperationCompleted(object arg) {
            if ((this.SearchCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SearchCompleted(this, new SearchCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://api.zto.cn/SearchZtoBill", RequestNamespace="http://api.zto.cn/", ResponseNamespace="http://api.zto.cn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable SearchZtoBill(string Userid, string Pwd, string SrtjobNo) {
            object[] results = this.Invoke("SearchZtoBill", new object[] {
                        Userid,
                        Pwd,
                        SrtjobNo});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void SearchZtoBillAsync(string Userid, string Pwd, string SrtjobNo) {
            this.SearchZtoBillAsync(Userid, Pwd, SrtjobNo, null);
        }
        
        /// <remarks/>
        public void SearchZtoBillAsync(string Userid, string Pwd, string SrtjobNo, object userState) {
            if ((this.SearchZtoBillOperationCompleted == null)) {
                this.SearchZtoBillOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSearchZtoBillOperationCompleted);
            }
            this.InvokeAsync("SearchZtoBill", new object[] {
                        Userid,
                        Pwd,
                        SrtjobNo}, this.SearchZtoBillOperationCompleted, userState);
        }
        
        private void OnSearchZtoBillOperationCompleted(object arg) {
            if ((this.SearchZtoBillCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SearchZtoBillCompleted(this, new SearchZtoBillCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void SearchCompletedEventHandler(object sender, SearchCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SearchCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SearchCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void SearchZtoBillCompletedEventHandler(object sender, SearchZtoBillCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SearchZtoBillCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SearchZtoBillCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591