using System;
using System.IO;
using Repographer.Modules.Main.Factories;
using Repographer.Mono.Options;

namespace Repographer.Modules.Main.Settings
{
    public class ShowHelpSettings : IRepoActionSettings
    {
        public string Message { get; private set; }
        public string Arguments { get; private set; }

        public OptionSet Set { get; private set; }

        public ShowHelpSettings(string message, OptionSet set)
        {
            Message = message;

            using (var writer = new StringWriter())
            {
                set.WriteOptionDescriptions(writer);
                Arguments = writer.ToString();

                writer.Close();
            }

            Set = set;
        }

        public IRepoAction CreateAction(IRepoActionFactory factory)
        {
            var repoActionFactory = factory as IRepoActionFactory<ShowHelpSettings>;

            if (repoActionFactory == null)
                throw new InvalidOperationException("An incompatible factory was provided for these settings");

            return repoActionFactory.Create(this);
        }
    }
}
