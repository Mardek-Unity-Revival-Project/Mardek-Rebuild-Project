using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;
using System;

namespace MURP.Core
{
    public class GuidReferenceConverter : fsDirectConverter<IAddressableGuid>
    {
        
        public override object CreateInstance(fsData data, Type storageType)
        {
            //shoudn't create an instance of an addressable, get reference from database instead
            var guid = data.AsDictionary["refGuid"].AsString;
            return AddressableDatabase.GetAddressableByGuid(guid);
        }

        protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref IAddressableGuid model)
        {
            return fsResult.Success;
        }

        protected override fsResult DoSerialize(IAddressableGuid model, Dictionary<string, fsData> serialized)
        {
            serialized["refGuid"] = new fsData(model.GetGuid().ToString());
            return fsResult.Success;
        }
    }
}