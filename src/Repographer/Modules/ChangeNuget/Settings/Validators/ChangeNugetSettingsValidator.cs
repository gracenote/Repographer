using System.Collections.Generic;
using Repographer.Components.Validation;

namespace Repographer.Modules.ChangeNuget.Settings.Validators
{
    public class ChangeNugetSettingsValidator : IValidator<ChangeNugetSettings>
    {
        public IEnumerable<ValidationCondition> Validate(ChangeNugetSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.Directory))
                yield return new ValidationCondition("Missing required argument: directory", ValidationConditionLevel.Error);

            if (string.IsNullOrWhiteSpace(settings.NewPath))
                yield return new ValidationCondition("Missing required argument: new path", ValidationConditionLevel.Error);
        }
    }
}