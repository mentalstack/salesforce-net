SalesforceNet
=============

Salesforce REST wrapper for .NET.

SalesforceNet is **lightweight and entity oriented** Salesforce wrapper. Password, authorization code and token oAuth flows are supported. It automatically extracts fields from entities and builds required SOQL requests. ExtractRecursively attribute allows you to get **related data**, for example Contact's Account. Or User's Account. Ignore attributes allow to filter what fields should be ignored from Get or Create.

For first you should to define your Salesforce entity derived from BaseEntity:

```CSharp
/// <summary>
/// LMS_Attempt__c object.
/// </summary>
[JsonObject("LMS_Attempt__c")]
public class AttemptCobject : BaseEntity
{
	#region Public Properties

	/// <summary>
	/// Gets or sets eid.
	/// </summary>
	[External]
	[JsonProperty("Eid__c")]
	public string Eid { get; set; }

	/// <summary>
	/// Gets or sets created date.
	/// </summary>
	[JsonProperty("Created__c")]
	public DateTime Created { get; set; }

	/// <summary>
	/// Gets or sets user id.
	/// </summary>
	[JsonProperty("User__c")]
	public string UserId { get; set; }

	/// <summary>
	/// Gets or sets user.
	/// </summary>
	[ExtractRecursively]
	[JsonProperty("User__r")]
	public UserCobject User { get; set; }

	/// <summary>
	/// Gets or sets learning element id.
	/// </summary>
	[JsonProperty("LearningElement__c")]
	public string LearningElementId { get; set; }

	/// <summary>
	/// Gets or sets learning element.
	/// </summary>
	[ExtractRecursively]
	[JsonProperty("LearningElement__r")]
	public LearningElementCobject LearningElement { get; set; }

	/// <summary>
	/// Gets or sets session time.
	/// </summary>
	[JsonProperty("SessionTime__c")]
	public float SessionTime { get; set; }

	/// <summary>
	/// Gets or sets completion status.
	/// </summary>
	[JsonProperty("CompletionStatus__c")]
	public string CompletionStatus { get; set; }

	/// <summary>
	/// Gets or sets success status.
	/// </summary>
	[JsonProperty("SuccessStatus__c")]
	public string SuccessStatus { get; set; }

	/// <summary>
	/// Gets or sets score raw.
	/// </summary>
	[JsonProperty("ScoreRaw__c")]
	public decimal ScoreRaw { get; set; }

	/// <summary>
	/// Gets or sets score min.
	/// </summary>
	[JsonProperty("ScoreMin__c")]
	public decimal ScoreMin { get; set; }

	/// <summary>
	/// Gets or sets score max.
	/// </summary>
	[JsonProperty("ScoreMax__c")]
	public decimal ScoreMax { get; set; }
	
	/// <summary>
	/// Gets or sets name.
	/// </summary>
	public string Name { get; set; }

	#endregion
}
```

Getting, Fetching
-----------------

```CSharp 
List<AttemptCobject> attempts = _salesforce.GetMany<AttemptCobject>(where, order); 
```

Creating
--------

```CSharp
_salesforce.Create<AttemptCobject>(new AttemptCobject { ... });
```