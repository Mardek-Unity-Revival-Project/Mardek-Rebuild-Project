using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;

namespace JRPG
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
