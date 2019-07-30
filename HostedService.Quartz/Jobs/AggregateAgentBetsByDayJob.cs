using Microsoft.EntityFrameworkCore;
using NLog;
using Quartz;
using Quartz.Infrastructure.GameBackend;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HostedService.Quartz.Jobs
{
    [DisallowConcurrentExecution]
    public class AggregateAgentBetsByDayJob : IJob
    {
        private readonly Logger _logger;
        private readonly GameBackendContext _context;

        public AggregateAgentBetsByDayJob(GameBackendContext context)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _context = context;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                //汇总昨天游戏数据，加入到 游戏汇总表

                var isFinished = _context.Set<BetAggregate_Game>().AsNoTracking().Any(x => x.create_date >= DateTime.Today.AddDays(-1));
                if (isFinished)
                {
                    return;
                }

                var list = _context.Set<Bet>().AsNoTracking()
                    .Where(p => p.create_date >= DateTime.Today.AddDays(-1) && p.create_date < DateTime.Today)
                    .Where(x => x.settle_flag == SettleFlag.已结算)
                    .GroupBy(x => new { x.game_id, x.agent_code, x.device })
                    .Select(g => new BetAggregate_Game
                    {
                        agent_code = g.Key.agent_code,
                        game_id = g.Key.game_id,
                        game_name = g.FirstOrDefault().game_name,
                        device = g.Key.device,
                        bet_amount = g.Sum(x => x.bet_amount),
                        valid_bet_amount = g.Sum(x => x.valid_bet_amount),
                        win_amount = g.Sum(x => x.win_amount),
                        pay_out = g.Sum(x => x.pay_out),
                        create_date = DateTime.Today.AddDays(-1),
                    }).ToList();

                _context.Set<BetAggregate_Game>().AddRange(list);
                await _context.SaveChangesAsync();

                return;

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return;

            }
        }
    }
}
