using UnityEngine;
using System.Collections.Generic;


namespace JRPG
{
    [SelectionBase]
    public class InMapParty : MonoBehaviour
    {
        static InMapParty instance;
        public static List<Vector2> positionsToLoad = new List<Vector2>();

        [SerializeField] List<GameObject> inMapCharacters = new List<GameObject>();
     
        private void Awake()
        {
            if (instance)
                Destroy(instance);
            instance = this;
        }

        private void OnLevelWasLoaded(int level)
        {
            if (positionsToLoad.Count > 0)
                PositionPartyAt(positionsToLoad, null);
        }

        public static void PositionPartyAt(List<Vector2> positions, MoveDirection facingDirection)
        {
            if (instance)
            {
                for (int i = 0; i < instance.inMapCharacters.Count; i++)
                {
                    GameObject character = instance.inMapCharacters[i];
                    if (character != null)
                    {
                        var position = i < positions.Count ? positions[i] : positions[positions.Count-1];
                        Utilities2D.SetTransformPosition(character.transform, position);
                        if (facingDirection)
                            character.GetComponent<Movement>().FaceDirection(facingDirection);
                    }
                }
            }
            else
            {
                Debug.LogError("No InMapParty found");
            }
        }

        public static List<Vector2> GetPartyPosition()
        {
            List<Vector2> pos = new List<Vector2>();
            foreach (var character in instance.inMapCharacters)
                pos.Add(character.transform.position);
            return pos;
        }
    }
}
