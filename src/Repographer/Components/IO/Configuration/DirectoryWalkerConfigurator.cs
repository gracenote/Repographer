using System;

namespace Repographer.Components.IO.Configuration
{
    public interface DirectoryWalkerConfigurator
    {
        void Directory(string directory);
        void Exclude(string exclude);
        void FileRule(Action<FileTypeRuleConfigurator> configure);
        void DirectoryRule(Action<DirectoryRuleConfigurator> configure);
    }
}