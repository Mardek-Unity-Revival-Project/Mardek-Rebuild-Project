namespace MURP.StatsSystem 
{
    public interface IStats
    {
        public StatHolder<T, StatOfType<T>> GetStat<T>(StatOfType<T> stat);
        public void ModifyStat<T> (StatOfType<T> stat, float delta);
    } 
}