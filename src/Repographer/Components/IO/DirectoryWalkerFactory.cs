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