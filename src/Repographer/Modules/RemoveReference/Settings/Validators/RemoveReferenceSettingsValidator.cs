#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System.Collections.Generic;
using Repographer.Components;
using Repographer.Components.Validation;

namespace Repographer.Modules.RemoveReference.Settings.Validators
{
    public class RemoveReferenceSettingsValidator : IValidator<RemoveReferenceSettings>
    {
        private readonly ITrueCounter _trueCounter;

        public RemoveReferenceSettingsValidator(ITrueCounter trueCounter)
        {
            _trueCounter = trueCounter;
        }

        public IEnumerable<ValidationCondition> Validate(RemoveReferenceSettings settings)
        {
            var count = _trueCounter.GetCount(settings.Project, settings.Assembly);

            if (count == 0)
                yield return new ValidationCondition("Missing required argument: assembly or project", ValidationConditionLevel.Error);

            if (count > 1)
                yield return new ValidationCondition("Multiple arguments specified: assembly and project", ValidationConditionLevel.Error);

            if (string.IsNullOrWhiteSpace(settings.Directory))
                yield return new ValidationCondition("Missing required argument: directory", ValidationConditionLevel.Error);

            if (string.IsNullOrWhiteSpace(settings.ItemName))
                yield return new ValidationCondition("Missing required argument: item name", ValidationConditionLevel.Error);
        }
    }
}