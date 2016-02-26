#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
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