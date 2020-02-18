using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Character", menuName = "Character")]
public class Character : ScriptableObject {
    public string displayName = null;
    public string element = null;
}
