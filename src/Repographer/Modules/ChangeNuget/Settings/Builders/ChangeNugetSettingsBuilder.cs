#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using Repographer.Components;
using Repographer.Components.Validation;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.Main.Settings.Builders;
using Repographer.Mono.Options;

namespace Repographer.Modules.ChangeNuget.Settings.Builders
{
    public class ChangeNugetSettingsBuilder : ISettingsBuilder<RepoActionSettings>
    {
        private readonly IValidator<ChangeNugetSettings> _validator;
        private bool _help;
        private string[] _args;

        public ChangeNugetSettingsBuilder(IValidator<ChangeNugetSettings> validator)
        {
            _validator = validator;
        }

        public void SetHelp(bool help)
        {
            _help = help;
        }

        public void SetArgs(string[] args)
        {
            _args = args;
        }

        public IRepoActionSettings Build()
        {
            var options = new OptionSet();

            var settings = new ChangeNugetSettings(options);
            settings.Parse(_args);

            if (_help)
                return new ShowHelpSettings("Change NuGet Package Location", options);

            _validator.Validate(settings).ThrowIfAny(options);

            return settings;
        }

        public bool IsBuilderFor(RepoActionSettings settings)
        {
            return settings.ChangeNuget;
        }
    }
}
