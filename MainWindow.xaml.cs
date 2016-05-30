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
using LeagueTracker.Controls;
using LeagueTracker.Data;
using MoonliteSoftware.Core.Extensions;
using MoonliteSoftware.Core.WPF;

namespace LeagueTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (var db = new LeagueTrackerDb())
            {
                if (!db.Games.Any())
                    CreateNewGame(db, true);
                else
                    Refresh(db);
            }
        }

        private void SelectGame_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new LeagueTrackerDb())
            {
                var selector = new ucGameSelector(db.Games.ToList());
                var window = new GenericDialogueWindow("Select Game", selector);
                window.Height = 200;
                window.Width = 300;
                window.OkButtonText = "Ok";
                var results = window.ShowDialog();
                if (results.HasValue && results.Value)
                    Refresh(db);
            }
        }

        private void Refresh(LeagueTrackerDb db)
        {
            DataContext = db.GetGame(Settings.Default.CurrentGameId);
        }

        private void ExitMenu_OnClick(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void NewGame_OnClick(object sender, RoutedEventArgs e)
        {
            CreateNewGame(null);
        }

        private void CreateNewGame(LeagueTrackerDb db, bool forced = false)
        {
            using (db = db ?? new LeagueTrackerDb())
            {
                var game = new Game();

                db.Games.Add(game);

                var ucGameEditor = new ucGameEditor(game, db);
                var newGameWindow = new GenericDialogueWindow("New Game", ucGameEditor)
                {
                    Height = 200,
                    Width = 300
                };

                if (forced) newGameWindow.Force();

                var result = newGameWindow.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    DataContext = game;
                    Settings.Default.CurrentGameId = game.Id;
                    Settings.Default.Save();
                }
            }
        }

        private void EditGame_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new LeagueTrackerDb())
            {
                var game = (Game)DataContext;
                var newGameWindow = new GenericDialogueWindow($"{game.Name}", new ucGameEditor(game, db))
                {
                    Height = 200,
                    Width = 300
                };
                db.UpdateEntity(game);
                newGameWindow.ShowDialog();
                Refresh(db);
            }
        }
    }
}
