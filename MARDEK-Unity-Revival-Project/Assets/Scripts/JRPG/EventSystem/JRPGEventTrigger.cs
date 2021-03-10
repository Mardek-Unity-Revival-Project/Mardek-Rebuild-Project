﻿using UnityEngine;
using System.Collections;

namespace JRPG
{
    public class JRPGEventTrigger : MonoBehaviour
    {
        [SerializeField] Event _event = null;

        [Header("Trigger Methods")]
        [SerializeField] bool onStart = false;
        [SerializeField] bool onInteractionKey = false;
        //[SerializeField] bool onPlayerTouch = false;

        void Start()
        {
            if (onStart) _event.Trigger();
        }

        public void Interact()
        {
            if(onInteractionKey) _event.Trigger();
        }
    }
}