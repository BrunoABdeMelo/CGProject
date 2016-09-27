﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApplication.GameServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Player", Namespace="http://schemas.datacontract.org/2004/07/GameService.Enums")]
    public enum Player : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        One = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Two = 2,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Game", Namespace="http://schemas.datacontract.org/2004/07/GameService.Models")]
    [System.SerializableAttribute()]
    public partial class Game : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int[] BoardField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ConsoleApplication.GameServiceReference.Player PlayerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ConsoleApplication.GameServiceReference.GameState StateField;
        
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
        public int[] Board {
            get {
                return this.BoardField;
            }
            set {
                if ((object.ReferenceEquals(this.BoardField, value) != true)) {
                    this.BoardField = value;
                    this.RaisePropertyChanged("Board");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ConsoleApplication.GameServiceReference.Player Player {
            get {
                return this.PlayerField;
            }
            set {
                if ((this.PlayerField.Equals(value) != true)) {
                    this.PlayerField = value;
                    this.RaisePropertyChanged("Player");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ConsoleApplication.GameServiceReference.GameState State {
            get {
                return this.StateField;
            }
            set {
                if ((this.StateField.Equals(value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GameState", Namespace="http://schemas.datacontract.org/2004/07/GameService.Enums")]
    public enum GameState : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        WaitingPlayer = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Running = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Finished = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GameServiceReference.IGameService")]
    public interface IGameService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/EnterGame", ReplyAction="http://tempuri.org/IGameService/EnterGameResponse")]
        ConsoleApplication.GameServiceReference.Player EnterGame();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/EnterGame", ReplyAction="http://tempuri.org/IGameService/EnterGameResponse")]
        System.Threading.Tasks.Task<ConsoleApplication.GameServiceReference.Player> EnterGameAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetGameData", ReplyAction="http://tempuri.org/IGameService/GetGameDataResponse")]
        ConsoleApplication.GameServiceReference.Game GetGameData();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetGameData", ReplyAction="http://tempuri.org/IGameService/GetGameDataResponse")]
        System.Threading.Tasks.Task<ConsoleApplication.GameServiceReference.Game> GetGameDataAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/Play", ReplyAction="http://tempuri.org/IGameService/PlayResponse")]
        void Play(ConsoleApplication.GameServiceReference.Player player, int position);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/Play", ReplyAction="http://tempuri.org/IGameService/PlayResponse")]
        System.Threading.Tasks.Task PlayAsync(ConsoleApplication.GameServiceReference.Player player, int position);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameServiceChannel : ConsoleApplication.GameServiceReference.IGameService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GameServiceClient : System.ServiceModel.ClientBase<ConsoleApplication.GameServiceReference.IGameService>, ConsoleApplication.GameServiceReference.IGameService {
        
        public GameServiceClient() {
        }
        
        public GameServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GameServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GameServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GameServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ConsoleApplication.GameServiceReference.Player EnterGame() {
            return base.Channel.EnterGame();
        }
        
        public System.Threading.Tasks.Task<ConsoleApplication.GameServiceReference.Player> EnterGameAsync() {
            return base.Channel.EnterGameAsync();
        }
        
        public ConsoleApplication.GameServiceReference.Game GetGameData() {
            return base.Channel.GetGameData();
        }
        
        public System.Threading.Tasks.Task<ConsoleApplication.GameServiceReference.Game> GetGameDataAsync() {
            return base.Channel.GetGameDataAsync();
        }
        
        public void Play(ConsoleApplication.GameServiceReference.Player player, int position) {
            base.Channel.Play(player, position);
        }
        
        public System.Threading.Tasks.Task PlayAsync(ConsoleApplication.GameServiceReference.Player player, int position) {
            return base.Channel.PlayAsync(player, position);
        }
    }
}
