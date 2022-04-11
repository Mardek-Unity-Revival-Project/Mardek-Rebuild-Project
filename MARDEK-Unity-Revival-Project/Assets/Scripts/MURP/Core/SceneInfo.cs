using UnityEngine;

namespace MURP.Core
{
    public class SceneInfo : MonoBehaviour
    {
        [field: SerializeField] public string id { get; private set; }
        [field: SerializeField] public string displayName { get; private set; }
    }
}
