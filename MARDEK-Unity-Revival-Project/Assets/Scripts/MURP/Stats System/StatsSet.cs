using System.Collections.Generic;

namespace MURP.StatsSystem
{
    [System.Serializable]
    public class StatsSet : IStats
    {
        public List<StatHolder<int, StatOfType<int>>> statuses;

        public StatHolder<int, StatOfType<int>> GetStatus(StatOfType<int> desiredStatus)
        {
            foreach (var statusHolder in statuses)
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
            throw new System.NotImplementedException();
        }
    }
}