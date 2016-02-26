using Repographer.Modules.Main.Settings;

namespace Repographer.Modules.Main.Factories
{
    public interface IRepoActionFactory
    {
        bool IsFactoryFor(IRepoActionSettings repoActionSettings);
    }

    public interface IRepoActionFactory<in T> : IRepoActionFactory
        where T : IRepoActionSettings
    {
        IRepoAction Create(T settings);
    }
}