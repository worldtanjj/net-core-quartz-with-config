using Microsoft.EntityFrameworkCore;
using System;

namespace Quartz.Infrastructure.GameBackend
{
    /// <summary>
    /// 游戏后台
    /// </summary>
    public class GameBackendContext : DbContext
    {
        public GameBackendContext(DbContextOptions<GameBackendContext> options)
            : base(options)
        {
            
        }

        public DbSet<BetAggregate_Game> BetAggregate_Game { get; set; }
        public DbSet<Bet> Bets { get; set; }

    }



}
