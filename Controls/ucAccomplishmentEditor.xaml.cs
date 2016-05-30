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
using LeagueTracker.Data;
using MoonliteSoftware.Core.WPF;

namespace LeagueTracker.Controls
{
    /// <summary>
    /// Interaction logic for ucAccomplishmentEditor.xaml
    /// </summary>
    public partial class ucAccomplishmentEditor : ucBaseDialogueControl
    {
        public ucAccomplishmentEditor(Accomplishment accomplishment)
        {
            InitializeComponent();
            DataContext = accomplishment;
        }

        public override void OnCancelClicked()
        {
            throw new NotImplementedException();
        }

        public override void OnSaveClicked()
        {
            throw new NotImplementedException();
        }
    }
}
