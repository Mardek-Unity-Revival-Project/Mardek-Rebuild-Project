using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPG {
    public abstract class BranchCommand : OngoingCommand
    {
        protected List<CommandBase> commandsBeingExecuted = new List<CommandBase>();
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

        public override void Update()
        {
            if (currentCommand != null)
                CheckOngoingCommand();
        }

        public override bool IsOngoing()
        {
            return waitForExecutionEnd && currentCommand != null;
        }

        public override void Trigger()
        {
            TriggerCurrentCommand();
        }

        void TriggerNextCommand()
        {
            // first unlock player based on the last command executed
            SetPlayerLockByCurrentEventType(false);
            // get next command and trigger it
            RemoveCommandAt0();
            TriggerCurrentCommand();
        }

        void TriggerCurrentCommand()
        {
            if (currentCommand == null)
                return;
            currentCommand.Trigger();
            SetPlayerLockByCurrentEventType(true);
            CheckOngoingCommand();
        }

        void CheckOngoingCommand()
        {
            // check ongoing (current event is non-null and can be converted to OngoingCommand)
            OngoingCommand command = currentCommand as OngoingCommand;
            if (command != null)
            {
                if (command.IsOngoing() == false || command.waitForExecutionEnd == false)
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
