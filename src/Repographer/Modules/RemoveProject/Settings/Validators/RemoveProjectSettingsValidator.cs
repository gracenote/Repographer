using System.Collections.Generic;
using Repographer.Components.Validation;

namespace Repographer.Modules.RemoveProject.Settings.Validators
{
    public class RemoveProjectSettingsValidator : IValidator<RemoveProjectSettings>
    {
        public IEnumerable<ValidationCondition> Validate(RemoveProjectSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.Directory))
                yield return new ValidationCondition("Missing required argument: directory", ValidationConditionLevel.Error);

            if (string.IsNullOrWhiteSpace(settings.Name))
                yield return new ValidationCondition("Missing required argument: name", ValidationConditionLevel.Error);
        }
    }
}