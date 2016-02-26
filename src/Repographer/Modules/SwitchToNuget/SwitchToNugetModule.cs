#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using Autofac;
using Repographer.Components.Validation;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.Main.Settings.Builders;
using Repographer.Modules.SwitchToNuget.Factories;
using Repographer.Modules.SwitchToNuget.Settings;
using Repographer.Modules.SwitchToNuget.Settings.Builders;
using Repographer.Modules.SwitchToNuget.Settings.Validators;

namespace Repographer.Modules.SwitchToNuget
{
    public class SwitchToNugetModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            LoadSettingsBuilders(builder);
            LoadSettingsValidators(builder);
            LoadActionFactories(builder);
        }

        private void LoadSettingsBuilders(ContainerBuilder builder)
        {
            builder
                .RegisterType<SwitchToNugetSettingsBuilder>()
                .As<ISettingsBuilder<RepoActionSettings>>();
        }

        private void LoadSettingsValidators(ContainerBuilder builder)
        {
            builder
                .RegisterType<SwitchToNugetSettingsValidator>()
                .As<IValidator<SwitchToNugetSettings>>();
        }

        private void LoadActionFactories(ContainerBuilder builder)
        {
            builder
                .RegisterType<SwitchToNugetActionFactory>()
                .As<IRepoActionFactory<SwitchToNugetSettings>>()
                .As<IRepoActionFactory>();
        }
    }
}
