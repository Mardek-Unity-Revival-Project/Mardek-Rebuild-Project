using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG
{
    [System.Serializable]
    public class CommandQueue
    {
        [SerializeField] GameObject commandsGameObject;
        public bool isOngoing { get; private set; } = false;
        Queue<CommandBase> commandQueue = new Queue<CommandBase>();
        CommandBase currentCommand = null;

        public void TriggerFirst()
        {
            if (isOngoing)
            {
                Debug.LogWarning("Trying to trigger event, but this event is already ongoing");
                return;
            }
            else
            {
                commandQueue = new Queue<CommandBase>(commandsGameObject.GetComponents<CommandBase>());
                if (commandQueue.Count > 0)
                {
                    //Debug.Log($"starting command queue with {commandQueue.Count} commands");
                    isOngoing = true;
                    GetAndTriggerNextCommand();
                }
                else
                {
                    Debug.LogWarning("Trying to trigger event with no commands");
                }
            }
        }
        
        public void TryAdvanceQueue()
        {
            if (isOngoing)
            {
                UpdateCurrentCommand();
            }
        }

        CommandBase NextCommand()
        {
            if (commandQueue.Count > 0)
                return commandQueue.Dequeue();
            else
                return null;
        }

        void UpdateCurrentCommand()
        {
            // check ongoing (current event is non-null and can be converted to OngoingCommand)
            OngoingCommand command = currentCommand as OngoingCommand;
            if (command != null)
            {
                if (command.IsOngoing() == false || command.waitForExecutionEnd == false)
                {
                    SetPlayerLockByCurrentEventType(false);
                    GetAndTriggerNextCommand();
                }
                else
                {
                    command.Update();
                }
            }
            else
            {
                GetAndTriggerNextCommand();
            }
        }

        void GetAndTriggerNextCommand()
        {
            currentCommand = NextCommand();
            if (currentCommand)
            {
                currentCommand.Trigger();
                SetPlayerLockByCurrentEventType(true);
                TryAdvanceQueue();
            }
            else
            {
                isOngoing = false;
                //Debug.Log("Command queue ended");
            }
        }

        void SetPlayerLockByCurrentEventType(bool setValue)
        {
            OngoingCommand cmd = currentCommand as OngoingCommand;
            if (cmd != null && cmd.waitForExecutionEnd)
            {
                if (setValue == true)
                    PlayerController.playerControllerLockValue++;
                else
                    PlayerController.playerControllerLockValue--;
            }
        }
    }
}
