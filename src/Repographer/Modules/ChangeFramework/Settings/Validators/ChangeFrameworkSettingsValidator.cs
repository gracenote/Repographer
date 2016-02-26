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

namespace Repographer.Modules.ChangeFramework.Settings.Validators
{
    public class ChangeFrameworkSettingsValidator : IValidator<ChangeFrameworkSettings>
    {
        public IEnumerable<ValidationCondition> Validate(ChangeFrameworkSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.Directory))
                yield return new ValidationCondition("Missing required argument: directory", ValidationConditionLevel.Error);

            if (string.IsNullOrWhiteSpace(settings.TargetFramework))
                yield return new ValidationCondition("Missing required argument: new path", ValidationConditionLevel.Error);

            var supportedFrameworks = new[] {"v4.5"};

            if (!supportedFrameworks.Contains(settings.TargetFramework))
                yield return new ValidationCondition("Invalid argument value: target framework", ValidationConditionLevel.Error);
        }
    }
}