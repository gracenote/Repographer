﻿using Autofac;
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