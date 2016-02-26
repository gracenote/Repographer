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