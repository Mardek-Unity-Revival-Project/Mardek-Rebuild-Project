using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonoBehaviour : MonoBehaviour
{
    [SerializeReference]
    [CreateReference(typeof(JRPG.CommandBase))]
    JRPG.CommandBase command = null;

    [CreateReference(typeof(TestReference))]
    [SerializeReference] public List<TestReference> test = new List<TestReference>() { new TestReference() };


    [CreateReference(typeof(TestReference))]
    [SerializeReference]
    public TestReference testReference = new TestReference();
}
