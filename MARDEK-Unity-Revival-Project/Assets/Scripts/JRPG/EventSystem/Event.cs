using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JRPG
{
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
        CommandBase currentCommand
        {
            get
            {
                while (commandsBeingExecuted.Count > 0)
                {
                    if (commandsBeingExecuted[0] == null)
                        RemoveCommandAt0();
                    else
                        return commandsBeingExecuted[0];
                }
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
            if(currentCommand)
                CheckOngoingCommand();
        }

        [ContextMenu("Trigger")]
        public void Trigger()
        {
            if (Application.isPlaying)
            {
                if (currentCommand)
                {
                    Debug.LogError("Can't trigger an ongoing event");
                    return;
                }                    
                commandsBeingExecuted = new List<CommandBase>(commands);
                TryTriggerNextCommand();
            }
            else
                Debug.LogError("You can only trigger events when the game is playing");
        }

        void TryTriggerNextCommand()
        {
            if (currentCommand)
            {
                currentCommand.Trigger();
                CheckOngoingCommand();
            }
        }

        void CheckOngoingCommand()
        {
            // check ongoing (current event is non-null and can be converted to OngoingCommand)
            OngoingCommand command = currentCommand as OngoingCommand;
            if (command == null || command.IsOngoing() == false || command.waitForExecutionEnd == false)
            {
                RemoveCommandAt0();
                TryTriggerNextCommand();
            }
        }

        void RemoveCommandAt0()
        {
            if (commandsBeingExecuted.Count > 0)
                commandsBeingExecuted.RemoveAt(0);
        }
    }
}

