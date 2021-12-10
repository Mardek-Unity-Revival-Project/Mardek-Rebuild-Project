namespace MURP.StatusSystem 
{
    public interface IAffectable
    {
        public StatusHolder<int, StatusOfType<int>> GetStatus(StatusOfType<int> desiredStatus);
        public StatusHolder<float, StatusOfType<float>> GetStatus(StatusOfType<float> desiredStatus);
    } 
}