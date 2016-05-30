using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Migrations;
using System.Windows;
using LeagueTracker.Data;
using MoonliteSoftware.Core.Extensions;
using MoonliteSoftware.Core.WPF;

namespace LeagueTracker.Controls
{
    /// <summary>
    /// Interaction logic for ucGameEditor.xaml
    /// </summary>
    public partial class ucGameEditor : ucBaseDialogueControl
    {
        private readonly LeagueTrackerDb _db;

        public ucGameEditor(Game game, LeagueTrackerDb db)
        {
            _db = db;
            InitializeComponent();
            DataContext = game;
        }

        public override void OnCancelClicked()
        {
            
        }

        public override void OnSaveClicked()
        {
            _db.SaveChanges();
        }
    }
}
