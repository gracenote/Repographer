#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System;

namespace Repographer.Components.IO
{
    public class DirectoryRule
    {
        public string Name { get; private set; }

        public Action<string> Execute { get; private set; }

        public DirectoryRule(string name, Action<string> execute)
        {
            Name = name;
            Execute = execute;
        }
    }
}