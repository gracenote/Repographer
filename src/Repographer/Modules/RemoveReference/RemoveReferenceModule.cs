﻿#region License
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
using Repographer.Modules.RemoveReference.Factories;
using Repographer.Modules.RemoveReference.Settings;
using Repographer.Modules.RemoveReference.Settings.Builders;
using Repographer.Modules.RemoveReference.Settings.Validators;

namespace Repographer.Modules.RemoveReference
{
    public class RemoveReferenceModule : Module
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
                .RegisterType<RemoveReferenceSettingsBuilder>()
                .As<ISettingsBuilder<RepoActionSettings>>();
        }

        private void LoadSettingsValidators(ContainerBuilder builder)
        {
            builder
                .RegisterType<RemoveReferenceSettingsValidator>()
                .As<IValidator<RemoveReferenceSettings>>();
        }

        private void LoadActionFactories(ContainerBuilder builder)
        {
            builder
                .RegisterType<RemoveReferenceActionFactory>()
                .As<IRepoActionFactory<RemoveReferenceSettings>>()
                .As<IRepoActionFactory>();
        }
    }
}
