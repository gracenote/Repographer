#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using Repographer.Components;
using Repographer.Modules.Main.Settings;

namespace Repographer.Modules.Main.Factories
{
    public class ShowHelpActionFactory : IRepoActionFactory<ShowHelpSettings>
    {
        private readonly ITraceWriter _traceWriter;

        public ShowHelpActionFactory(ITraceWriter traceWriter)
        {
            _traceWriter = traceWriter;
        }

        public IRepoAction Create(ShowHelpSettings repoActionSettings)
        {
            return new ShowHelpAction(_traceWriter, repoActionSettings.Message, repoActionSettings.Arguments);
        }

        public bool IsFactoryFor(IRepoActionSettings repoActionSettings)
        {
            return repoActionSettings.GetType() == typeof (ShowHelpSettings);
        }
    }
}