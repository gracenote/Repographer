#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
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