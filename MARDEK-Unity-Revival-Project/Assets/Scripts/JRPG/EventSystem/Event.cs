using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JRPG
{
    public class Event : MonoBehaviour
    {
        [CreateReference(type = typeof(CommandBase))] 
        [SerializeReference] List<CommandBase> commands = new List<CommandBase>();

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

        private void Update()
        {
            if(currentCommand != null)
                CheckOngoingCommand();
        }

        [ContextMenu("Trigger")]
        public void Trigger()
        {
            if (Application.isPlaying)
            {
                if (currentCommand != null)
                {
                    Debug.LogWarning("Trying to trigger event, but this event is already ongoing");
                    return;
                }                    
                commandsBeingExecuted = new List<CommandBase>(commands);
                TriggerCurrentCommand();
            }
            else
                Debug.LogError("You can only trigger events when the game is playing");
        }

        void TriggerNextCommand()
        {
            RemoveCommandAt0();
            TriggerCurrentCommand();
        }

        void TriggerCurrentCommand()
        {
            if (currentCommand == null)
                return;
            currentCommand.Trigger();
            CheckOngoingCommand();
        }

        void CheckOngoingCommand()
        {
            // check ongoing (current event is non-null and can be converted to OngoingCommand)
            OngoingCommand command = currentCommand as OngoingCommand;
            if (command != null)
            {
                if(command.IsOngoing() == false || command.waitForExecutionEnd == false)
                {
                    TriggerNextCommand();
                }
                else
                {
                    command.Update();
                }
            }
            else
            {
                TriggerNextCommand();
            }
        }

        void RemoveCommandAt0()
        {
            if (commandsBeingExecuted.Count > 0)
                commandsBeingExecuted.RemoveAt(0);
        }
    }
}

