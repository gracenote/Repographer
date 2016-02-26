using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repographer.Modules.Main.Settings;
using Repographer.Mono.Options;

namespace Repographer.Components
{
    public static class SettingsExtensions
    {
        public static void Parse(this IRepoActionSettings settings, string[] args)
        {
            settings.Set.Parse(args);
        }
    }
}
