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