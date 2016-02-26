#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System.Collections.Generic;
using System.Linq;
using Repographer.Components;
using Repographer.Components.Validation;

namespace Repographer.Modules.Main.Settings.Validators
{
    public class RepoActionSettingsValidator : IValidator<RepoActionSettings>
    {
        private readonly ITrueCounter _trueCounter;

        public RepoActionSettingsValidator(ITrueCounter trueCounter)
        {
            _trueCounter = trueCounter;
        }

        public IEnumerable<ValidationCondition> Validate(RepoActionSettings settings)
        {
            var trueCount = _trueCounter.GetCount(settings.ActionSwitches.ToArray());

            if (!settings.Help && trueCount == 0)
                yield return new ValidationCondition("No action specified", ValidationConditionLevel.Error);

            if (trueCount > 1)
                yield return new ValidationCondition("Too many actions specified", ValidationConditionLevel.Error);
        }
    }
}