using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JRPG
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(GridObject))]
    public class Event : MonoBehaviour
    {
        [Header("Trigger Methods")]
        //[SerializeField] bool onInteractionKey = false;
        //[SerializeField] bool onPlayerTouch = false;
        [SerializeField] bool onStart = false;

        [Space(10)]
        [ExtendedSO]
        [SerializeField] List<CommandBase> commands = default;
        List<CommandBase> commandsBeingExecuted = new List<CommandBase>();
        CommandBase ongoingCommand
        {
            get
            {
                if (commandsBeingExecuted.Count > 0)
                    return commandsBeingExecuted[0];
                return null;
            }
        }

        void Start()
        {
            if (onStart)
                Trigger();
        }

        private void Update()
        {
            if(commandsBeingExecuted.Count > 0)
                CheckOngoingCommand();
        }

        [ContextMenu("Trigger")]
        public void Trigger()
        {
            if (Application.isPlaying)
            {
                commandsBeingExecuted = new List<CommandBase>(commands);
                TryTriggerNextCommand();
            }
            else
                Debug.LogError("You can only trigger events when the game is playing");
        }

        void TryTriggerNextCommand()
        {
            // check trigger
            if (commandsBeingExecuted.Count == 0)
            {

            }
            else
            {
                //get and trigger next command
                if(ongoingCommand == null)
                {
                    Debug.LogWarning("Skipping execution of Null command");
                    commandsBeingExecuted.RemoveAt(0);
                    TryTriggerNextCommand();
                    return;
                }
                ongoingCommand.Trigger();
                CheckOngoingCommand();
            }
        }

        void CheckOngoingCommand()
        {
            // check ongoing (current event is non-null and can be converted to OngoingCommand)
            if (ongoingCommand is OngoingCommand)
            {
                OngoingCommand command = ongoingCommand as OngoingCommand;
                if (command.IsOngoing() == false || command.waitForExecutionEnd == false)
                {
                    commandsBeingExecuted.RemoveAt(0);
                    TryTriggerNextCommand();
                }
            }
            else
            {
                commandsBeingExecuted.RemoveAt(0);
            }
        }
    }
}

