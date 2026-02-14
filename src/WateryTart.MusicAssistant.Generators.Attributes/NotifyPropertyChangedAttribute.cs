using System;

namespace WateryTart.MusicAssistant.Generators.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class NotifyPropertyChangedAttribute : Attribute
{
}
