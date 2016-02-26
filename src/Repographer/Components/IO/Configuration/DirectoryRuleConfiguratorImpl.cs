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
    public class DirectoryRuleConfiguratorImpl : DirectoryRuleConfigurator
    {
        private string _name;
        private Action<string> _execute;

        public void Name(string name)
        {
            _name = name;
        }

        public void Execute(Action<string> execute)
        {
            _execute = execute;
        }

        public DirectoryRule BuildRule()
        {
            return new DirectoryRule(_name, _execute);
        }
    }
}