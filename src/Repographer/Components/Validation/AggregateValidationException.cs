#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repographer.Mono.Options;

namespace Repographer.Components.Validation
{
    public class AggregateValidationException : Exception
    {
        private readonly List<ValidationCondition> _conditions = new List<ValidationCondition>();
        
        public OptionSet Set { get; private set; }

        public IEnumerable<ValidationCondition> Conditions { get { return _conditions; } }

        public AggregateValidationException() : base("One or more validation errors occurred")
        {
        }

        public AggregateValidationException(IEnumerable<ValidationCondition> conditions, OptionSet set) : base("One or more validation errors occurred")
        {
            Set = set;
            _conditions = conditions.ToList();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Message);

            _conditions.ForEach(c =>
            {
                sb.AppendLine(string.Format("{0}: {1}", c.Level, c.Message));
            });

            return sb.ToString();
        }
    }
}
