using MoonliteSoftware.Core.Data.Models;
using PropertyChanged;
using SQLite.CodeFirst;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using System.Linq;
using MoreLinq;

namespace LeagueTracker.Data
{
    public class LeagueTrackerDb : DbContext
    {
        // Your context has been configured to use a 'LeagueTrackerDb' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LeagueTracker.Data.LeagueTrackerDb' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'LeagueTrackerDb' 
        // connection string in the application configuration file.
        public LeagueTrackerDb()
            : base("name=LeagueTrackerDb")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<LeagueTrackerDb>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

        public Game GetGame(Guid id)
        {
            Games.ToList();
            var game = Games.Find(id);
            var accomplishments =
                Accomplishments
                    .Where(x => x.Game.Id == id)
                    .GroupBy(x => x.Name)
                    .SelectMany(x => x)
                    .OrderByDescending(x => x.VersionNumber)
                    .Take(1);
            game.Accomplishments = new ObservableCollection<Accomplishment>(accomplishments);
            return game;
        }

        public virtual DbSet<Game> Games { get; set; }

        public virtual DbSet<Player> Players { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Accomplishment> Accomplishments { get; set; }

        public virtual DbSet<Result> Results { get; set; }

        public virtual DbSet<PlayerResult> PlayerResults { get; set; }

        public virtual DbSet<EventAccomplishment> EventAccomplishments { get; set; }
    }

    [ImplementPropertyChanged]
    public class Game : BaseEntity
    {
        public string Name { get; set; }

        [NotMapped]
        public ObservableCollection<Accomplishment> Accomplishments { get; set; } =
            new ObservableCollection<Accomplishment>();
    }

    [ImplementPropertyChanged]
    public class Player : BaseEntity
    {
        public string Name { get; set; }
        public string UniqueIdentifier { get; set; }
        public DateTime DateAdded { get; set; }
    }

    [ImplementPropertyChanged]
    public class Event : BaseEntity
    {
        public Game Game { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Result> Results { get; set; } = new ObservableCollection<Result>();
        public DateTime DateOfEvent { get; set; }
    }

    [ImplementPropertyChanged]
    public class Result : BaseEntity
    {
        public Event Event { get; set; }

        public ObservableCollection<PlayerResult> PlayerResults { get; set; } = new ObservableCollection<PlayerResult>();
    }

    [ImplementPropertyChanged]
    public class PlayerResult : BaseEntity
    {
        public Event Event { get; set; }

        public ObservableCollection<EventAccomplishment> EventAccomplishments { get; set; } =
            new ObservableCollection<EventAccomplishment>();

        public Player Player { get; set; }
        public int OtherPoints { get; set; }
    }

    [ImplementPropertyChanged]
    public class Accomplishment : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public int VersionNumber { get; set; }
        public Game Game { get; set; }

        public static Accomplishment CreateModifiedAccomplishment(Accomplishment accomplishment) => new Accomplishment
        {
            Name = accomplishment.Name,
            Description = accomplishment.Description,
            Game = accomplishment.Game,
            VersionNumber = accomplishment.VersionNumber + 1
        };
    }

    [ImplementPropertyChanged]
    public class EventAccomplishment : BaseEntity
    {
        public Event Event { get; set; }
        public Accomplishment Accomplishment { get; set; }
        public int Occurrences { get; set; }
    }
}