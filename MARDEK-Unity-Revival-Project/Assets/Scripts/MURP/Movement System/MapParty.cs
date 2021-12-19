using UnityEngine;
using System.Collections.Generic;
using MURP.Core;
using MURP.SaveSystem;

namespace MURP.MovementSystem
{
    [SelectionBase]
    public class MapParty : AddressableMonoBehaviour
    {
        static MapParty instance;
        static bool forceLoadOnNextAwake = false;
        [SerializeField, FullSerializer.fsIgnore] List<GameObject> inMapCharacters = new List<GameObject>();

        //[Header("--- Saved fields ---")]
        [SerializeField] List<Vector2> partyPositions = new List<Vector2>();
        [SerializeField] List<MoveDirection> partyDirections = new List<MoveDirection>();

        List<Vector2> GetPartyPosition()
        {
            if (instance == null) return null;
            List<Vector2> pos = new List<Vector2>();
            foreach (var character in instance.inMapCharacters)
                pos.Add(character.transform.position);
            return pos;
        }
        List<MoveDirection> GetPartyDirections()
        {
            if (instance == null) return null;
            List<MoveDirection> directions = new List<MoveDirection>();
            foreach (var character in instance.inMapCharacters)
                directions.Add(character.GetComponent<Movable>().currentDirection);
            return directions;
        }

        public override void Save()
        {
            partyPositions = GetPartyPosition();
            partyDirections = GetPartyDirections();
            base.Save();
        }

        private void Awake()
        {
            if (instance)
                Destroy(instance);
            instance = this;

            if (forceLoadOnNextAwake)
            {
                Load();
                forceLoadOnNextAwake = false;
            }
            if (partyPositions.Count > 0)
                PositionCharactersAt(partyPositions, partyDirections);
        }

        void PositionCharactersAt(List<Vector2> positions, List<MoveDirection> directions)
        {
            for (int i = 0; i < inMapCharacters.Count; i++)
            {
                GameObject character = inMapCharacters[i];
                if (character != null)
                {
                    var position = i < positions.Count ? positions[i] : positions[positions.Count - 1];
                    Utilities2D.SetTransformPosition(character.transform, position);
                    if (directions != null && directions.Count > 0)
                    {
                        var direction = i < directions.Count ? directions[i] : directions[directions.Count - 1];
                        character.GetComponent<Movable>().FaceDirection(direction);
                    }
                }
            }
        }

        public void SetForceLoadOnNextAwake()
        {
            forceLoadOnNextAwake = true;
        }

        public static void OverrideAfterTransition(List<Vector2> positions, List<MoveDirection> directions)
        {
            instance.PositionCharactersAt(positions, directions);
        }
    }
}