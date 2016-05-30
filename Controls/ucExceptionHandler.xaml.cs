using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MoonliteSoftware.Core.WPF;

namespace LeagueTracker.Controls
{
    /// <summary>
    /// Interaction logic for ucExceptionHandler.xaml
    /// </summary>
    public partial class ucExceptionHandler : ucBaseDialogueControl
    {
        public string ExceptionText { get; private set; }
        public ucExceptionHandler(Exception exception)
        {
            InitializeComponent();
            DataContext = this;
            ExceptionText = exception.ToString();
        }

        public override void OnSaveClicked()
        {
            Clipboard.SetText(ExceptionText);
            MessageBox.Show("Copied to clipboard");
        }

        public override void OnCancelClicked()
        {
            
        }
    }
}
