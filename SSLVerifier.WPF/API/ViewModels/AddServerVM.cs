﻿using System;
using System.Windows;
using System.Windows.Input;
using SSLVerifier.API.MainLogic;
using SysadminsLV.WPF.OfficeTheme.Toolkit.Commands;

namespace SSLVerifier.API.ViewModels {
    class AddServerVM : ViewModelBase {
        String serverName;
        Int32 port;

        public AddServerVM() {
            OkCommand = new RelayCommand(addServer, canAddServer);
        }

        public ICommand OkCommand { get; }

        Boolean checkPort() {
            return port > 0 && port < 65535;
        }

        public String ServerName {
            get => serverName;
            set {
                serverName = value;
                OnPropertyChanged(nameof(ServerName));
            }
        }
        public Int32 Port {
            get => port;
            set {
                port = value;
                OnPropertyChanged(nameof(Port));
            }
        }
        void addServer(Object obj) {
            if (String.IsNullOrEmpty(ServerName)) {
                Tools.MsgBox("Error", "The string must not be empty.");
                return;
            }
            MainWindowVM mwvm = (MainWindowVM)Application.Current.MainWindow.DataContext;
            if (mwvm.AddServerItem(ServerName.Replace("https://", null), Port)) { ServerName = String.Empty; }
        }
        Boolean canAddServer(Object obj) {
            return !String.IsNullOrEmpty(ServerName) || !String.IsNullOrWhiteSpace(ServerName) && checkPort();
        }
    }
}