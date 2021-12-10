using System.Collections.Generic;
using UnityEngine;
using MURP.StatusSystem;

namespace MURP.CharacterSystem
{
    public class CharacterStatusSet : MonoBehaviour, IAffectable
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