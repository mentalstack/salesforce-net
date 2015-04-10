namespace SalesforceNet.Entities
{
    using SalesforceNet;
    using SalesforceNet.Attributes;

    using System;

    /// <summary>
    /// Entity base class.
    /// </summary>
    public class BaseEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [IgnoreCreateUpdate]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets system modstamp.
        /// </summary>
        [IgnoreCreateUpdate]
        public DateTime SystemModstamp { get; set; }

        /// <summary>
        /// Gets or sets last modified by user id.
        /// </summary>
        [IgnoreCreateUpdate]
        public string LastModifiedById { get; set; }

        /// <summary>
        /// Gets or sets last modified date.
        /// </summary>
        [IgnoreCreateUpdate]
        public DateTime LastModifiedDate { get; set; }
        
        /// <summary>
        /// Gets or sets created by user id.
        /// </summary>
        [IgnoreCreateUpdate]
        public string CreatedById { get; set; }

        /// <summary>
        /// Gets or sets created date.
        /// </summary>
        [IgnoreCreateUpdate]
        public DateTime CreatedDate { get; set; }

        #endregion
    }
}
