#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
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