using System;
using System.Collections.Generic;
using LeagueTracker.Data;
using MoonliteSoftware.Core.WPF;

namespace LeagueTracker.Controls
{
    /// <summary>
    /// Interaction logic for ucGameSelector.xaml
    /// </summary>
    public partial class ucGameSelector : ucBaseDialogueControl
    {
        public ucGameSelector(List<Game> games)
        {
            InitializeComponent();
            comboBox.ItemsSource = games;
            comboBox.SelectedValue = Settings.Default.CurrentGameId;
        }

        public override void OnCancelClicked()
        {
            
        }

        public override void OnSaveClicked()
        {
            Settings.Default.CurrentGameId = (Guid) comboBox.SelectedValue;
            Settings.Default.Save();
        }
    }
}
