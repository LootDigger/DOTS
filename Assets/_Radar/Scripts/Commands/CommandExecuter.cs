namespace Radar.Commands
{
    public struct CommandExecuter 
    {
        public static void ExecuteCommandNonManaged<T>(T command) where T : struct, ICommand
        {
            command.Execute();
        }
    }
}