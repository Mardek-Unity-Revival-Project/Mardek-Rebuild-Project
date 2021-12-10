using UnityEngine;

namespace MURP.StatusSystem
{
    [System.Serializable]
    public class StatusHolder<Y, T> where Y : StatusOfType<T>
    {
        [SerializeField] StatusOfType<T> statusEnum;
        [SerializeField] T value;
    }
}