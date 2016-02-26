#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
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
