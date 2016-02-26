using System;

namespace Repographer.Components.IO.Configuration
{
    public interface FileTypeRuleConfigurator
    {
        void Extension(string extension);
        void Execute(Action<string> execute);
    }
}