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
            PositionPartyAt(partyPositions, partyDirections);
        }

        private void Awake()
        {
            if (instance)
                Destroy(instance);
            instance = this;
            if (partyPositions.Count > 0)
                PositionPartyAt(partyPositions, partyDirections);
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
                            character.GetComponent<Movable>().FaceDirection(direction);
                        }                        
                    }
                }
            }
            else
            {
                Debug.LogError("No InMapParty found");
            }
        }
        public static void OverridePositionAndDirection(List<Vector2> positions, List<MoveDirection> directions)
        {
            if (instance == null) return;
            instance.partyPositions = positions;
            instance.partyDirections = directions;
        }
    }
}