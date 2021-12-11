using System.Collections.Generic;

namespace MURP.StatusSystem
{
    [System.Serializable]
    public class StatusSet : IStatus
    {
        public List<StatusHolder<int, StatusOfType<int>>> statuses;

        public StatusHolder<int, StatusOfType<int>> GetStatus(StatusOfType<int> desiredStatus)
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
        public StatusHolder<float, StatusOfType<float>> GetStatus(StatusOfType<float> desiredStatus)
        {
            throw new System.NotImplementedException();
        }
    }
}