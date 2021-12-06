using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Database")]
public class AddressableDatabase : ScriptableObject
{
    static AddressableDatabase _instance;
    static AddressableDatabase instance
    {
        get
        {
            if(_instance == null)
                _instance = Resources.Load("Database") as AddressableDatabase;
            return _instance;
        }
    }

    [SerializeField] List<string> filters = new List<string>();
    [SerializeField] List<string> guids = new List<string>();
    [SerializeField] List<Object> objects = new List<Object>();

#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach(var filter in filters)
        {
            if (string.IsNullOrEmpty(filter))
                continue;

            guids.Clear();
            objects.Clear();

            var result = AssetDatabase.FindAssets(filter,null);
            foreach(var guid in result)
            {
                guids.Add(Guid.Parse(guid).ToString());
                var path = AssetDatabase.GUIDToAssetPath(guid);
                objects.Add(AssetDatabase.LoadAssetAtPath<Object>(path));
            }
        }
    }
#endif

    public static Object GetAddressableByGuid(string guid)
    {
        int index = instance.guids.IndexOf(guid);
        return instance.objects[index];
    }

    public static Guid GetGUID(IAddressableGuid addressable)
    {
        int index = instance.objects.IndexOf((Object)addressable);
        return Guid.Parse(instance.guids[index]);
    }
}
