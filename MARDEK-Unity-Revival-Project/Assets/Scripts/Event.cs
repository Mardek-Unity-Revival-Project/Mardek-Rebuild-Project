using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JRPG
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Event : MonoBehaviour
    {
        [Header("Trigger Methods")]
        [SerializeField] bool onInteractionKey = false;
        [SerializeField] bool onPlayerTouch = false;
        [SerializeField] bool onStart = false;

        [Space(10)]
        [SerializeField] UnityEvent OnTriggerEvent = default;

        private void Start()
        {
            if (onStart)
                Trigger();
        }

        [ContextMenu("Trigger")]
        public void Trigger()
        {
            
        }
    }
}

