namespace SalesforceNet.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Id query result.
    /// </summary>
    public class IdResult
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion
    }

    /// <summary>
    /// Common query result.
    /// </summary>
    public class QueryResult<T> where T : BaseEntity
    {
        #region Private Fields

        /// <summary>
        ///
        /// </summary>
        private List<T> _records = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public List<T> Records
        {
            get { return _records ?? (_records = new List<T>()); }

            set { _records = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TotalSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NextRecordsUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Done { get; set; }

        #endregion

        #region Constructors

        #endregion
    }
}
