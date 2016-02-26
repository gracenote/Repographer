using Repographer.Modules.Main.Settings;

namespace Repographer.Modules.Main.Factories.Selectors
{
    public interface IRepoActionFactorySelector
    {
        IRepoActionFactory GetFactory(IRepoActionSettings repoActionSettings);
    }
}