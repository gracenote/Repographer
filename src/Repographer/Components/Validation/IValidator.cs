using System.Collections.Generic;

namespace Repographer.Components.Validation
{
    public interface IValidator<in T>
    {
        IEnumerable<ValidationCondition> Validate(T settings);
    }
}