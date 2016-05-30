using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LeagueTracker.Controls;
using MoonliteSoftware.Core.WPF;

namespace LeagueTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exceptionHandler = new ucExceptionHandler(e.Exception);
            var window = new GenericDialogueWindow("Exception Occurred", exceptionHandler);
            window.CancelButtonText = "Close";
            window.OkButtonText = "Copy to Clipboard";
            window.ShowDialog();
            e.Handled = true;
        }
    }
}
