namespace Repographer.Modules.Main.Settings.Builders
{
    public interface ISettingsBuilder
    {
        void SetHelp(bool help);

        void SetArgs(string[] args);

        IRepoActionSettings Build();
    }

    public interface ISettingsBuilder<in T> : ISettingsBuilder
    {
        bool IsBuilderFor(T settings);
    }
}