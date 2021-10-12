using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEditor;
using System;

[CreateAssetMenu(menuName = "Database")]
public class AddressableDatabase : ScriptableObject
{
    static AddressableDatabase instance;

    [SerializeField] List<string> filters = new List<string>();
    [SerializeField] List<Guid> guids = new List<Guid>();
    [SerializeField] List<Object> objects = new List<Object>();

    private void OnValidate()
    {
        if(instance == null)
            instance = this;
        else
            throw new Exception("can't create another AddressableDatabase");

        foreach(var filter in filters)
        {
            if (string.IsNullOrEmpty(filter))
                continue;

            guids.Clear();
            objects.Clear();

            var result = AssetDatabase.FindAssets(filter,null);
            foreach(var guid in result)
            {
                guids.Add(Guid.Parse(guid));
                var path = AssetDatabase.GUIDToAssetPath(guid);
                objects.Add(AssetDatabase.LoadAssetAtPath<Object>(path));
            }
        }
    }

    public static Object GetAddressableByGuid(string guid)
    {
        int index = instance.guids.IndexOf(Guid.Parse(guid));
        return instance.objects[index];
    }

    public static Guid GetGUID(IAddressableGuid addressable)
    {
        int index = instance.objects.IndexOf((Object)addressable);
        return instance.guids[index];
    }
}
