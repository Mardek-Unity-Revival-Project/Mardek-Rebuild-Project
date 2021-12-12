using System.Collections.Generic;

namespace MURP.StatsSystem
{
    [System.Serializable]
    public class StatsSet : IStats
    {
        public List<StatHolder<int, StatOfType<int>>> intStats;
        public List<StatHolder<float, StatOfType<float>>> floatStats;

        public StatHolder<int, StatOfType<int>> GetStatus(StatOfType<int> desiredStatus)
        {
            foreach (var statusHolder in intStats)
            {
                if (statusHolder.statusEnum == desiredStatus)
                {
                    return statusHolder;
                }
            }
            return null;
        }
        public StatHolder<float, StatOfType<float>> GetStatus(StatOfType<float> desiredStatus)
        {
            foreach (var statusHolder in floatStats)
            {
                if (statusHolder.statusEnum == desiredStatus)
                {
                    return statusHolder;
                }
            }
            return null;
        }
    }
}