#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using Repographer.Components;

namespace Repographer.Modules.Main
{
    public class ShowHelpAction : IRepoAction
    {
        private readonly ITraceWriter _traceWriter;
        private readonly string _message;
        private readonly string _arguments;

        public ShowHelpAction(
            ITraceWriter traceWriter,
            string message,
            string arguments
            )
        {
            _traceWriter = traceWriter;
            _message = message;
            _arguments = arguments;
        }

        public void Execute()
        {
            _traceWriter.Output(_message);
            _traceWriter.Output(_arguments);
        }
    }
}