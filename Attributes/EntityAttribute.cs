namespace SalesforceNet.Attributes
{
    using System;

    /// <summary>
    /// Entity attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityAttribute : Attribute
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
