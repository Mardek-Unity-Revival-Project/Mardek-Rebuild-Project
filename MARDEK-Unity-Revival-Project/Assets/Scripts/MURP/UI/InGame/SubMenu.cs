using UnityEngine;
using MURP.CharacterSystem;

namespace MURP.UI
{
    public class SubMenu : MonoBehaviour
    {
        public virtual void SetActive(){}

        public virtual void SetInActive(){}

        public virtual void SetParty(Party theParty){}
    }
}