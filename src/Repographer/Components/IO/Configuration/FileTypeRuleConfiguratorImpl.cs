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
    public class FileTypeRuleConfiguratorImpl : FileTypeRuleConfigurator
    {
        private string _extension;
        private Action<string> _execute;

        public void Extension(string extension)
        {
            _extension = "*." + extension;
        }

        public void Execute(Action<string> execute)
        {
            _execute = execute;
        }

        public FileTypeRule BuildRule()
        {
            return new FileTypeRule(_extension, _execute);
        }
    }
}