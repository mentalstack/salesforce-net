namespace SalesforceNet.Attributes
{
    using System;

    /// <summary>
    /// Ignore get attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreGetAttribute : IgnoreAttribute { }
}
