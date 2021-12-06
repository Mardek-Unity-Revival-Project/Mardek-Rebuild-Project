using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MURP.SaveSystem
{
    public class GeneralProgressData : AddressableMonoBehaviour
    {
        [SerializeField] string _gameName = string.Empty;
        public string GameName
        {
            get
            {
                return _gameName;
            }
            set
            {
                if (string.IsNullOrEmpty(_gameName))
                    _gameName = value;
                return;
            }
        }
    }
}
