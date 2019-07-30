using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Quartz.Infrastructure.GameBackend
{

    [Table("be_betaggregate_game")]
    public class BetAggregate_Game
    {
        public long id { get; set; }
        public DateTime? create_date { get; set; }

        /// <summary>
        /// 代理编号
        /// </summary>
        public string agent_code { get; set; }
        /// <summary>
        /// 游戏编号
        /// </summary>
        public int game_id { get; set; }
        /// <summary>
        /// 游戏名称
        /// </summary>
        public string game_name { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public Device device { get; set; }
        /// <summary>
        /// 投注金额
        /// </summary>
        public decimal bet_amount { get; set; }
        /// <summary>
        /// 有效下注金额
        /// </summary>
        public decimal valid_bet_amount { get; set; }
        /// <summary>
        /// 派彩
        /// </summary>
        public decimal pay_out { get; set; }
        /// <summary>
        /// 输赢金额
        /// </summary>
        public decimal win_amount { get; set; }

    }


    /// <summary>
    /// 注单表
    /// </summary>
    [Table("game_bet")]
    public class Bet
    {

        public long id { get; set; }
        public DateTime? create_date { get; set; }
        public string create_person { get; set; }
        public DateTime? update_date { get; set; }
        public string update_person { get; set; }

        /// <summary>
        /// 注单编号
        /// </summary>
        public string bet_code { get; set; }
        /// <summary>
        /// 代理编号
        /// </summary>
        public string agent_code { get; set; }
        /// <summary>
        /// 游戏编号
        /// </summary>
        public int game_id { get; set; }
        /// <summary>
        /// 游戏名称
        /// </summary>
        public string game_name { get; set; }
        /// <summary>
        /// 房间编号
        /// </summary>
        public string room_code { get; set; }
        /// <summary>
        /// 桌子编号
        /// </summary>
        public string desk_code { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public long user_id { get; set; }
        /// <summary>
        /// 玩家名称
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 局号
        /// </summary>
        public string round_code { get; set; }
        public long round_id { get; set; }
        /// <summary>
        /// 抽成
        /// </summary>
        public decimal commission { get; set; }
        /// <summary>
        /// 玩家派彩后金额
        /// </summary>
        public decimal money { get; set; }

        /// <summary>
        /// 投注金额
        /// </summary>
        public decimal bet_amount { get; set; }
        /// <summary>
        /// 有效下注金额
        /// </summary>
        public decimal valid_bet_amount { get; set; }
        /// <summary>
        /// 派彩金额
        /// </summary>
        public decimal pay_out { get; set; }
        /// <summary>
        /// 输赢金额
        /// </summary>
        public decimal win_amount { get; set; }
        /// <summary>
        /// 结算状态  0未结算 1已结算
        /// </summary>
        public SettleFlag settle_flag { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public Device device { get; set; }
        /// <summary>
        /// 登录IP
        /// </summary>
        public string login_ip { get; set; }
        /// <summary>
        /// 货币类型 cny
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 投注时间
        /// </summary>
        public DateTime? bet_date { get; set; }
        /// <summary>
        /// 派彩时间
        /// </summary>
        public DateTime pay_out_date { get; set; }
        /// <summary>
        /// 注单结果
        /// </summary>
        public string result { get; set; }
    }

    public enum Device
    {
        /// <summary>
        /// PC
        /// </summary>
        PC,
        /// <summary>
        /// Android
        /// </summary>
        Android,
        /// <summary>
        /// IOS
        /// </summary>
        IOS
    }

    public enum SettleFlag
    {
        未结算,
        已结算
    }
}
