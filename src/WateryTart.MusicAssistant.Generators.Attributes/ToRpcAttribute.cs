using System;

namespace WateryTart.MusicAssistant.Generators.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ToRpcAttribute : Attribute
    {
    }
}
