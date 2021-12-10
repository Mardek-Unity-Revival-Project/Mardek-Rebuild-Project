using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.StatusSystem;

namespace MURP.CharacterSystem
{
    [System.Serializable]
    public class Character : MonoBehaviour
    {
        [SerializeField] CharacterBio bio;
        [SerializeField] StatusSet baseStatus;
    }
}