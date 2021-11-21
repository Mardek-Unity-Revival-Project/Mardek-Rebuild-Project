namespace JRPG
{
    public class PopBGMCommands : CommandBase
    {
        public override void Trigger()
        {
            AudioManager.PopMusic();
        }
    }
}
