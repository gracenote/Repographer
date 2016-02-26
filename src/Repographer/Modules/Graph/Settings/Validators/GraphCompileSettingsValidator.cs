using System.Collections.Generic;
using Repographer.Components.Validation;

namespace Repographer.Modules.Graph.Settings.Validators
{
    public class GraphCompileSettingsValidator : IValidator<GraphCompileSettings>
    {
        public IEnumerable<ValidationCondition> Validate(GraphCompileSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.Directory))
                yield return new ValidationCondition("Missing required argument: directory", ValidationConditionLevel.Error);
        }
    }
}