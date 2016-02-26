#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
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