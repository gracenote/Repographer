#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System.Collections.Generic;
using System.Linq;
using Repographer.Components.Validation;

namespace Repographer.Modules.PurgeFolder.Settings.Validators
{
    public class PurgeFolderSettingsValidator : IValidator<PurgeFolderSettings>
    {
        public IEnumerable<ValidationCondition> Validate(PurgeFolderSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.Directory))
                yield return new ValidationCondition("Missing required argument: directory", ValidationConditionLevel.Error);

            if (!settings.Includes.Any())
                yield return new ValidationCondition("Missing required value(s): includes", ValidationConditionLevel.Error);
        }
    }
}