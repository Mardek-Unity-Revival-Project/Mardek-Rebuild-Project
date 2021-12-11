namespace MURP.StatusSystem 
{
    public interface IStatus
    {
        public StatusHolder<int, StatusOfType<int>> GetStatus(StatusOfType<int> desiredStatus);
        public StatusHolder<float, StatusOfType<float>> GetStatus(StatusOfType<float> desiredStatus);
    } 
}