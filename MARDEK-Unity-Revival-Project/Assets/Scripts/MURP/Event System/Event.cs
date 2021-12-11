using UnityEngine;

namespace MURP.EventSystem
{
    public class Event : MonoBehaviour
    {
        [Header("Event Commands")]
        [SerializeField] CommandQueue commands;

        [Header("Event Triggers")]
        [SerializeField] bool onStart = false;
        [SerializeField] bool onInteractionKey = false;
        [SerializeField] bool onTriggerEnter = false;

        void Start()
        {
            if (onStart) commands.TriggerFirst();
        }

        public void Interact()
        {
            if (onInteractionKey) commands.TriggerFirst();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (onTriggerEnter)
                if (collision.gameObject.CompareTag("Player"))
                    commands.TriggerFirst();
        }

        private void Update()
        {
            if (commands.isOngoing)
                commands.TryAdvanceQueue();
        }

        [ContextMenu("Trigger")]
        public void Trigger()
        {
            if (Application.isPlaying)
            {
                commands.TriggerFirst();
            }
            else
                Debug.LogError("You can only trigger events when the game is playing");
        }
    }
}