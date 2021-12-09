using UnityEngine;
using System.Collections.Generic;
using MURP.Core;
using MURP.SaveSystem;

namespace MURP.Movement
{
    [SelectionBase]
    public class InMapParty : AddressableMonoBehaviour
    {
        static InMapParty instance;
        [SerializeField, FullSerializer.fsIgnore] List<GameObject> inMapCharacters = new List<GameObject>();

        private static List<Vector2> partyPositionsOverride;
        private static List<MoveDirection> partyDirectionsOverride;

        //Saved properties
        [SerializeField, HideInInspector] List<Vector2> partyPositions = new List<Vector2>();
        [SerializeField, HideInInspector] List<MoveDirection> partyDirections = new List<MoveDirection>();

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
        public override void Load()
        {
            base.Load();
            partyPositionsOverride = partyPositions;
            partyDirectionsOverride = partyDirections;
        }

        private void Awake()
        {
            if (instance)
                Destroy(instance);
            instance = this;
            if(partyPositionsOverride?.Count > 0)
            {
                OverridePositionAndDirection(partyPositionsOverride, partyDirectionsOverride);
                partyPositionsOverride = new List<Vector2>();
                partyDirectionsOverride = new List<MoveDirection>();
            }
        }
        void OverridePositionAndDirection(List<Vector2> positions, List<MoveDirection> directions)
        {
            for (int i = 0; i < inMapCharacters.Count; i++)
            {
                GameObject character = inMapCharacters[i];
                if (character != null)
                {
                    var position = i < positions.Count ? positions[i] : positions[positions.Count-1];
                    Utilities2D.SetTransformPosition(character.transform, position);
                    if(directions != null && directions.Count > 0)
                    {
                        var direction = i < directions.Count ? directions[i] : directions[directions.Count-1];
                        character.GetComponent<Movable>().FaceDirection(direction);
                    }                        
                }
            }

        }
        public static void SetPositionAndDirectionOverrides(List<Vector2> positions, List<MoveDirection> directions)
        {
            partyPositionsOverride = positions;
            partyDirectionsOverride = directions;
        }
    }
}