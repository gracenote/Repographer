using System.Collections.Generic;
using Repographer.Components.Validation;

namespace Repographer.Modules.Graph.Settings.Validators
{
    public class GraphExportSettingsValidator : IValidator<GraphExportSettings>
    {
        public  IEnumerable<ValidationCondition> Validate(GraphExportSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.Creator))
                yield return new ValidationCondition("Missing recommended argument: creator", ValidationConditionLevel.Warning);

            if (string.IsNullOrWhiteSpace(settings.Description))
                yield return new ValidationCondition("Missing recommended argument: description", ValidationConditionLevel.Warning);
        }
    }
}