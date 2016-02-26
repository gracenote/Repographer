using Repographer.Modules.Main.Factories;
using Repographer.Mono.Options;

namespace Repographer.Modules.Main.Settings
{
    public interface IRepoActionSettings
    {
        OptionSet Set { get; }

        IRepoAction CreateAction(IRepoActionFactory factory);
    }
}