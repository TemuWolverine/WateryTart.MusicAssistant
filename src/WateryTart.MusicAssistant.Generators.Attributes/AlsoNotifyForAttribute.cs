using System;

namespace WateryTart.MusicAssistant.Generators.Attributes;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
public sealed class AlsoNotifyForAttribute : Attribute
{
    public string PropertyName { get; }
    public AlsoNotifyForAttribute(string propertyName) => PropertyName = propertyName;
}