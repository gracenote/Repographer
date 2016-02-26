namespace Repographer.Modules.Main.Settings
{
    public interface IRepoActionSettingsFactory
    {
        IRepoActionSettings Create(string[] args);
    }
}