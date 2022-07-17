using MURP.DiscoverySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MURP.UI
{
    public class EncyclopediaMenu : MonoBehaviour
    {
        [SerializeField] InGameMenu menu;
        [SerializeField] GameObject encyclopediaEntryPrefab;

        [SerializeField] PersonDetails personDetails;
        [SerializeField] PlaceDetails placeDetails;
        [SerializeField] ArtefactDetails artefactDetails;

        public void EnterSelectedSection()
        {
            // TODO Remove this early return once epvery section has its own list
            if (EncyclopediaSection.selected.section == null) return;

            menu.ShowEncyclopediaSection(EncyclopediaSection.selected.section);

            // Clear the old encyclopedia entries because they could be outdated
            var grid = EncyclopediaSection.selected.section.GetComponentsInChildren<GridLayoutGroup>()[0];
            var oldChildren = new List<Transform>();
            foreach (Transform oldChild in grid.gameObject.transform)
            {
                oldChildren.Add(oldChild);
            }
            foreach (Transform oldChild in oldChildren)
            {
                oldChild.gameObject.SetActive(false);
                Destroy(oldChild.gameObject);
            }

            // Insert the up-to-date encyclopedia entries
            for (int itemIndex = 0; itemIndex < EncyclopediaSection.selected.list.items.Count; itemIndex++)
            {
                var item = EncyclopediaSection.selected.list.items[itemIndex];
                EncyclopediaEntry entry = Instantiate(encyclopediaEntryPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<EncyclopediaEntry>();
                entry.Init(item, itemIndex);
                entry.transform.SetParent(grid.transform);
                entry.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            grid.gameObject.GetComponent<SelectableLayout>().RefreshSelectables();
        }

        public void LeaveSelectedSection()
        {
            menu.ExitEncyclopediaSection(EncyclopediaSection.selected.section);
        }

        public void ShowSelectedItem(GameObject encyclopediaSection)
        {
            EncyclopediaItem item = EncyclopediaEntry.selectedEntry.item;
            if (item.isDiscovered)
            {
                encyclopediaSection.SetActive(false);
                if (item is EncyclopediaPerson)
                {
                    personDetails.SetPerson(item as EncyclopediaPerson);
                    personDetails.gameObject.SetActive(true);
                }
                if (item is EncyclopediaPlace)
                {
                    placeDetails.SetPlace(item as EncyclopediaPlace);
                    placeDetails.gameObject.SetActive(true);
                }
                if (item is EncyclopediaArtefact)
                {
                    artefactDetails.SetArtefact(item as EncyclopediaArtefact);
                    artefactDetails.gameObject.SetActive(true);
                }
            }
        }
    }
}
