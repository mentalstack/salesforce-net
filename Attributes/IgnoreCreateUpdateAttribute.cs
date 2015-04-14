namespace SalesforceNet.Attributes
{
    using System;

    /// <summary>
    /// Ignore create and update attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreCreateUpdateAttribute : IgnoreAttribute { }
}
