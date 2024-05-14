using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shortalk___Back_End.Models;
using System.Collections.Concurrent;

namespace Shortalk___Back_End.Services.Context
{
    public class DataContext : DbContext
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new();

        public ConcurrentDictionary<string, UserConnection> connections => _connections;

        public DbSet<UserModel> UserInfo { get; set; }
        public DbSet<LobbyRoomModel> LobbyInfo { get; set; }
        public DbSet<GameModel> GameInfo {get; set;}

        public DataContext(DbContextOptions options) : base(options) { }

        //this function will build out our table in the database
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}