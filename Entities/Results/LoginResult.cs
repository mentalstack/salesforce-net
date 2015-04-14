namespace SalesforceNet.Entities
{
    /// <summary>
    /// Login result.
    /// </summary>
    public class LoginResult
    {
        #region Private Fields

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets instance url.
        /// </summary>
        public string InstanceUrl { get; set; }

        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        public string UserId { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LoginResult() { }

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public LoginResult(string id)
        {
            UserId = id;
        }

        #endregion
    }
}
