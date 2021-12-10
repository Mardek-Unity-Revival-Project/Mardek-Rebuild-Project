using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;

namespace MURP.Core
{
    [fsObject(Converter = typeof(GuidReferenceConverter))]
    public abstract class AddressableScriptableObject : ScriptableObject, IAddressableGuid
    {
        public Guid GetGuid()
        {
            return AddressableDatabase.GetGUID(this);
        }
    }
}
