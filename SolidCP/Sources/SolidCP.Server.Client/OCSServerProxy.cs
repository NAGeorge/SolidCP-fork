// Copyright (c) 2016, SolidCP
// SolidCP is distributed under the Creative Commons Share-alike license
// 
// SolidCP is a fork of WebsitePanel:
// Copyright (c) 2015, Outercurve Foundation.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//
// - Redistributions of source code must  retain  the  above copyright notice, this
//   list of conditions and the following disclaimer.
//
// - Redistributions in binary form  must  reproduce the  above  copyright  notice,
//   this list of conditions  and  the  following  disclaimer in  the documentation
//   and/or other materials provided with the distribution.
//
// - Neither  the  name  of  the  Outercurve Foundation  nor   the   names  of  its
//   contributors may be used to endorse or  promote  products  derived  from  this
//   software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING,  BUT  NOT  LIMITED TO, THE IMPLIED
// WARRANTIES  OF  MERCHANTABILITY   AND  FITNESS  FOR  A  PARTICULAR  PURPOSE  ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL,  SPECIAL,  EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO,  PROCUREMENT  OF  SUBSTITUTE  GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)  HOWEVER  CAUSED AND ON
// ANY  THEORY  OF  LIABILITY,  WHETHER  IN  CONTRACT,  STRICT  LIABILITY,  OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE)  ARISING  IN  ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by wsdl, Version=2.0.50727.42.
// 
using SolidCP.Providers.HostedSolution;

