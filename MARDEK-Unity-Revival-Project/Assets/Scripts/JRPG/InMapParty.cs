using UnityEngine;
using System.Collections.Generic;


namespace JRPG
{
    [SelectionBase]
    public class InMapParty : MonoBehaviour
    {
        static InMapParty instance;
        public static List<Vector2> positionsToLoad = new List<Vector2>();
        public static List<MoveDirection> directionsToLoad = new List<MoveDirection>();

        [SerializeField] List<GameObject> inMapCharacters = new List<GameObject>();
     
        private void Awake()
        {
            if (instance)
                Destroy(instance);
            instance = this;
            if (positionsToLoad.Count > 0)
                PositionPartyAt(positionsToLoad, directionsToLoad);
            positionsToLoad = new List<Vector2>();
            directionsToLoad = new List<MoveDirection>();
        }

        public static void PositionPartyAt(List<Vector2> positions, List<MoveDirection> directions)
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
                        if(directions != null && directions.Count > 0)
                        {
                            var direction = i < directions.Count ? directions[i] : directions[directions.Count-1];
                            character.GetComponent<Movement>().FaceDirection(direction);
                        }                        
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
        public static List<MoveDirection> GetPartyDirections()
        {
            List<MoveDirection> directions = new List<MoveDirection>();
            foreach (var character in instance.inMapCharacters)
                directions.Add(character.GetComponent<Movement>().currentDirection);
            return directions;
        }
    }
}
