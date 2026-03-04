using System;

namespace WateryTart.MusicAssistant.Generators.Attributes;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
public sealed class AlsoNotifyForAttribute(string propertyName) : Attribute
{
    public string PropertyName { get; } = propertyName;
}