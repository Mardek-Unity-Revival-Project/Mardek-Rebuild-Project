using System;
using UnityEngine;
using FullSerializer;
using MURP.SaveSystem;

namespace MURP.Movement
{
    [fsObject(Converter = typeof(GuidReferenceConverter))]
    public class MoveDirection : ScriptableObject, IAddressableGuid
    {
        [SerializeField] Vector2 direction = Vector2.zero;
        public Vector2 value
        {
            get { return direction; }
        }

        public Guid GetGuid()
        {
            return AddressableDatabase.GetGUID(this);
        }
    }
}