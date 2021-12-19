using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guid = System.Guid;
using MURP.Core;
using FullSerializer;

namespace MURP.SaveSystem
{
    [System.Serializable]
    public class SaveState
    {
        public Dictionary<Guid, AddresableSaveWrapper> addressableState = new Dictionary<Guid, AddresableSaveWrapper>();
        public class AddresableSaveWrapper
        {
            public string jsonData = default;
        }

        public void SaveObject(IAddressableGuid addressable, fsSerializer serializer)
        {
            Guid guid = addressable.GetGuid();
            if (addressableState.ContainsKey(guid) == false)
                addressableState.Add(guid, null);

            serializer.TrySerialize(addressable.GetType(), addressable, out fsData data);
            var json = fsJsonPrinter.CompressedJson(data);
            var newWrapper = new AddresableSaveWrapper() { jsonData = json };
            addressableState[guid] = newWrapper;
        }
        public bool LoadObjects(IAddressableGuid addressable, fsSerializer serializer)
        {
            Guid guid = addressable.GetGuid();
            if (addressableState.ContainsKey(guid))
            {
                // Addressable found, override object from json
                addressableState.TryGetValue(guid, out AddresableSaveWrapper wrappedAddressable);
                fsJsonParser.Parse(wrappedAddressable.jsonData, out fsData data);
                var type = addressable.GetType();
                var obj = addressable as object;
                serializer.TryDeserialize(data, storageType: type, ref obj);
                return true;
            }
            return false;
        }
    }
}