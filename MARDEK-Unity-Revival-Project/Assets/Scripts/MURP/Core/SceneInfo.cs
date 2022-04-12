using UnityEngine;

namespace MURP.Core
{
    public class SceneInfo : MonoBehaviour
    {
        [field: SerializeField] public string id { get; private set; }
        [field: SerializeField] public string displayName { get; private set; }

        public void OnValidate()
        {
            SceneInfo checkDuplicate = SceneInfo.FindObjectOfType<SceneInfo>();
            if (checkDuplicate != this)
            {
                throw new System.ApplicationException("Duplicated SceneInfo: " + this.id + " vs " + checkDuplicate.id);
            }
        }
    }
}
