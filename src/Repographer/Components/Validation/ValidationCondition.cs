#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System;

namespace Repographer.Components.Validation
{
    public class ValidationCondition : IEquatable<ValidationCondition>
    {
        public string Message { get; private set; }

        public ValidationConditionLevel Level { get; private set; }

        public ValidationCondition(string message, ValidationConditionLevel level = ValidationConditionLevel.Info)
        {
            Message = message;
            Level = level;
        }

        public bool Equals(ValidationCondition other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Message, other.Message) && Level == other.Level;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ValidationCondition)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Message != null ? Message.GetHashCode() : 0) * 397) ^ (int)Level;
            }
        }

        public static bool operator ==(ValidationCondition left, ValidationCondition right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValidationCondition left, ValidationCondition right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Level, Message);
        }
    }
}