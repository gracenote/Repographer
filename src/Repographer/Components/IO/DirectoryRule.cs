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