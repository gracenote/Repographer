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