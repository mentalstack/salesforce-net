namespace SalesforceNet
{
    using SalesforceNet.Entities;

    using System;

    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Salesforce gateway interface.
    /// </summary>
    public interface ISalesforceGateway : IDisposable
    {
        #region Methods

        /// <summary>
        /// Binds current gateway instance.
        /// </summary>
        void Bind(string token, string instanceUrl);

        /// <summary>
        /// Logs in current gateway instance.
        /// </summary>
        LoginResult Login(string username, string password, string token);

        /// <summary>
        /// Logs in current gateway instance.
        /// </summary>
        LoginResult Login(string code, out string refreshToken);

        /// <summary>
        /// Logs in current gateway instance.
        /// </summary>
        LoginResult Login(string refreshToken);

        /// <summary>
        /// Describes global.
        /// </summary>
        DescribeGlobalResult Global();

        /// <summary>
        /// Describes object.
        /// </summary>
        DescribeResult DescribeObject(string typeName);

        /// <summary>
        /// Gets single record.
        /// </summary>
        T Get<T>(string id) where T : BaseEntity;

        /// <summary>
        /// Gets collection of records.
        /// </summary>
        List<T> GetMany<T>() where T : BaseEntity;

        /// <summary>
        /// Gets collection of records.
        /// </summary>
        List<T> GetMany<T>(string where) where T : BaseEntity;

        /// <summary>
        /// Gets collection of records.
        /// </summary>
        List<T> GetMany<T>(int limit, int offset, string where) where T : BaseEntity;

        /// <summary>
        /// Creates new record.
        /// </summary>
        T Create<T>(T source) where T : BaseEntity;

        /// <summary>
        /// Updates record.
        /// </summary>
        void Update<T>(string id, T source) where T : BaseEntity;

        /// <summary>
        /// Deletes record.
        /// </summary>
        void Delete<T>(string id) where T : BaseEntity;

        #endregion
    }
}