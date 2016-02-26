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
using Repographer.Modules.Main.Settings.Builders.Selectors;
using Repographer.Mono.Options;

namespace Repographer.Modules.Graph.Settings.Builders
{
    public class GraphSettingsBuilder : ISettingsBuilder<RepoActionSettings>
    {
        private readonly ITrueCounter _trueCounter;
        private readonly IBuilderSelector<GraphSettings> _builderSelector;
        private readonly IValidator<GraphSettings> _validator;

        private string[] _args;
        private bool _help;

        public GraphSettingsBuilder(
            ITrueCounter trueCounter,
            IBuilderSelector<GraphSettings> builderSelector,
            IValidator<GraphSettings> validator)
        {
            _trueCounter = trueCounter;
            _builderSelector = builderSelector;
            _validator = validator;
        }

        public void SetArgs(string[] args)
        {
            _args = args;
        }

        public void SetHelp(bool help)
        {
            _help = help;
        }

        public IRepoActionSettings Build()
        {
            var options = new OptionSet();
            var settings = new GraphSettings(options);

            settings.Parse(_args);

            if (_help && !_trueCounter.Any(settings.Compile, settings.Export))
                return new ShowHelpSettings("Graph Options", options);

            _validator.Validate(settings).ThrowIfAny(options);

            var subBuilder = _builderSelector.GetBuilder(settings);

            subBuilder.SetArgs(_args);
            subBuilder.SetHelp(_help);
            
            return subBuilder.Build();
        }

        public bool IsBuilderFor(RepoActionSettings repoActionSettings)
        {
            return repoActionSettings.Graph;
        }
    }
}