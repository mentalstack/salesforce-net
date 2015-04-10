namespace SalesforceNet.Infrastructure
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Newtonsoft.Json.Converters;
    
    using RestSharp;
    using RestSharp.Deserializers;
    using RestSharp.Serializers;

    using SalesforceNet.Attributes;

    using System.Linq;
    using System.Globalization;
    using System.Reflection;

    /// <summary>
    /// Salesforce contract resolver.
    /// </summary>
    public class SalesforceContractResolver : CamelCasePropertyNamesContractResolver
    {
        #region Protected Methods

        /// <summary>
        /// Creates property.
        /// </summary>
        protected override JsonProperty CreateProperty(MemberInfo memberInfo, MemberSerialization s)
        {
            IgnoreAttribute ignore = null;

            var result = base.CreateProperty(memberInfo, s);

            object[] attributes = memberInfo.GetCustomAttributes(typeof(IgnoreAttribute), true);
            {
                ignore = (attributes.FirstOrDefault() as IgnoreAttribute); if (ignore == null) return result;
            }

            if (ignore is IgnoreCreateUpdateAttribute)
            {
                return null; // null if ignore for create and update
            }

            return result;
        }

        #endregion
    }

    /// <summary>
    /// Json.NET serializer.
    /// </summary>
    public class JsonNetSerializer : ISerializer, IDeserializer
    {
        #region Defines

        /// <summary>
        /// RFC1123 date time format.
        /// </summary>
        public const string RFC1123 = "R";

        #endregion

        #region Private Properties

        /// <summary>
        /// Gets or sets JSON settings instance.
        /// </summary>
        private JsonSerializerSettings Settings { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets date format.
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// Gets or sets root element.
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        /// Gets or sets namespace.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets content type.
        /// </summary>
        public string ContentType { get; set; }

        #endregion

        #region Public Methods : Serialization

        /// <summary>
        /// Serialize object to JSON
        /// </summary>
        public string Serialize(object obj)
        {
            var settings = new JsonSerializerSettings // define serializer settings
            {
                ContractResolver = new SalesforceContractResolver(), NullValueHandling = NullValueHandling.Include
            };

            settings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = DateFormat ?? RFC1123, DateTimeStyles = DateTimeStyles.None
            });

            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// Serialize object to JSON
        /// </summary>
        public T Deserialize<T>(IRestResponse response)
        {
            string content = response.Content;

            var settings = new JsonSerializerSettings // define de-serializer settings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Include
            };

            settings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = DateFormat ?? RFC1123, DateTimeStyles = DateTimeStyles.None
            });

            return JsonConvert.DeserializeObject<T>(content, settings);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public JsonNetSerializer()
        {
            ContentType = "application/json";
        }

        #endregion
    }
}
