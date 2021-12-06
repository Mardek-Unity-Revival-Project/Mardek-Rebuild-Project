using MURP.EventSystem;

namespace MURP.Audio
{
    public class PopBGMCommands : CommandBase
    {
        public override void Trigger()
        {
            AudioManager.PopMusic();
        }
    }
}