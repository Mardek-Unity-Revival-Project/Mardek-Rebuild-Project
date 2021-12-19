using System;
using System.Collections.Generic;

namespace MURP.StatsSystem
{
    [System.Serializable]
    public class StatsSet : IStats
    {
        public List<StatHolder<int, StatOfType<int>>> intStats;
        public List<StatHolder<float, StatOfType<float>>> floatStats;

        public StatHolder<T, StatOfType<T>> GetStat<T>(StatOfType<T> stat)
        {
            if (typeof(T) == typeof(int))
                return GetStatFromList(stat as StatOfType<int>, intStats) as StatHolder<T, StatOfType<T>>;
            if (typeof(T) == typeof(float))
                return GetStatFromList(stat as StatOfType<float>, floatStats) as StatHolder<T, StatOfType<T>>;
            return null;
        }
        private StatHolder<T, StatOfType<T>> GetStatFromList<T>(StatOfType<T> stat, List<StatHolder<T, StatOfType<T>>> statList)
        {
            foreach (var statusHolder in statList)
                if (statusHolder.statusEnum == stat)
                    return statusHolder as StatHolder<T, StatOfType<T>>;
            return null;
        }
    }
}