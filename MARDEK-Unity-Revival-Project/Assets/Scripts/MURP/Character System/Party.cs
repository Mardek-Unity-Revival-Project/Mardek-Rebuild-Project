using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MURP.SaveSystem;

namespace MURP.CharacterSystem
{
    public class Party : AddressableMonoBehaviour
    {
        [SerializeField] List<Character> characters;
        public List<Character> GetCharacters()
        {
            return characters;
        }
    }
}