namespace MURP.StatsSystem 
{
    public interface IStats
    {
        public StatHolder<T, StatOfType<T>> GetStat<T>(StatOfType<T> desiredStatus);
    } 
}