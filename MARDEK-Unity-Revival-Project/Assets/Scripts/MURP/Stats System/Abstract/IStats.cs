namespace MURP.StatsSystem 
{
    public interface IStats
    {
        public StatHolder<int, StatOfType<int>> GetStatus(StatOfType<int> desiredStatus);
        public StatHolder<float, StatOfType<float>> GetStatus(StatOfType<float> desiredStatus);
    } 
}