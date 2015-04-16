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
        /// Describes specified object.
        /// </summary>
        DescribeResult DescribeObject(string name);

        /// <summary>
        /// Gets single record.
        /// </summary>
        T Get<T>(string id, string name) where T : BaseEntity;

        /// <summary>
        /// Gets single record.
        /// </summary>
        T Get<T>(string id) where T : BaseEntity;

        /// <summary>
        /// Gets collection of records.
        /// </summary>
        List<T> GetMany<T>(string soql) where T : BaseEntity;

        /// <summary>
        /// Gets collection of records.
        /// </summary>
        List<T> GetMany<T>(string name, string where, string order) where T : BaseEntity;

        /// <summary>
        /// Gets collection of records.
        /// </summary>
        List<T> GetMany<T>(string where, string order) where T : BaseEntity;

        /// <summary>
        /// Gets collection of records.
        /// </summary>
        List<T> GetMany<T>(string name, int limit, int offset, string where, string order) where T : BaseEntity;

        /// <summary>
        /// Gets collection of records.
        /// </summary>
        List<T> GetMany<T>(int limit, int offset, string where, string order) where T : BaseEntity;

        /// <summary>
        /// Updates record.
        /// </summary>
        void Update(string id, string name, object source);

        /// <summary>
        /// Updates record.
        /// </summary>
        void Update<T>(string id, T source) where T : BaseEntity;

        /// <summary>
        /// Creates new record.
        /// </summary>
        string Create(string name, object source);

        /// <summary>
        /// Creates new record.
        /// </summary>
        string Create<T>(T source) where T : BaseEntity;

        /// <summary>
        /// Deletes record.
        /// </summary>
        void Delete(string id, string name);

        /// <summary>
        /// Deletes record.
        /// </summary>
        void Delete<T>(string id) where T : BaseEntity;

        #endregion
    }
}
