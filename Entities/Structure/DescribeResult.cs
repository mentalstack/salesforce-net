namespace SalesforceNet.Entities
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Pick list.
    /// </summary>
    public class PickList
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets is default value.
        /// </summary>
        public bool DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets label.
        /// </summary>
        public string Label { get; set; }

        #endregion
    }

    /// <summary>
    /// Child relationship.
    /// </summary>
    public class ChildRelationship
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets field.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets child object.
        /// </summary>
        public string ChildSObject { get; set; }

        /// <summary>
        /// Gets or sets relationship name.
        /// </summary>
        public string RelationshipName { get; set; }

        /// <summary>
        /// Gets or sets is restricted delete.
        /// </summary>
        public bool RestrictedDelete { get; set; }

        /// <summary>
        /// Gets or sets is deprecated and hidden.
        /// </summary>
        public bool DeprecatedAndHidden { get; set; }

        /// <summary>
        /// Gets or sets is cascade delete.
        /// </summary>
        public bool CascadeDelete { get; set; }

        #endregion
    }

    /// <summary>
    /// Record type info layout url.
    /// </summary>
    public class RecordTypeInfoLayoutUrl
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets layout.
        /// </summary>
        public string Layout { get; set; }

        #endregion
    }

    /// <summary>
    /// Record type info.
    /// </summary>
    public class RecordTypeInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets record type id.
        /// </summary>
        public string RecordTypeId { get; set; }

        /// <summary>
        /// Gets or sets urls.
        /// </summary>
        public RecordTypeInfoLayoutUrl Urls { get; set; }

        /// <summary>
        /// Gets or sets is record type mapping.
        /// </summary>
        public bool DefaultRecordTypeMapping { get; set; }

        /// <summary>
        /// Gets or sets is available.
        /// </summary>
        public bool Available { get; set; }

        #endregion
    }

    /// <summary>
    /// Field.
    /// </summary>
    public class Field
    {
        #region Private Fields

        /// <summary>
        /// Picklist values.
        /// </summary>
        private List<PickList> _picklistValues = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets length.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets precision.
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// Gets or sets scale.
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// Gets or sets byte length.
        /// </summary>
        public int ByteLength { get; set; }

        /// <summary>
        /// Gets or sets digits.
        /// </summary>
        public int Digits { get; set; }

        /// <summary>
        /// Gets or sets picklist values.
        /// </summary>
        public List<PickList> PicklistValues
        {
            get { return _picklistValues ?? (_picklistValues = new List<PickList>()); }

            set { _picklistValues = value; }
        }

        /// <summary>
        /// Gets or sets type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets inline help text.
        /// </summary>
        public string InlineHelpText { get; set; }

        /// <summary>
        /// Gets or sets default value.
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets relationship name.
        /// </summary>
        public string RelationshipName { get; set; }

        /// <summary>
        /// Gets or sets controller name.
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Gets or sets label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets reference to.
        /// </summary>
        public List<string> ReferenceTo { get; set; }

        /// <summary>
        /// Gets or sets relationship order.
        /// </summary>
        public int? RelationshipOrder { get; set; }

        /// <summary>
        /// Gets or sets soap type.
        /// </summary>
        public string SoapType { get; set; }

        /// <summary>
        /// Gets or sets is auto number.
        /// </summary>
        public bool AutoNumber { get; set; }

        /// <summary>
        /// Gets or sets is unique.
        /// </summary>
        public bool Unique { get; set; }

        /// <summary>
        /// Gets or sets is calculated.
        /// </summary>
        public bool Calculated { get; set; }

        /// <summary>
        /// Gets or sets write requires master read.
        /// </summary>
        public bool WriteRequiresMasterRead { get; set; }

        /// <summary>
        /// Gets or sets is deprecated and hidden.
        /// </summary>
        public bool DeprecatedAndHidden { get; set; }

        /// <summary>
        /// Gets or sets is createable.
        /// </summary>
        public bool Createable { get; set; }

        /// <summary>
        /// Gets or sets is updateable.
        /// </summary>
        public bool Updateable { get; set; }

        /// <summary>
        /// Gets or sets is external id.
        /// </summary>
        public bool ExternalId { get; set; }

        /// <summary>
        /// Gets or sets is id lookup.
        /// </summary>
        public bool IdLookup { get; set; }

        /// <summary>
        /// Gets or sets is name field.
        /// </summary>
        public bool NameField { get; set; }

        /// <summary>
        /// Gets or sets is dortable.
        /// </summary>
        public bool Sortable { get; set; }

        /// <summary>
        /// Gets or sets is case sensitive.
        /// </summary>
        public bool CaseSensitive { get; set; }

        /// <summary>
        /// Gets or sets is filterable.
        /// </summary>
        public bool Filterable { get; set; }

        /// <summary>
        /// Gets or sets is restricted picklist.
        /// </summary>
        public bool RestrictedPicklist { get; set; }

        /// <summary>
        /// Gets or sets is nullable.
        /// </summary>
        public bool Nillable { get; set; }

        /// <summary>
        /// Gets or sets is display location in decimal.
        /// </summary>
        public bool DisplayLocationInDecimal { get; set; }

        /// <summary>
        /// Gets or sets is cascade delete.
        /// </summary>
        public bool CascadeDelete { get; set; }

        /// <summary>
        /// Gets or sets is restricted delete.
        /// </summary>
        public bool RestrictedDelete { get; set; }

        /// <summary>
        /// Gets or sets is defaulted on create.
        /// </summary>
        public bool DefaultedOnCreate { get; set; }

        /// <summary>
        /// Gets or sets is croupable.
        /// </summary>
        public bool Groupable { get; set; }

        /// <summary>
        /// Gets or sets is permissionable.
        /// </summary>
        public bool Permissionable { get; set; }

        /// <summary>
        /// Gets or sets is dependent picklist.
        /// </summary>
        public bool DependentPicklist { get; set; }

        /// <summary>
        /// Gets or sets is name pointing.
        /// </summary>
        public bool NamePointing { get; set; }

        /// <summary>
        /// Gets or sets is html formatted.
        /// </summary>
        public bool HtmlFormatted { get; set; }

        /// <summary>
        /// Gets or sets is custom.
        /// </summary>
        public bool Custom { get; set; }

        #endregion

        #region Constructors

        #endregion
    }

    /// <summary>
    /// Object urls.
    /// </summary>
    public class ObjectUrls
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets UI edit template.
        /// </summary>
        public string UiEditTemplate { get; set; }

        /// <summary>
        /// Gets or sets sobject.
        /// </summary>
        public string Sobject { get; set; }

        /// <summary>
        /// Gets or sets quick actions.
        /// </summary>
        public string QuickActions { get; set; }

        /// <summary>
        /// Gets or sets UI detail template.
        /// </summary>
        public string UiDetailTemplate { get; set; }

        /// <summary>
        /// Gets or sets describe.
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// Gets or sets row template.
        /// </summary>
        public string RowTemplate { get; set; }

        /// <summary>
        /// Gets or sets layouts.
        /// </summary>
        public string Layouts { get; set; }

        /// <summary>
        /// Gets or sets compact layouts.
        /// </summary>
        public string CompactLayouts { get; set; }

        /// <summary>
        /// Gets or sets UI new record.
        /// </summary>
        public string UiNewRecord { get; set; }

        #endregion
    }

    /// <summary>
    /// Describe global result.
    /// </summary>
    public class DescribeGlobalResult
    {
        #region Private Fields

        /// <summary>
        /// Sobjects list.
        /// </summary>
        private List<DescribeResult> _sObjects = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets current encoding.
        /// </summary>
        public string Encoding { get; set; }

        /// <summary>
        /// Gets or sets sobjects.
        /// </summary>
        public List<DescribeResult> sObjects
        {
            get { return _sObjects ?? (_sObjects = new List<DescribeResult>()); }

            set { _sObjects = value; }
        }

        /// <summary>
        /// Gets or sets max batch size.
        /// </summary>
        public int MaxBatchSize { get; set; }

        #endregion

        #region Constructors

        #endregion
    }

    /// <summary>
    /// Describe result.
    /// </summary>
    public class DescribeResult
    {
        #region Private Fields

        /// <summary>
        /// Fields list.
        /// </summary>
        private List<Field> _fields = null;

        /// <summary>
        /// Child relationships list.
        /// </summary>
        private List<ChildRelationship> _childRelationships = null;

        /// <summary>
        /// Type infos list.
        /// </summary>
        private List<RecordTypeInfo> _recordTypeInfos = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets fields.
        /// </summary>
        public List<Field> Fields
        {
            get { return _fields ?? (_fields = new List<Field>()); }

            set { _fields = value; }
        }

        /// <summary>
        /// Gets or sets child relationships.
        /// </summary>
        public List<ChildRelationship> ChildRelationships
        {
            get { return _childRelationships ?? (_childRelationships = new List<ChildRelationship>()); }

            set { _childRelationships = value; }
        }

        /// <summary>
        /// Gets or sets record type infos.
        /// </summary>
        public List<RecordTypeInfo> RecordTypeInfos
        {
            get { return _recordTypeInfos ?? (_recordTypeInfos = new List<RecordTypeInfo>()); }

            set { _recordTypeInfos = value; }
        }

        /// <summary>
        /// Gets or sets urls.
        /// </summary>
        public ObjectUrls Urls { get; set; }

        /// <summary>
        /// Gets or sets label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets plural.
        /// </summary>
        public string LabelPlural { get; set; }

        /// <summary>
        /// Gets or sets key prefix.
        /// </summary>
        public string KeyPrefix { get; set; }

        /// <summary>
        /// Gets or sets is custom setting.
        /// </summary>
        public bool CustomSetting { get; set; }

        /// <summary>
        /// Gets or sets is deprecated and hidden.
        /// </summary>
        public bool DeprecatedAndHidden { get; set; }

        /// <summary>
        /// Gets or sets is mergable.
        /// </summary>
        public bool Mergeable { get; set; }

        /// <summary>
        /// Gets or sets is replicateable.
        /// </summary>
        public bool Replicateable { get; set; }

        /// <summary>
        /// Gets or sets is triggerable.
        /// </summary>
        public bool Triggerable { get; set; }

        /// <summary>
        /// Gets or sets is feed enabled.
        /// </summary>
        public bool FeedEnabled { get; set; }

        /// <summary>
        /// Gets or sets is retrieveable.
        /// </summary>
        public bool Retrieveable { get; set; }

        /// <summary>
        /// Gets or sets is createable.
        /// </summary>
        public bool Createable { get; set; }

        /// <summary>
        /// Gets or sets is undeletable.
        /// </summary>
        public bool Undeletable { get; set; }

        /// <summary>
        /// Gets or sets is deletable.
        /// </summary>
        public bool Deletable { get; set; }

        /// <summary>
        /// Gets or sets is updateable.
        /// </summary>
        public bool Updateable { get; set; }

        /// <summary>
        /// Gets or sets is queryable.
        /// </summary>
        public bool Queryable { get; set; }

        /// <summary>
        /// Gets or sets is layoutable.
        /// </summary>
        public bool Layoutable { get; set; }

        /// <summary>
        /// Gets or sets is search layoutable.
        /// </summary>
        public bool SearchLayoutable { get; set; }

        /// <summary>
        /// Gets or sets is compact layoutable.
        /// </summary>
        public bool CompactLayoutable { get; set; }

        /// <summary>
        /// Gets or sets is searchable.
        /// </summary>
        public bool Searchable { get; set; }

        /// <summary>
        /// Gets or sets is activateable.
        /// </summary>
        public bool Activateable { get; set; }

        /// <summary>
        /// Gets or sets is custom.
        /// </summary>
        public bool Custom { get; set; }

        #endregion

        #region Constructors

        #endregion
    }
}
