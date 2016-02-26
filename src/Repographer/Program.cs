#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System;
using Autofac;
using Repographer.Modules.ChangeFramework;
using Repographer.Modules.ChangeNuget;
using Repographer.Modules.Graph;
using Repographer.Modules.Main;
using Repographer.Modules.Main.Factories.Selectors;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.PurgeFolder;
using Repographer.Modules.RemoveProject;
using Repographer.Modules.RemoveReference;
using Repographer.Modules.SwitchToNuget;

namespace Repographer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var container = BuildContainer();

            using (var scope = container.BeginLifetimeScope())
            {
                var settingsFactory = scope.Resolve<IRepoActionSettingsFactory>();
                var actionFactorySelector = scope.Resolve<IRepoActionFactorySelector>();

                var settings = settingsFactory.Create(args);
                var actionFactory = actionFactorySelector.GetFactory(settings);

                var action = settings.CreateAction(actionFactory);
                
                action.Execute();
            }
        }

        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new MainModule());
            builder.RegisterModule(new GraphModule());
            builder.RegisterModule(new PurgeFolderModule());
            builder.RegisterModule(new SwitchToNugetModule());
            builder.RegisterModule(new ChangeNugetModule());
            builder.RegisterModule(new RemoveProjectModule());
            builder.RegisterModule(new RemoveReferenceModule());
            builder.RegisterModule(new ChangeFrameworkModule());

            return builder.Build();
        }
    }
}
