namespace SalesforceNet
{
    using Newtonsoft;
    using Newtonsoft.Json;

    using RestSharp;
    using RestSharp.Authenticators;

    using SalesforceNet.Attributes;
    using SalesforceNet.Infrastructure;
    using SalesforceNet.Entities;

    using System.Collections;
    using System.Collections.Generic;

    using System;
    using System.Linq;
    using System.Configuration;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// OAuth token response.
    /// </summary>
    class TokenResponse
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets access token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets signature.
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// Gets or sets instance URL.
        /// </summary>
        public string InstanceUrl { get; set; }

        /// <summary>
        /// Gets or sets refresh token.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets issued at.
        /// </summary>
        public string IssuedAt { get; set; }

        #endregion
    }

    /// <summary>
    /// Identity service response.
    /// </summary>
    class IdentityServiceResponse
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets URLs.
        /// </summary>
        public Dictionary<string, string> Urls { get; set; }

        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets organization id.
        /// </summary>
        public string OrganizationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        #endregion
    }

    /// <summary>
    /// Salesforce gateway implementation.
    /// </summary>
    public class SalesforceGateway : ISalesforceGateway
    {
        #region Defines

        private const string ID_FIELD_NAME = "Id";

        private const string FLOW_PASSWORD = "password";
        private const string FLOW_AUTHORIZATION_CODE = "authorization_code";
        private const string FLOW_TOKEN = "refresh_token";

        private const string SELECT_FORMAT = "SELECT {0} FROM {1}";
        private const string SELECT_ID_FORMAT = "SELECT {0} FROM {1} WHERE Id = '{2}'";
        private const string SELECT_LIMIT_OFFSET_FORMAT = "SELECT {0} FROM {1} {2} {3} LIMIT {4} OFFSET {5}";
        private const string SELECT_WHERE_FORMAT = "SELECT {0} FROM {1} {2}";

        private const string VERSION = "v36.0";

        #endregion

        #region Private Properties

        /// <summary>
        /// Gets or sets client.
        /// </summary>
        private RestClient RestClient { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Extracts entity name.
        /// </summary>
        public string ExtractEntityName(Type type)
        {
            var attribute = type.GetCustomAttribute<JsonObjectAttribute>();
            {
                return (attribute != null) ? attribute.Id : type.Name; // rename if required
            }
        }

        /// <summary>
        /// Recursively extracts properties names.
        /// </summary>
        public List<string> ExtractEntityFields(Type type, string prefix = null, bool stop = false)
        {
            var result = new List<string>();

            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                string propertyName = null;

                var attribute = property.GetCustomAttribute<JsonPropertyAttribute>();
                {
                    propertyName = attribute != null ? attribute.PropertyName : property.Name;
                }

                if (property.GetCustomAttribute<IgnoreGetAttribute>() != null) // check ignore get property
                {
                    continue;
                }
                else if (property.GetCustomAttribute<ExtractRecursivelyAttribute>() != null) // check extract recursively
                {
                    if (stop) continue; // skip property

                    bool loop = (type == property.PropertyType); // check is loop reference

                    if (!String.IsNullOrEmpty(prefix)) // check prefix
                    {
                        result.AddRange(ExtractEntityFields(property.PropertyType, String.Format("{0}.{1}", prefix, propertyName), stop: loop));
                    }
                    else // if no type prefix - just add name to list, no prefix
                    {
                        result.AddRange(ExtractEntityFields(property.PropertyType, propertyName, stop: loop));
                    }
                }
                else // none of them
                {
                    if (!String.IsNullOrEmpty(prefix))
                    {
                        result.Add(String.Format("{0}.{1}", prefix, propertyName));
                    }
                    else // if no type prefix - just add name to list
                    {
                        result.Add(propertyName);
                    }
                }
            }

            return result;
        }

        #endregion

        #region Private Methods : Event Handlers

        /// <summary>
        /// Processes response errors.
        /// </summary>
        private void OnBeforeDeserialization(IRestResponse response)
        {
            if (!CheckResponse((int)response.StatusCode))
            {
                throw new InvalidOperationException(response.ErrorMessage);
            }
        }

        #endregion

        #region Private Methods : Request

        /// <summary>
        /// Checks response status.
        /// </summary>
        private bool CheckResponse(int statusCode)
        {
            return (statusCode >= 200 && statusCode <= 299);
        }

        /// <summary>
        /// Gets new request.
        /// </summary>
        private RestRequest BuildRequest(Method method, string[] segments, Dictionary<string, string> parameters = null, object body = null)
        {
            RestRequest result = null;

            string prefix = String.Format("services/data/{0}", VERSION);

            var resource = new StringBuilder(prefix);
            {
                for (int i = 0; i < segments.Length; i++) resource.AppendFormat("/{0}", segments[i]);
            }

            result = new RestRequest(resource.ToString(), method)
            {
                RequestFormat = DataFormat.Json, JsonSerializer = new JsonNetSerializer(), Timeout = 60000
            };

            result.OnBeforeDeserialization = OnBeforeDeserialization;

            if (parameters != null && parameters.Count > 0)
            {
                foreach (var p in parameters) result.AddParameter(p.Key, p.Value);
            }

            if (body != null) result.AddBody(body);
            {
                return result;
            }
        }

        /// <summary>
        /// Gets new request.
        /// </summary>
        private RestRequest BuildRequest(Method method, string resource, object body = null)
        {
            RestRequest result = null;

            result = new RestRequest(resource, method)
            {
                RequestFormat = DataFormat.Json, JsonSerializer = new JsonNetSerializer(), Timeout = 60000
            };

            result.OnBeforeDeserialization = OnBeforeDeserialization;

            if (body != null) result.AddBody(body);
            {
                return result;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Binds current gateway instance.
        /// </summary>
        public void Bind(string token, string instanceUrl)
        {
            RestClient.BaseUrl = new Uri(instanceUrl);

            var authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token);
            {
                RestClient.Authenticator = authenticator;
            }
        }

        /// <summary>
        /// Logs in current gateway instance.
        /// </summary>
        public LoginResult Login(string username, string password, string token)
        {
            RestClient client = null;
            {
                client = new RestClient(ConfigurationManager.AppSettings["SalesforceBaseUrl"]);
            }

            var request = new RestRequest(Method.POST)
            {
                Resource = ConfigurationManager.AppSettings["SalesforceTokenResource"], RequestFormat = DataFormat.Json
            };

            IdentityServiceResponse identityResponse = null; TokenResponse response = null;

            request.AddParameter("grant_type",    FLOW_PASSWORD);

            request.AddParameter("password",      String.Format("{0}{1}", password, token));

            request.AddParameter("client_id",     ConfigurationManager.AppSettings["SalesforceClientId"]);
            request.AddParameter("client_secret", ConfigurationManager.AppSettings["SalesforceClientSecret"]);

            request.AddParameter("username",      username);

            if ((response = client.Execute<TokenResponse>(request).Data) == null)
            {
                throw new Exception(String.Format("Salesforce token obtain failed, username: {0}", username));
            }

            var bearer = new OAuth2AuthorizationRequestHeaderAuthenticator(response.AccessToken);
            {
                RestClient.BaseUrl = new Uri(response.InstanceUrl); RestClient.Authenticator = bearer;
            }

            client = new RestClient(response.Id); client.Authenticator = bearer;

            var identityRequest = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };

            if ((identityResponse = client.Execute<IdentityServiceResponse>(identityRequest).Data) == null)
            {
                throw new InvalidOperationException(String.Format("Salesforce identity failed, id: {0}", response.Id));
            }

            var result = new LoginResult(identityResponse.UserId)
            {
                Token = response.AccessToken, InstanceUrl = response.InstanceUrl
            };

            return result;
        }

        /// <summary>
        /// Logs in current gateway instance.
        /// </summary>
        public LoginResult Login(string code, out string refreshToken)
        {
            RestClient client = null;
            {
                client = new RestClient(ConfigurationManager.AppSettings["SalesforceBaseUrl"]);
            }

            var request = new RestRequest(Method.POST)
            {
                Resource = ConfigurationManager.AppSettings["SalesforceTokenResource"], RequestFormat = DataFormat.Json
            };

            IdentityServiceResponse identityResponse = null; TokenResponse response = null;

            request.AddParameter("grant_type",    FLOW_AUTHORIZATION_CODE);

            request.AddParameter("redirect_uri",  ConfigurationManager.AppSettings["SalesforceRedirectUrl"]);
            request.AddParameter("client_id",     ConfigurationManager.AppSettings["SalesforceClientId"]);
            request.AddParameter("client_secret", ConfigurationManager.AppSettings["SalesforceClientSecret"]);

            request.AddParameter("code",          code);

            if ((response = client.Execute<TokenResponse>(request).Data) == null)
            {
                throw new Exception(String.Format("Salesforce token obtain failed, authorization code: {0}", code));
            }

            var bearer = new OAuth2AuthorizationRequestHeaderAuthenticator(response.AccessToken);
            {
                RestClient.BaseUrl = new Uri(response.InstanceUrl); RestClient.Authenticator = bearer;
            }

            client = new RestClient(response.Id); client.Authenticator = bearer;

            var identityRequest = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };

            if ((identityResponse = client.Execute<IdentityServiceResponse>(identityRequest).Data) == null)
            {
                throw new InvalidOperationException(String.Format("Salesforce identity failed, id: {0}", response.Id));
            }

            var result = new LoginResult(identityResponse.UserId)
            {
                Token = response.AccessToken, InstanceUrl = response.InstanceUrl
            };

            refreshToken = response.RefreshToken;
            {
                return result;
            }
        }

        /// <summary>
        /// Logs in current gateway instance.
        /// </summary>
        public LoginResult Login(string refreshToken)
        {
            RestClient client = null;
            {
                client = new RestClient(ConfigurationManager.AppSettings["SalesforceBaseUrl"]);
            }

            var request = new RestRequest(Method.POST)
            {
                Resource = ConfigurationManager.AppSettings["SalesforceTokenResource"], RequestFormat = DataFormat.Json
            };

            IdentityServiceResponse identityResponse = null; TokenResponse response = null;

            request.AddParameter("grant_type",    FLOW_TOKEN);

            request.AddParameter("client_id",     ConfigurationManager.AppSettings["SalesforceClientId"]);
            request.AddParameter("client_secret", ConfigurationManager.AppSettings["SalesforceClientSecret"]);

            request.AddParameter("refresh_token", refreshToken);

            if ((response = client.Execute<TokenResponse>(request).Data) == null)
            {
                throw new Exception(String.Format("Salesforce token obtain failed, token: {0}", refreshToken));
            }

            var bearer = new OAuth2AuthorizationRequestHeaderAuthenticator(response.AccessToken);
            {
                RestClient.BaseUrl = new Uri(response.InstanceUrl); RestClient.Authenticator = bearer;
            }

            client = new RestClient(response.Id); client.Authenticator = bearer;

            var identityRequest = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };

            if ((identityResponse = client.Execute<IdentityServiceResponse>(identityRequest).Data) == null)
            {
                throw new InvalidOperationException(String.Format("Salesforce identity failed, id: {0}", response.Id));
            }

            var result = new LoginResult(identityResponse.UserId)
            {
                Token = response.AccessToken, InstanceUrl = response.InstanceUrl
            };

            return result;
        }

        /// <summary>
        /// Describes global.
        /// </summary>
        public DescribeGlobalResult Global()
        {
            return Execute<DescribeGlobalResult>(Method.GET, new[] { "sobjects" });
        }

        /// <summary>
        /// Describes object.
        /// </summary>
        public DescribeResult DescribeObject(string typeName)
        {
            return Execute<DescribeResult>(Method.GET, new[] { "sobjects", typeName, "describe" });
        }

        /// <summary>
        /// Gets single record.
        /// </summary>
        public T Get<T>(string id) where T : BaseEntity
        {
            return Query<T>(id, ExtractEntityName(typeof(T)));
        }

        /// <summary>
        /// Gets collection of records.
        /// </summary>
        public List<T> GetMany<T>() where T : BaseEntity
        {
            string joined = String.Join(", ", ExtractEntityFields(typeof(T), null));

            string soql = String.Format(SELECT_FORMAT, joined, ExtractEntityName(typeof(T)));
            {
                return QueryMany<T>(soql); // make a query
            }
        }

        /// <summary>
        /// Gets collection of records.
        /// </summary>
        public List<T> GetMany<T>(string where) where T : BaseEntity
        {
            string joined = String.Join(", ", ExtractEntityFields(typeof(T), null));

            string soql = String.Format(SELECT_WHERE_FORMAT, joined, ExtractEntityName(typeof(T)), where);
            {
                return QueryMany<T>(soql); // make a query
            }
        }

        /// <summary>
        /// Gets collection of records.
        /// </summary>
        public List<T> GetMany<T>(int limit, int offset, string where) where T : BaseEntity
        {
            string joined = String.Join(", ", ExtractEntityFields(typeof(T), null));

            string soql = String.Format(SELECT_LIMIT_OFFSET_FORMAT, joined, ExtractEntityName(typeof(T)), where, limit, offset);
            {
                return QueryMany<T>(soql); // make a query
            }
        }

        /// <summary>
        /// Creates new record.
        /// </summary>
        public T Create<T>(T source) where T : BaseEntity
        {
            string name = ExtractEntityName(typeof(T));

            var qr = Execute<CreateResult>(Method.POST, new[] { "sobjects", name }, body: source);
            {
                source.Id = qr.Id; // reestablish id
            }

            return source;
        }

        /// <summary>
        /// Updates record.
        /// </summary>
        public void Update<T>(string id, T source) where T : BaseEntity
        {
            Execute(Method.PATCH, new[] { "sobjects", ExtractEntityName(typeof(T)), id }, body: source);
        }

        /// <summary>
        /// Deletes record.
        /// </summary>
        public void Delete<T>(string id) where T : BaseEntity
        {
            Execute(Method.DELETE, new[] { "sobjects", ExtractEntityName(typeof(T)), id });
        }

        #endregion

        #region Public Methods : IDisposible Implementation

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true); GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes current instance resources.
        /// </summary>
        protected virtual void Dispose(bool disposing = true)
        {
            /* disconnect from salesforce */ if (disposing) RestClient = null;
        }

        /// <summary>
        /// Default destructor.
        /// </summary>
        ~SalesforceGateway()
        {
            Dispose(false);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Executes request.
        /// </summary>
        protected void Execute(Method method, string[] segments, Dictionary<string, string> parameters = null, object body = null)
        {
            RestClient.Execute(BuildRequest(method, segments, parameters, body));
        }

        /// <summary>
        /// Executes request.
        /// </summary>
        protected T Execute<T>(Method method, string[] segments, Dictionary<string, string> parameters = null, object body = null) where T : new()
        {
            return RestClient.Execute<T>(BuildRequest(method, segments, parameters, body)).Data;
        }

        /// <summary>
        /// Executes request.
        /// </summary>
        protected T Execute<T>(Method method, string resource, object body = null) where T : new()
        {
            return RestClient.Execute<T>(BuildRequest(method, resource, body)).Data;
        }

        #endregion

        #region Protected Methods : Query

        /// <summary>
        /// Gets single record.
        /// </summary>
        protected T Query<T>(string name) where T : BaseEntity
        {
            var parameters = new Dictionary<string, string>
            {
                { "q", String.Format(SELECT_FORMAT, String.Join(", ", ExtractEntityFields(typeof(T), null)), name) }
            };

            var result = Execute<QueryResult<T>>(Method.GET, new[] { "query" }, parameters);
            {
                return result.Records.FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets single record.
        /// </summary>
        protected T Query<T>(string id, string name) where T : BaseEntity
        {
            var parameters = new Dictionary<string, string> // define parameters including id
            {
                { "q", String.Format(SELECT_ID_FORMAT, String.Join(", ", ExtractEntityFields(typeof(T), null)), name, id) }
            };

            var result = Execute<QueryResult<T>>(Method.GET, new[] { "query" }, parameters);
            {
                return result.Records.FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets collection of records.
        /// </summary>
        protected List<T> QueryMany<T>(string soql) where T : BaseEntity
        {
            var result = new List<T>();

            var parameters = new Dictionary<string, string>
            {
                { "q", soql }
            };

            var qr = Execute<QueryResult<T>>(Method.GET, new[] { "query" }, parameters);
            {
                result.AddRange(qr.Records);
            }

            while (!String.IsNullOrEmpty(qr.NextRecordsUrl))
            {
                qr = Execute<QueryResult<T>>(Method.GET, qr.NextRecordsUrl);
                {
                    result.AddRange(qr.Records);
                }
            }

            return result;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public SalesforceGateway()
        {
            RestClient = new RestClient();
            {
                RestClient.AddHandler("application/json", new JsonNetSerializer());
            }
        }

        #endregion
    }
}