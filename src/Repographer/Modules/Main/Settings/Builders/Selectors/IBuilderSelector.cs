namespace Repographer.Modules.Main.Settings.Builders.Selectors
{
    public interface IBuilderSelector<in T>
    {
        ISettingsBuilder GetBuilder(T settings);
    }
}