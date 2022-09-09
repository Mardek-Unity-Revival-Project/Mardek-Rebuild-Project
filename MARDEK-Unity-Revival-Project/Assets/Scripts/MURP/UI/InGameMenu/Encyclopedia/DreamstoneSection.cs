using MURP.DiscoverySystem;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class DreamstoneSection : MonoBehaviour
    {
        [SerializeField] DreamstoneList dreamstones;
        [SerializeField] GameObject dreamstoneEntryPrefab;
        [SerializeField] DreamstoneMessageView dreamstoneView;

        void Start()
        {
            var grid = this.GetComponentsInChildren<GridLayoutGroup>()[0];

            // The first time the dreamstones are viewed, the list needs to be initialized
            if (grid.transform.childCount == 0)
            {
                foreach (Dreamstone dreamstone in dreamstones.dreamstones)
                {
                    DreamstoneEntry entry = Instantiate(dreamstoneEntryPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<DreamstoneEntry>();
                    entry.Init(dreamstone);
                    entry.transform.SetParent(grid.transform);
                    entry.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                grid.gameObject.GetComponent<SelectableLayout>().RefreshSelectables();
            }
        }

        public void OpenSelectedDreamstone()
        {
            dreamstoneView.OpenDreamstone(DreamstoneEntry.selected);
        }
    }
}
