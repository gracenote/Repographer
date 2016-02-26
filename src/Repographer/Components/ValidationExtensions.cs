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
