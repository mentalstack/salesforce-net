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
    /// RFC1123 converter.
    /// </summary>
    public class RfcDateTimeConverter : IsoDateTimeConverter
    {
        #region Defines

        /// <summary>
        /// RFC1123 date time format.
        /// </summary>
        public const string RFC1123 = "R";

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RfcDateTimeConverter()
        {
            DateTimeFormat = RFC1123; DateTimeStyles = DateTimeStyles.None;
        }

        #endregion
    }

    /// <summary>
    /// Serialization contract resolver.
    /// </summary>
    public class SerializationContractResolver : CamelCasePropertyNamesContractResolver
    {
        #region Protected Methods

        /// <summary>
        /// Creates property.
        /// </summary>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization serialization)
        {
            JsonProperty result = null;

            result = base.CreateProperty(member, serialization);

            if (member.GetCustomAttribute<IgnoreCreateUpdateAttribute>() != null)
            {
                return null; // null if ignore for create and update
            }

            return result;
        }

        #endregion
    }

    /// <summary>
    /// Deserialization contract resolver.
    /// </summary>
    public class DeserializationContractResolver : CamelCasePropertyNamesContractResolver
    {
        #region Protected Methods

        /// <summary>
        /// Creates property.
        /// </summary>
        protected override JsonProperty CreateProperty(MemberInfo memberInfo, MemberSerialization s)
        {
            return base.CreateProperty(memberInfo, s);
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
                ContractResolver = new SerializationContractResolver(), NullValueHandling = NullValueHandling.Include
            };

            settings.Converters.Add(new RfcDateTimeConverter());

            return JsonConvert.SerializeObject
                (
                    obj, settings
                );
        }

        /// <summary>
        /// Serialize object to JSON
        /// </summary>
        public T Deserialize<T>(IRestResponse response)
        {
            string content = response.Content;

            var settings = new JsonSerializerSettings // define de-serializer settings
            {
                ContractResolver = new DeserializationContractResolver(), NullValueHandling = NullValueHandling.Include
            };

            settings.Converters.Add(new RfcDateTimeConverter());

            return JsonConvert.DeserializeObject<T>
                (
                    content, settings
                );
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