using System;
using Repographer.Components.IO.Configuration;

namespace Repographer.Components.IO
{
    public interface IDirectoryWalkerFactory
    {
        DirectoryWalker New(Action<DirectoryWalkerConfigurator> configure);
    }
}