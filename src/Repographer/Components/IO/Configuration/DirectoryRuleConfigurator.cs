using System;

namespace Repographer.Components.IO.Configuration
{
    public interface DirectoryRuleConfigurator
    {
        void Name(string name);
        void Execute(Action<string> execute);
    }
}