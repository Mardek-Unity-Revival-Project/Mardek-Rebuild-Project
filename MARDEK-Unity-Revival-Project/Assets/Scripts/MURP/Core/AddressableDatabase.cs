using UnityEngine;
using Object = UnityEngine.Object;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MURP.Core
{
    //[CreateAssetMenu(menuName = "MURP/Database")]
    public class AddressableDatabase : ScriptableObject
    {
        static AddressableDatabase _instance;
        static AddressableDatabase instance
        {
            get
            {
                if (_instance == null)
                    _instance = Resources.Load("Database") as AddressableDatabase;
                return _instance;
            }
        }

        [SerializeField] List<string> filters = new List<string>();
        [SerializeField] List<string> guids = new List<string>();
        [SerializeField] List<Object> objects = new List<Object>();

        [ContextMenu("Validate")]
        void ValidateDatabase()
        {
            guids.Clear();
            objects.Clear();
            foreach (var filter in filters)
            {
                if (string.IsNullOrEmpty(filter))
                    continue;

                var result = AssetDatabase.FindAssets(filter, null);
                foreach (var guid in result)
                {
                    guids.Add(Guid.Parse(guid).ToString());
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    objects.Add(AssetDatabase.LoadAssetAtPath<Object>(path));
                }
            }
        }

        public static Object GetAddressableByGuid(string guid)
        {
            int index = instance.guids.IndexOf(guid);
            if (index == -1)
                return null;
            return instance.objects[index];
        }

        public static Guid GetGUID(IAddressableGuid addressable)
        {
            int index = instance.objects.IndexOf((Object)addressable);
            if (index == -1) 
                return Guid.Empty;
            return Guid.Parse(instance.guids[index]);
        }
    }
}