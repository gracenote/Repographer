#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using Autofac;
using Repographer.Components.Validation;
using Repographer.Modules.ChangeFramework.Factories;
using Repographer.Modules.ChangeFramework.Settings;
using Repographer.Modules.ChangeFramework.Settings.Builders;
using Repographer.Modules.ChangeFramework.Settings.Validators;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.Main.Settings.Builders;

namespace Repographer.Modules.ChangeFramework
{
    public class ChangeFrameworkModule : Module
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
                .RegisterType<ChangeFrameworkSettingsBuilder>()
                .As<ISettingsBuilder<RepoActionSettings>>();
        }

        private void LoadSettingsValidators(ContainerBuilder builder)
        {
            builder
                .RegisterType<ChangeFrameworkSettingsValidator>()
                .As<IValidator<ChangeFrameworkSettings>>();
        }

        private void LoadActionFactories(ContainerBuilder builder)
        {
            builder
                .RegisterType<ChangeFrameworkActionFactory>()
                .As<IRepoActionFactory<ChangeFrameworkSettings>>()
                .As<IRepoActionFactory>();
        }
    }
}
