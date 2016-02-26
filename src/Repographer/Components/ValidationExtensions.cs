using System.Collections.Generic;
using System.Linq;
using Repographer.Components.Validation;
using Repographer.Mono.Options;

namespace Repographer.Components
{
    public static class ValidationExtensions
    {
        public static void ThrowIfAny(this IEnumerable<ValidationCondition> conditions, OptionSet set)
        {
            var list = conditions.ToList();

            if (!list.Any())
                return;

            throw new AggregateValidationException(list, set);
        }
    }
}
