#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System;
using Repographer.Components.IO.Configuration;

namespace Repographer.Components.IO
{
    public class DirectoryWalkerFactory : IDirectoryWalkerFactory
    {
        public DirectoryWalker New(Action<DirectoryWalkerConfigurator> configure)
        {
            if (configure == null)
                throw new ArgumentNullException("configure");

            var configurator = new DirectoryWalkerConfiguratorImpl();

            configure(configurator);

            return configurator.BuildEngine();
        }
    }
}