namespace SolidCP.Providers.OCS {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="OCSServerSoap", Namespace="http://smbsaas/solidcp/server/")]
    public partial class OCSServer : Microsoft.Web.Services3.WebServicesClientProtocol {
        
        public ServiceProviderSettingsSoapHeader ServiceProviderSettingsSoapHeaderValue;
        
        private System.Threading.SendOrPostCallback CreateUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetUserGeneralSettingsOperationCompleted;
        
        private System.Threading.SendOrPostCallback SetUserGeneralSettingsOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback SetUserPrimaryUriOperationCompleted;
        
        /// <remarks/>
        public OCSServer() {
            this.Url = "http://exchange-dev:9003/OCSServer.asmx";
        }
        
        /// <remarks/>
        public event CreateUserCompletedEventHandler CreateUserCompleted;
        
        /// <remarks/>
        public event GetUserGeneralSettingsCompletedEventHandler GetUserGeneralSettingsCompleted;
        
        /// <remarks/>
        public event SetUserGeneralSettingsCompletedEventHandler SetUserGeneralSettingsCompleted;
        
        /// <remarks/>
        public event DeleteUserCompletedEventHandler DeleteUserCompleted;
        
        /// <remarks/>
        public event SetUserPrimaryUriCompletedEventHandler SetUserPrimaryUriCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("ServiceProviderSettingsSoapHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/solidcp/server/CreateUser", RequestNamespace="http://smbsaas/solidcp/server/", ResponseNamespace="http://smbsaas/solidcp/server/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CreateUser(string userUpn, string userDistinguishedName) {
            object[] results = this.Invoke("CreateUser", new object[] {
                        userUpn,
                        userDistinguishedName});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCreateUser(string userUpn, string userDistinguishedName, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("CreateUser", new object[] {
                        userUpn,
                        userDistinguishedName}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndCreateUser(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CreateUserAsync(string userUpn, string userDistinguishedName) {
            this.CreateUserAsync(userUpn, userDistinguishedName, null);
        }
        
        /// <remarks/>
        public void CreateUserAsync(string userUpn, string userDistinguishedName, object userState) {
            if ((this.CreateUserOperationCompleted == null)) {
                this.CreateUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateUserOperationCompleted);
            }
            this.InvokeAsync("CreateUser", new object[] {
                        userUpn,
                        userDistinguishedName}, this.CreateUserOperationCompleted, userState);
        }
        
        private void OnCreateUserOperationCompleted(object arg) {
            if ((this.CreateUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateUserCompleted(this, new CreateUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("ServiceProviderSettingsSoapHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/solidcp/server/GetUserGeneralSettings", RequestNamespace="http://smbsaas/solidcp/server/", ResponseNamespace="http://smbsaas/solidcp/server/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public OCSUser GetUserGeneralSettings(string instanceId) {
            object[] results = this.Invoke("GetUserGeneralSettings", new object[] {
                        instanceId});
            return ((OCSUser)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetUserGeneralSettings(string instanceId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetUserGeneralSettings", new object[] {
                        instanceId}, callback, asyncState);
        }
        
        /// <remarks/>
        public OCSUser EndGetUserGeneralSettings(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((OCSUser)(results[0]));
        }
        
        /// <remarks/>
        public void GetUserGeneralSettingsAsync(string instanceId) {
            this.GetUserGeneralSettingsAsync(instanceId, null);
        }
        
        /// <remarks/>
        public void GetUserGeneralSettingsAsync(string instanceId, object userState) {
            if ((this.GetUserGeneralSettingsOperationCompleted == null)) {
                this.GetUserGeneralSettingsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUserGeneralSettingsOperationCompleted);
            }
            this.InvokeAsync("GetUserGeneralSettings", new object[] {
                        instanceId}, this.GetUserGeneralSettingsOperationCompleted, userState);
        }
        
        private void OnGetUserGeneralSettingsOperationCompleted(object arg) {
            if ((this.GetUserGeneralSettingsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUserGeneralSettingsCompleted(this, new GetUserGeneralSettingsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("ServiceProviderSettingsSoapHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/solidcp/server/SetUserGeneralSettings", RequestNamespace="http://smbsaas/solidcp/server/", ResponseNamespace="http://smbsaas/solidcp/server/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void SetUserGeneralSettings(string instanceId, bool enabledForFederation, bool enabledForPublicIMConectivity, bool archiveInternalCommunications, bool archiveFederatedCommunications, bool enabledForEnhancedPresence) {
            this.Invoke("SetUserGeneralSettings", new object[] {
                        instanceId,
                        enabledForFederation,
                        enabledForPublicIMConectivity,
                        archiveInternalCommunications,
                        archiveFederatedCommunications,
                        enabledForEnhancedPresence});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSetUserGeneralSettings(string instanceId, bool enabledForFederation, bool enabledForPublicIMConectivity, bool archiveInternalCommunications, bool archiveFederatedCommunications, bool enabledForEnhancedPresence, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SetUserGeneralSettings", new object[] {
                        instanceId,
                        enabledForFederation,
                        enabledForPublicIMConectivity,
                        archiveInternalCommunications,
                        archiveFederatedCommunications,
                        enabledForEnhancedPresence}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndSetUserGeneralSettings(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        public void SetUserGeneralSettingsAsync(string instanceId, bool enabledForFederation, bool enabledForPublicIMConectivity, bool archiveInternalCommunications, bool archiveFederatedCommunications, bool enabledForEnhancedPresence) {
            this.SetUserGeneralSettingsAsync(instanceId, enabledForFederation, enabledForPublicIMConectivity, archiveInternalCommunications, archiveFederatedCommunications, enabledForEnhancedPresence, null);
        }
        
        /// <remarks/>
        public void SetUserGeneralSettingsAsync(string instanceId, bool enabledForFederation, bool enabledForPublicIMConectivity, bool archiveInternalCommunications, bool archiveFederatedCommunications, bool enabledForEnhancedPresence, object userState) {
            if ((this.SetUserGeneralSettingsOperationCompleted == null)) {
                this.SetUserGeneralSettingsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSetUserGeneralSettingsOperationCompleted);
            }
            this.InvokeAsync("SetUserGeneralSettings", new object[] {
                        instanceId,
                        enabledForFederation,
                        enabledForPublicIMConectivity,
                        archiveInternalCommunications,
                        archiveFederatedCommunications,
                        enabledForEnhancedPresence}, this.SetUserGeneralSettingsOperationCompleted, userState);
        }
        
        private void OnSetUserGeneralSettingsOperationCompleted(object arg) {
            if ((this.SetUserGeneralSettingsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SetUserGeneralSettingsCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("ServiceProviderSettingsSoapHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/solidcp/server/DeleteUser", RequestNamespace="http://smbsaas/solidcp/server/", ResponseNamespace="http://smbsaas/solidcp/server/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void DeleteUser(string instanceId) {
            this.Invoke("DeleteUser", new object[] {
                        instanceId});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDeleteUser(string instanceId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DeleteUser", new object[] {
                        instanceId}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndDeleteUser(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        public void DeleteUserAsync(string instanceId) {
            this.DeleteUserAsync(instanceId, null);
        }
        
        /// <remarks/>
        public void DeleteUserAsync(string instanceId, object userState) {
            if ((this.DeleteUserOperationCompleted == null)) {
                this.DeleteUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteUserOperationCompleted);
            }
            this.InvokeAsync("DeleteUser", new object[] {
                        instanceId}, this.DeleteUserOperationCompleted, userState);
        }
        
        private void OnDeleteUserOperationCompleted(object arg) {
            if ((this.DeleteUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteUserCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("ServiceProviderSettingsSoapHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smbsaas/solidcp/server/SetUserPrimaryUri", RequestNamespace="http://smbsaas/solidcp/server/", ResponseNamespace="http://smbsaas/solidcp/server/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void SetUserPrimaryUri(string instanceId, string userUpn) {
            this.Invoke("SetUserPrimaryUri", new object[] {
                        instanceId,
                        userUpn});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSetUserPrimaryUri(string instanceId, string userUpn, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SetUserPrimaryUri", new object[] {
                        instanceId,
                        userUpn}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndSetUserPrimaryUri(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        public void SetUserPrimaryUriAsync(string instanceId, string userUpn) {
            this.SetUserPrimaryUriAsync(instanceId, userUpn, null);
        }
        
        /// <remarks/>
        public void SetUserPrimaryUriAsync(string instanceId, string userUpn, object userState) {
            if ((this.SetUserPrimaryUriOperationCompleted == null)) {
                this.SetUserPrimaryUriOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSetUserPrimaryUriOperationCompleted);
            }
            this.InvokeAsync("SetUserPrimaryUri", new object[] {
                        instanceId,
                        userUpn}, this.SetUserPrimaryUriOperationCompleted, userState);
        }
        
        private void OnSetUserPrimaryUriOperationCompleted(object arg) {
            if ((this.SetUserPrimaryUriCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SetUserPrimaryUriCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void CreateUserCompletedEventHandler(object sender, CreateUserCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void GetUserGeneralSettingsCompletedEventHandler(object sender, GetUserGeneralSettingsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUserGeneralSettingsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUserGeneralSettingsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public OCSUser Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((OCSUser)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void SetUserGeneralSettingsCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void DeleteUserCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void SetUserPrimaryUriCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}