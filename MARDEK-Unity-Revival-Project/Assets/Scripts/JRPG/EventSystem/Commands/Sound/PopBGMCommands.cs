namespace JRPG
{
    public class PopBGMCommands : CommandBase
    {
        public override void Trigger()
        {
            SoundManager.PopBackgroundMusic();
        }
    }
}
