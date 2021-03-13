using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestReference
{
    [SerializeField] public string value = "value";
}

public class Test3 : TestReference
{
    [SerializeField] public string otherValue = "otherValue";
    [SerializeField]
    [CreateReference(typeof(TestReference))] TestReference testReference = null;
}

public class Test4 : TestReference
{
    [SerializeField] MonoBehaviour mb = null;
}