﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Saliens
{
    /// <summary>
    /// The difficulty of a zone.
    /// </summary>
    public enum ZoneDifficulty
    {
        Invalid,
        Easy,
        Medium,
        Hard,
    }

    public enum ZoneType
    {
        Invalid = 2,
        Normal = 3,
        Boss = 4
    }

    public class ZoneResponse
    {
        [JsonProperty(PropertyName = "zone_info", Required = Required.Always)]
        public Zone Zone { get; private set; }

        [JsonProperty(PropertyName = "waiting_for_players", Required = Required.DisallowNull)]
        public bool Waiting { get; private set; }

        [JsonProperty(PropertyName = "gameid", Required = Required.DisallowNull)]
        public int GameID { get; private set; }
    }

    public class Zone
    {
        [JsonProperty(PropertyName = "zone_position", Required = Required.Always)]
        public int Position { get; private set; }

        [JsonProperty(PropertyName = "leader", Required = Required.DisallowNull)]
        public ClanInfo Leader { get; private set; }

        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        public ZoneType @Type { get; private set; }

        [JsonProperty(PropertyName = "gameid", Required = Required.DisallowNull)]
        public int GameID { get; private set; }

        [JsonProperty(PropertyName = "difficulty", Required = Required.Always)]
        public ZoneDifficulty Difficulty { get; private set; }

        [JsonProperty(PropertyName = "captured", Required = Required.Always)]
        public bool Captured { get; private set; }

        [JsonProperty(PropertyName = "capture_progress", Required = Required.DisallowNull)]
        public float CaptureProgress { get; private set; }

        [JsonProperty(PropertyName = "top_clans", Required = Required.DisallowNull)]
        public ClanInfo[] TopClans { get; private set; }

        [JsonProperty(PropertyName = "boss_active", Required = Required.DisallowNull)]
        public bool BossActive { get; private set; }

        [JsonIgnore]
        public int MaxScore => 300 << (int)Difficulty;

        [JsonIgnore]
        public int Tickrate
        {
            get
            {
                switch (Type)
                {
                    case ZoneType.Normal:
                        return 110 * 1000;
                    case ZoneType.Boss:
                        return 5 * 1000;
                    default:
                        return 0;
                }
            }
        }

        [JsonIgnore]
        public bool IsActiveBossZone => (BossActive && Type == ZoneType.Boss);
    }
}
