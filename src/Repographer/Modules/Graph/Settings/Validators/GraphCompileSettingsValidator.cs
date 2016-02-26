#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
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